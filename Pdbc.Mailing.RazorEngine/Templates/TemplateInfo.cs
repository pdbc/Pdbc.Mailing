using System;
using System.IO;

namespace Pdbc.Mailing.RazorEngine.Templates
{
    public class TemplateInfo
    {
        public TemplateInfo(String filename)
        {
            var fileInfo = new FileInfo(filename);

            FilePath = fileInfo.FullName;
            Name = fileInfo.Name;
            Key = Name.Replace(".cshtml", "");

            var parts = Key.Split('_');
            if (parts.Length == 1)
            {
                TemplateName = parts[0];
            } else if (parts.Length == 2)
            {
                TemplateName = parts[0];
                LanguageName = parts[1];
            }

            TemplateBody = File.ReadAllText(FilePath);
        }

        public string FilePath { get; set; }

        public string LanguageName { get; set; }

        public string TemplateName { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public String TemplateBody { get; set; }
    }
}