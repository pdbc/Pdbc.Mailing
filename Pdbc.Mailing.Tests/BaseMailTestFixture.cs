using System.IO;
using System.Net.Mail;
using NUnit.Framework;
using Pdbc.Mailing.RazorEngine.Templates;
using Pdbc.Mailing.RazorEngine.Models;
using Pdbc.Mailing.Sample.Models;

namespace Pdbc.Mailing.Tests
{
    public class BaseMailTestFixture 
    {
        protected IMailGenerationService MailEngineService; // => Container.Resolve<IMailGenerationService>();

        private string SaveEmailAsHtmlToScreenshotfolder(string mailBody, string filename)
        {
            var mailOutputfolder = Path.Combine(GetScreenshotFolder());
            var mailOutputPath = Path.Combine(mailOutputfolder, filename);

            System.IO.File.WriteAllText(mailOutputPath, mailBody);

            return mailOutputPath;
        }

        private string GetScreenshotFolder()
        {
            return @"c:\temp";
        }

        private string GetMailNamefor(string name, string language)
        {
            var mailFileName = $"{language}_{name}.html";
            return mailFileName;
        }

        [Test]
        public void Verify_template()
        {
            //// SETUP
            //var viewmodel = new InvitationViewModelTestDataBuilder()
            //    .WithApplicationScope("ApplicationScopeName")
            //    .Build();
            //var mailInfo = new MailInfoTestDataBuilder()
            //    .WithTemplateName(MailTemplates.InvitationForSingleApplicationPortal)
            //    .WithTemplateLanguageCode(language)
            //    .WithSubject(MailTemplateSubjectResolver.GetSubjectFor(MailTemplates.InvitationForSingleApplicationPortal, language).FormatWith(viewmodel.AccountOfficialName));
            //viewmodel.MailInfo = mailInfo;
            ////viewmodel = new InvitationViewModelTestDataBuilder().WithMailInfo(mailInfo).Build();

            //string mailName = null;
            //string language = null;
            MailOneViewModel viewmodel = new MailOneViewModel();
            viewmodel.MailTitle = "Some title to add";
            viewmodel.MailCommunicationInfo = new MailCommunicationInfo()
            {
                Firstname = "Firstname",
                Lastname = "Lastname",
                Priority = MailPriority.High,
                Subject = "Subject",
                ToEmailAddress = "test@test.com"
            };
            viewmodel.MailTemplateInfo = new MailTemplateInfo()
            {
                TemplateLanguageCode = "EN",
                TemplateName = "MailOne"
            };

            var mailBody = MailEngineService.GenerateHtmlMail(viewmodel);

            //var mailPath = SaveEmailAsHtmlToScreenshotfolder(mailBody, GetMailNamefor(mailName, language));

            //NavigateToPage(mailPath).TakeScreenshot(GetScreenshotNameFor(mailName));
            // Move + Rename screenshots to folder
            //MoveScreenshotsToDocumentationFolder(language);
        }


    }
}
