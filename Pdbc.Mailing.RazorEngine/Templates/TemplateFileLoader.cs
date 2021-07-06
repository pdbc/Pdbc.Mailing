using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pdbc.Mailing.RazorEngine.Extensions;

namespace Pdbc.Mailing.RazorEngine.Templates
{
    public interface ITemplateFileLoader
    {
        TemplateInfo GetHtmlMailTemplateInfoFor(String templateKey, String defaultLanguageTemplateKey, String defaultKey);
        TemplateInfo GetTextMailTemplateInfoFor(String templateKey, String defaultLanguageTemplateKey, String defaultKey);
        Dictionary<String,String> GetLayoutPartsFor(string languageCode);
    }

    public class TemplateFileLoader : ITemplateFileLoader
    {
        private String HtmlMailsDirectory { get; set; }
        private String TextMailsDirectory { get; set; }
        private String LayoutDirectory { get; set; }

        private ConcurrentDictionary<string, TemplateInfo> MailParts = new ConcurrentDictionary<string, TemplateInfo>();
        private ConcurrentDictionary<string, TemplateInfo> MailTextParts = new ConcurrentDictionary<string, TemplateInfo>();
        private ConcurrentDictionary<string, TemplateInfo> LayoutParts = new ConcurrentDictionary<string, TemplateInfo>();

        
        public TemplateFileLoader(IMailTemplateConfiguration configuration)
        {
            HtmlMailsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.TemplateDirectory, "Mails");
            TextMailsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.TemplateDirectory, "Text");
            LayoutDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.TemplateDirectory, "Layout");

            // Load all templates from file system
            LoadHtmlMailParts();
            LoadTextMailParts();
            LoadLayoutParts();

        }

        public TemplateInfo GetHtmlMailTemplateInfoFor(String templateKey, String defaultLanguageTemplateKey, String defaultKey)
        {
            var result = GetTemplateInfoFor(MailParts, templateKey.ToLowerInvariant(), defaultLanguageTemplateKey.ToLowerInvariant(), defaultKey.ToLowerInvariant());
            if (result != null)
                return result;

            throw new NotSupportedException($"{templateKey} mail part not found");
        }

        public TemplateInfo GetTextMailTemplateInfoFor(String templateKey, String defaultLanguageTemplateKey, String defaultKey)
        {
            return GetTemplateInfoFor(MailTextParts, templateKey.ToLowerInvariant(), defaultLanguageTemplateKey.ToLowerInvariant(), defaultKey.ToLowerInvariant());
        }

        private TemplateInfo GetTemplateInfoFor(IDictionary<String, TemplateInfo> items,  String templateKey, String defaultLanguageTemplateKey, String defaultKey)
        {
            if (items.Keys.Contains(templateKey.ToLowerInvariant()))
            {
                var one = items[templateKey.ToLowerInvariant()];
                if (one != null)
                    return one;
            }

            if (items.Keys.Contains(defaultLanguageTemplateKey.ToLowerInvariant()))
            {
                var two = items[defaultLanguageTemplateKey.ToLowerInvariant()];
                if (two != null)
                    return two;
            }

            if (items.Keys.Contains(defaultKey.ToLowerInvariant()))
            {
                var three = items[defaultKey.ToLowerInvariant()];
                if (three != null)
                    return three;
            }

            return null;
        }


        #region Load all templates from file system
        private void LoadHtmlMailParts()
        {
            var layoutTemplates = GetAllPossibleTemplates(HtmlMailsDirectory);
            foreach (var t in layoutTemplates)
            {
                MailParts.GetOrAdd(t.Key.ToLowerInvariant(), t);
            }
        }
        private void LoadTextMailParts()
        {
            var layoutTemplates = GetAllPossibleTemplates(TextMailsDirectory);
            foreach (var t in layoutTemplates)
            {
                MailTextParts.GetOrAdd(t.Key.ToLowerInvariant(), t);
            }
        }
        private void LoadLayoutParts()
        {
            var layoutTemplates = GetAllPossibleTemplates(LayoutDirectory);
            foreach (var t in layoutTemplates)
            {
                LayoutParts.GetOrAdd(t.Key.ToLowerInvariant(), t);
            }
        }
        #endregion


        private IList<TemplateInfo> GetAllPossibleTemplates(String baseDirectory)
        {
            return GetCshtmlFilesFrom(baseDirectory)
                .Select(f => new TemplateInfo(f))
                .ToList();
        }

        private IList<String> GetCshtmlFilesFrom(String baseDirectory)
        {
            var files = Directory.GetFiles(baseDirectory, "*.cshtml").ToList();
            var directories = Directory.GetDirectories(baseDirectory);
            foreach (var directory in directories)
            {
                files.AddRange(GetCshtmlFilesFrom(directory));
            }
            return files;
        }

        public Dictionary<String,String> GetLayoutPartsFor(string languageCode)
        {
            var parts = new Dictionary<String, String>();

            
            parts.Add("footer", FindLayoutPartFor("footer", languageCode));
            parts.Add("greeting", FindLayoutPartFor("greeting", languageCode));
            parts.Add("header", FindLayoutPartFor("header", languageCode));
            parts.Add("horizontal-divider", FindLayoutPartFor("horizontal-divider", languageCode));
            parts.Add("layout", FindLayoutPartFor("layout", languageCode));
            parts.Add("stylesheet", FindLayoutPartFor("stylesheet", languageCode));

            return parts;
        }

        private String FindLayoutPartFor(string key, string language)
        {
            key = key.ToLowerInvariant();
            language = language?.ToLowerInvariant();

            if (language != null && language?.SafeTrim() != "")
            {
                if (LayoutParts.Keys.Contains($"{key}_{language}"))
                {
                    var p = LayoutParts[$"{key}_{language}"];
                    if (p != null)
                        return p.TemplateBody;
                }
            }

            if (LayoutParts.Keys.Contains($"{key}"))
            {
                var p = LayoutParts[$"{key}"];
                if (p != null)
                    return p.TemplateBody;
            }

            throw new NotSupportedException($"{key}_{language} layout part not found");
        }
    }
}