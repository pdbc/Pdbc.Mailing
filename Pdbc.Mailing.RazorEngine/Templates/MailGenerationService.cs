using System;
using Pdbc.Mailing.RazorEngine.Models;

namespace Pdbc.Mailing.RazorEngine.Templates
{
    public interface IMailGenerationService
    {
        String GenerateHtmlMail(IMailViewModel mailViewModel);
        String GenerateTextOnlyMail(IMailViewModel mailViewModel);
    }

    public class MailGenerationService : IMailGenerationService
    {
        private readonly IMailTemplateCacheService _mailTemplateCacheService;
        public MailGenerationService(IMailTemplateCacheService mailTemplateCacheService)
        {
            _mailTemplateCacheService = mailTemplateCacheService;
        }


        public String GenerateHtmlMail(IMailViewModel mailViewModel)
        {
            // Retrieve the template
            var compiledTemplate = _mailTemplateCacheService.GetHtmlTemplate(mailViewModel.MailInfo.TemplateName, mailViewModel.MailInfo.TemplateLanguageCode);
            var result = compiledTemplate.Run(mailViewModel);
            return result;
        }

        public String GenerateTextOnlyMail(IMailViewModel mailViewModel)
        {
            // Retrieve the template
            var compiledTemplate = _mailTemplateCacheService.GetTextOnlyTemplate(mailViewModel.MailInfo.TemplateName, mailViewModel.MailInfo.TemplateLanguageCode);

            var result = compiledTemplate?.Run(mailViewModel);
            return result;
        }
    }
}