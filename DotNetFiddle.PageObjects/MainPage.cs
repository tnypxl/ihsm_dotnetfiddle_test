using OpenQA.Selenium;

namespace DotNetFiddle.PageObjects
{
    public class MainPage : PageBase
    {
        public void Navigate()
        {
            Driver.Navigate().GoToUrl("https://dotnetfiddle.net");
        }

        public void SearchNugetPackages(string query)
        {
            NugetSearchField.Click();
            NugetSearchField.SendKeys(query);
        }

        public void SelectNugetPackage(string name, string version)
        {
            HoverElement(NugetPackage(name));
            NugetPackageVersion(version).Click();
        }

        public bool OutputHasText(string text)
        {
            return Output.Text == text;
        }

        public bool NugetIsInstalled(string nugetName)
        {
            return InstalledNugetPackage(nugetName).Displayed;
        }

        public IWebElement RunButton => Locate(By.Id("run-button"));

        public IWebElement NugetSearchField => Locate(By.CssSelector("input[type='search'].new-package"));

        public IWebElement NugetPackageMenu => Locate(By.Id("menu"));

        public IWebElement NugetPackage(string name) => Locate(By.XPath($"//a[@package-id='{name}']"));

        public IWebElement NugetPackageVersion(string version) => Locate(By.XPath($"//a[@version-name and text()='{version}']"));

        public IWebElement InstalledNugetPackage(string name) => Locate(By.XPath($"//div[@class='package-name' and @package-id='{name}']"));

        public IWebElement Output => Locate(By.Id("output"));
    }
}