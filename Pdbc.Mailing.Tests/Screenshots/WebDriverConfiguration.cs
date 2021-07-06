using Microsoft.Extensions.Configuration;

namespace Pdbc.Mailing.Tests.Screenshots
{
    public interface IWebDriverConfiguration
    {
        string ScreenShotDirectory { get; }
        string Driver { get; }
        bool RunHeadless { get; }
    }

    public class WebDriverConfiguration : IWebDriverConfiguration
    {
        public WebDriverConfiguration(IConfiguration configuration)
        {
            configuration.GetSection("WebTests:WebDriver").Bind(this);
        }
        public string ScreenShotDirectory { get; set; }
        public string Driver { get; set; }
        public bool RunHeadless { get; set; }
       
    }
}