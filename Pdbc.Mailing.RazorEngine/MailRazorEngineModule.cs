using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pdbc.Mailing.RazorEngine.Templates;

namespace Pdbc.Mailing.RazorEngine
{
    public class MailRazorEngineModule //: IModule
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITemplateFileLoader, TemplateFileLoader>();
            services.AddSingleton<IMailTemplateCacheService, MailTemplateCacheService>();
            services.AddScoped<IMailGenerationService, MailGenerationService>();
            services.AddScoped<IMailTemplateConfiguration, MailTemplateConfiguration>();
            services.AddScoped<RazorEngineCore.RazorEngine>();

        }
    }
}
