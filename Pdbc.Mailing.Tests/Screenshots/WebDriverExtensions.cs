using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pdbc.Mailing.Tests.Screenshots
{
    public static class WebDriverExtensions
    {
        public static IEnumerable<IWebElement> FindElementsByCssClassNameDirect(this IWebDriver webElement, String cssClass)
        {
            var elements = webElement.FindElements(By.ClassName(cssClass));
            return elements;
        }

        #region FindElements (Direct) => Private use Func<IWebElement> variants


        private static IWebElement FindElementByXPathDirect(this IWebDriver webElement, String xpath)
        {
            var element = webElement.FindElement(By.XPath(xpath));
            return element;
        }

        private static IWebElement FindElementByIdDirect(this IWebDriver webElement, String id)
        {
            var element = webElement.FindElement(By.Id(id));
            return element;
        }



        private static IWebElement FindElementByTagDirect(this IWebDriver webElement, String tag)
        {
            var element = webElement.FindElement(By.TagName(tag));
            return element;
        }
        private static IEnumerable<IWebElement> FindElementsByTagDirect(this IWebDriver webElement, String tag)
        {
            var element = webElement.FindElements(By.TagName(tag));
            return element;
        }


        private static IWebElement FindElementByCssSelectorNameDirect(this IWebDriver webElement, String cssClass)
        {
            var element = webElement.FindElement(By.CssSelector(cssClass));
            return element;
        }
        private static IEnumerable<IWebElement> FindElementsByCssSelectorNameDirect(this IWebDriver webElement, String cssClass)
        {
            var elements = webElement.FindElements(By.CssSelector(cssClass));
            return elements;
        }



        private static IWebElement FindElementByCssClassNameDirect(this IWebDriver webElement, String cssClass)
        {
            var element = webElement.FindElement(By.ClassName(cssClass));
            return element;
        }

        #endregion

        #region FindElement (Function pointers)


        public static Func<IWebElement> FindElementByXPathFunction(this IWebDriver webElement, String xpath)
        {
            Func<IWebElement> f = () => webElement.FindElementByXPathDirect(xpath);
            return f;
        }

        public static Func<IWebElement> FindElementByIdFunction(this IWebDriver webElement, String id)
        {
            Func<IWebElement> f = () => webElement.FindElementByIdDirect(id);
            return f;
        }

        public static Func<IWebElement> FindElementByTagFunction(this IWebDriver webElement, String tag)
        {
            Func<IWebElement> f = () => webElement.FindElementByTagDirect(tag);
            return f;
        }
        public static Func<IEnumerable<IWebElement>> FindElementsByTagFunction(this IWebDriver webElement, String tagname)
        {
            Func<IEnumerable<IWebElement>> f = () => webElement.FindElementsByTagDirect(tagname);
            return f;
        }


        public static Func<IWebElement> FindElementByCssSelectorFunction(this IWebDriver webElement, String cssSelector)
        {
            Func<IWebElement> f = () => webElement.FindElementByCssSelectorNameDirect(cssSelector);
            return f;
        }
        public static Func<IEnumerable<IWebElement>> FindElementsByCssSelectorFunction(this IWebDriver webElement, String tagname)
        {
            Func<IEnumerable<IWebElement>> f = () => webElement.FindElementsByCssSelectorNameDirect(tagname);
            return f;
        }


        public static Func<IWebElement> FindElementByCssClassNameFunction(this IWebDriver webElement, String tagname)
        {
            Func<IWebElement> f = () => webElement.FindElementByCssClassNameDirect(tagname);
            return f;
        }

        public static Func<IWebElement> FindElementByCssClassNameFunctionAndValidationFieldName(this IWebDriver webElement, String tagname, string field)
        {
            Func<IWebElement> f = () => webElement.FindElementByCssClassNameDirect(tagname).FindElement(By.XPath($"//*[@data-valmsg-for='{field}']"));
            return f;
        }
        public static Func<IEnumerable<IWebElement>> FindElementsByCssClassNameFunction(this IWebDriver webElement, String tagname)
        {
            Func<IEnumerable<IWebElement>> f = () => webElement.FindElementsByCssClassNameDirect(tagname);
            return f;
        }
        #endregion

        #region Cookies

        #endregion

        public static void WaitUntillOnUrl(this IWebDriver driver, string url, int maxMillisecondsToWait = 5000)
        {
            try
            {
                // Wait for the callback url is processed.
                Func<bool> function = () => driver.Url.ToLower().Contains(url.ToLower());
                function.WaitUntilTrue(maxMillisecondsToWait, true);
            }
            catch (Exception)
            {
                throw new Exception($"Expected url to contain '{url}', but was {driver.Url}");
            }
        }

        public static IWebElement WaitUntillElementIsClickable(this IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementIsClickable(locator));
            return element;
        }

        public static IWebElement WaitUntillElementIsVisible(this IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element;
        }


        public static void WaitUntillPageContainsText(this IWebDriver driver, string sourceText, int maxMillisecondsToWait = 5000)
        {
            try
            {
                // Wait for the callback url is processed.
                Func<bool> function = () => driver.PageSource.Contains(sourceText);
                function.WaitUntilTrue(maxMillisecondsToWait, true);
            }
            catch (Exception)
            {
                throw new Exception($"Expected page to contain '{sourceText}'");
            }
        }
    }
}