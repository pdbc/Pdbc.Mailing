using Microsoft.Extensions.Configuration;

namespace Pdbc.Mailing.RazorEngine
{
    public interface IMailTemplateConfiguration
    {
        string TemplateDirectory { get; }

        string ImageDirectory { get; }

    }
    public class MailTemplateConfiguration : IMailTemplateConfiguration
    {
        public MailTemplateConfiguration(IConfiguration configuration)
        {
            configuration.GetSection("MailTemplateConfiguration").Bind(this);
        }

        public string TemplateDirectory { get; set; }
        public string ImageDirectory { get; set; }
    }
}