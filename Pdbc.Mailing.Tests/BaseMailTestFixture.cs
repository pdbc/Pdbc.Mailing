using System.IO;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Pdbc.Mailing.RazorEngine;
using Pdbc.Mailing.RazorEngine.Templates;
using Pdbc.Mailing.RazorEngine.Models;
using Pdbc.Mailing.Sample.Models;
using Pdbc.Mailing.Tests.Screenshots;

namespace Pdbc.Mailing.Tests
{
    public class BaseMailTestFixture
    {
        

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

        private IConfiguration LoadConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return configurationBuilder.Build();

        }
        [Test]
        public void Verify_template_generation_mail_one()
        {
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

            var configuration = LoadConfiguration();

            var services = new ServiceCollection();
            services.AddSingleton(configuration);


            services.AddSingleton<ITemplateFileLoader, TemplateFileLoader>();
            services.AddSingleton<IMailTemplateCacheService, MailTemplateCacheService>();
            services.AddScoped<IMailGenerationService, MailGenerationService>();
            services.AddScoped<IMailTemplateConfiguration, MailTemplateConfiguration>();
            services.AddScoped<RazorEngineCore.RazorEngine>();

            var serviceProvider = services.BuildServiceProvider();

            IMailGenerationService MailEngineService = serviceProvider.GetRequiredService<IMailGenerationService>(); // => Container.Resolve<IMailGenerationService>();

            var mailBody = MailEngineService.GenerateHtmlMail(viewmodel);

            var mailPath = SaveEmailAsHtmlToScreenshotfolder(mailBody, GetMailNamefor(viewmodel.MailTemplateInfo.TemplateName, viewmodel.MailTemplateInfo.TemplateLanguageCode));

            var webDriverConfiguration = new WebDriverConfiguration(configuration);
            var webdriver = WebDriverFactory.SetupWebDriver(webDriverConfiguration);
            webdriver.Navigate().GoToUrl(mailPath);
            webdriver.TakeScreenshot(webDriverConfiguration.ScreenShotDirectory);

            webdriver.Quit();
            webdriver.Dispose();
        }


    }
}
