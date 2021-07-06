using System;
using System.Net.Mail;

namespace Pdbc.Mailing.RazorEngine.Models
{
    public class MailInfo
    {
        // Template
        public String TemplateName { get; set; }
        public String TemplateLanguageCode { get; set; }

        public String GetTemplateKey()
        {
            return $"{TemplateName}_{TemplateLanguageCode}";
        }
        public String GetTemplateKey(string defaultLanguage)
        {
            return $"{TemplateName}_{defaultLanguage}";
        }

        // Mail
        public string ToEmailAddress { get; set; }

        public string Subject { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        // Extra testing parameters ....
        public string Cc { get; set; }

        public string Bcc { get; set; }

        public MailPriority Priority { get; set; }
    }
}