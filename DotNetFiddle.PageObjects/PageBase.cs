using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace DotNetFiddle.PageObjects
{
    public abstract class PageBase
    {
        public static IWebDriver Driver => DriverFactory.Instance;

        public DefaultWait<IWebDriver> Wait { get; } = new DefaultWait<IWebDriver>(Driver)
        {
            Timeout = TimeSpan.FromSeconds(15)
        };

        public IWebElement Locate(By by)
        {
            Wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(StaleElementReferenceException));

            return Wait.Until(_ => Driver.FindElement(by));
        }

        public void HoverElement(IWebElement element)
        {
            var action = new Actions(Driver);
            action.MoveToElement(element).Perform();
        }
    }
}
