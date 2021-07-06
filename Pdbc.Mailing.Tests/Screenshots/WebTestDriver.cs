using System;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;

namespace Pdbc.Mailing.Tests.Screenshots
{


    public class WebTestDriver
    {
        public static IWebDriver Driver { get; set; }

        public static void Initialize(IWebDriverConfiguration configuration)
        {
            if (Driver == null)
            {
                Driver = WebDriverFactory.SetupWebDriver(configuration);
                TurnOnWait();
            }
        }

        public static void NoWait(Action action)
        {
            TurnOffWait();
            action?.Invoke();
            TurnOnWait();
        }

        public static void Close()
        {
            try
            {
                //Driver.Dispose();
                Driver?.Quit();
                Driver = null;
            }
            finally
            {

            }
        }

        private static void TurnOnWait()
        {
            //Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
            //Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        private static void TurnOffWait()
        {
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(0);
        }

        private static void ClearChromeProcesses()
        {
            // Kill all chrome processes
            var chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
            var chromeProcesses = Process.GetProcessesByName("chrome");

            foreach (var p in chromeDriverProcesses.Union(chromeProcesses))
            {
                try
                {
                    p.Kill();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}

