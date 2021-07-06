
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using Microsoft.Edge.SeleniumTools;

namespace Pdbc.Mailing.Tests.Screenshots
{
    public class WebDriverFactory
    {
        private static bool _runHeadless;

        public static IWebDriver SetupWebDriver(IWebDriverConfiguration configuration)
        {
            IWebDriver webDriver;
            var driver = configuration.Driver;

            _runHeadless = configuration.RunHeadless;

            switch (driver)
            {
                case "Chrome":
                    webDriver = BuildChromeWebDriver();
                    break;

                case "Chromium":
                    webDriver = BuildChromiumWebDriver();
                    break;

                default:
                    var options = new InternetExplorerOptions
                    {
                        // may need to set zoom to 100% ?
                        IgnoreZoomLevel = true,
                        // we are unable to set protected mode for all zones, settings managed remotely
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                    };
                    //options.AddAdditionalCapability("download.default_directory", downloadPath);

                    webDriver = new InternetExplorerDriver(options);
                    break;

            }


            // Extra Default settings (timeout)
            //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //webDriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(10));
            webDriver.Manage().Window.Maximize();

            // Reset alle cookies
            webDriver.ClearAllCookies();
            return webDriver;
        }

        private static IWebDriver BuildChromiumWebDriver()
        {
            var edgeOptions = GetEdgeOptions();
            var edgeDriverDirectory = TestContext.CurrentContext.TestDirectory;

            var service = EdgeDriverService.CreateChromiumService(edgeDriverDirectory, "msedgedriver.exe");
            service.HideCommandPromptWindow = true;

            IWebDriver webDriver = new EdgeDriver(service, edgeOptions);

            return webDriver;
        }

        private static EdgeOptions GetEdgeOptions()
        {
            var options = new EdgeOptions { UseChromium = true };
            options.AddArgument("disable-gpu");

            if (_runHeadless)
            {
                options.AddArgument("headless");
            }

            return options;
        }

        private static IWebDriver BuildChromeWebDriver()
        {
            var chromeOptions = GetChromeOptions();

            var chromeDriverDirectory = TestContext.CurrentContext.TestDirectory;
            var service = ChromeDriverService.CreateDefaultService(chromeDriverDirectory);
            service.HideCommandPromptWindow = true;
            service.EnableVerboseLogging = true;

            IWebDriver webDriver = new ChromeDriver(service, chromeOptions);

            return webDriver;
        }

        private static ChromeOptions GetChromeOptions()
        {
            var chromeOptions = new ChromeOptions();

            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            chromeOptions.AddExcludedArgument("enable-automation");

            // Wait 
            //chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal; //(PageLoadStrategy.NONE);
            chromeOptions.AddArguments("--window-size=1920,1080");
            chromeOptions.AddArguments("--start-maximized");
            chromeOptions.AddArguments("--no-sandbox");
            chromeOptions.AddArguments("--disable-extensions");
            chromeOptions.AddArguments("--disable-infobars");
            chromeOptions.AddArguments("--disable-gpu");
            chromeOptions.AddArguments("--disable-setuid-sandbox");
            chromeOptions.AddArguments("--dns-prefetch-disable");
            chromeOptions.AddArguments("--allow-running-insecure-content");
            chromeOptions.AddArguments("--ignore-certificate-errors");
            chromeOptions.AddArguments("--always-authorize-plugins");

            if (_runHeadless)
            {
                chromeOptions.AddArguments("--headless");
                chromeOptions.AddArguments("--disable-features=UserAgentClientHint");
            }

            return chromeOptions;
        }
    }
}