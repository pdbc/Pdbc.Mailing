using System;
using Pdbc.Mailing.RazorEngine.Templates.BaseTemplate;

namespace Pdbc.Mailing.RazorEngine.Templates
{
    public interface IMailTemplateCacheService
    {
        BaseCompiledTemplate GetHtmlTemplate(string key, string languageCode);
        BaseCompiledTemplate GetTextOnlyTemplate(string key, string languageCode);
    }

    public class MailTemplateCacheService : IMailTemplateCacheService
    {
        private readonly ITemplateFileLoader _templateFileFileLoader;
        private readonly RazorEngineCore.RazorEngine _razorEngine;

        //private ConcurrentDictionary<string, BaseCompiledTemplate> TemplateCache = new ConcurrentDictionary<string, BaseCompiledTemplate>();

        public MailTemplateCacheService(ITemplateFileLoader templateFileLoader, RazorEngineCore.RazorEngine razorEngine)
        {
            // Inject 
            _templateFileFileLoader = templateFileLoader;
            _razorEngine = razorEngine;
        }

        public BaseCompiledTemplate GetHtmlTemplate(string key, string languageCode)
        {
            string templateKey = $"{key}_{languageCode}";
            string defaultLanguageKey = $"{key}_en";
            //string cacheKey = $"{templateKey}_html";

            // TODO PERFO - Try-Load template from file system (implement save&load on template, verify file exists and load) => take time into account for refresh of template???
            var parts = _templateFileFileLoader.GetLayoutPartsFor(languageCode);

            var templateInfo = _templateFileFileLoader.GetHtmlMailTemplateInfoFor(templateKey, defaultLanguageKey, key);
            if (templateInfo != null)
            {
                var compiledTemplate = _razorEngine.Compile(templateInfo.TemplateBody, parts);
                //compiledTemplate.Save();
                return compiledTemplate;
            }

            throw new NotSupportedException($"{key}_{languageCode} cannot be compiled");
        }


        public BaseCompiledTemplate GetTextOnlyTemplate(string key, string languageCode)
        {

            string templateKey = $"{key}_{languageCode}";
            string defaultLanguageKey = $"{key}_en";
            //string cacheKey = $"{templateKey}_text";

            // TODO Try-Load template from file system (implement save&load on template, verify file exists and load) => take time into account for refresh of template???
            var parts = _templateFileFileLoader.GetLayoutPartsFor(languageCode);

            var templateInfo = _templateFileFileLoader.GetTextMailTemplateInfoFor(templateKey, defaultLanguageKey, key);
            if (templateInfo != null)
            {
                var compiledTemplate = _razorEngine.Compile(templateInfo.TemplateBody, parts);
                //compiledTemplate.Save();
                return compiledTemplate;
            }

            return null;
        }
    }
}