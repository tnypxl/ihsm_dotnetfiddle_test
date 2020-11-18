using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace DotNetFiddle.Tests
{
    public class NugetPackageTests
    {
        private IWebDriver _driver;

        public IWebDriver Driver => _driver ?? throw new NullReferenceException("_driver is null");

        #region Page Elements

        public IWebElement NugetSearchField => Locate(By.CssSelector("input[type='search'].new-package"));

        public IWebElement NugetPackage(string name) => Locate(By.XPath($"//a[@package-id='{name}']"));

        public IWebElement NugetPackageVersion(string version) => Locate(By.XPath($"//a[@version-name='{version}']"));

        public IWebElement InstalledNugetPackage(string name) => Locate(By.XPath($"//div[@class='package-name' and @package-id='{name}']"));

        #endregion Page Elements

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("https://dotnetfiddle.net");
        }

        [Test]
        public void ShouldAddPackages()
        {
            NugetSearchField.Click();
            NugetSearchField.SendKeys("nunit");
            HoverElement(NugetPackage("NUnit"));
            NugetPackageVersion("3.12.0.0").Click();
            Assert.True(InstalledNugetPackage("NUnit").Displayed);
        }

        [TearDown]
        public void Teardown()
        {
            Driver.Close();
            Driver?.Quit();
        }

        public void HoverElement(IWebElement element)
        {
            var action = new Actions(Driver);
            action.MoveToElement(element).Perform();
        }

        public IWebElement Locate(By by)
        {
            var wait = new DefaultWait<IWebDriver>(Driver)
            {
                Timeout = TimeSpan.FromSeconds(10),
            };

            wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(StaleElementReferenceException));

            return wait.Until(_ => Driver.FindElement(by));
        }
    }
}