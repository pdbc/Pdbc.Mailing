using OpenQA.Selenium;

namespace Pdbc.Mailing.Tests.Screenshots
{
    public static class WebDriverCookieExtensions
    {
        public static void ClearAllCookies(this IWebDriver driver)
        {
            ICookieJar cookies = driver.Manage().Cookies;
            foreach (var c in cookies.AllCookies)
            {
                driver.ClearCookieByName(c.Name);
            };
        }

        public static void ClearCookieByName(this IWebDriver driver, string name)
        {
            driver.Manage().Cookies.DeleteCookieNamed(name);
        }
    }
}