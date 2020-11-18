using NUnit.Framework;
using DotNetFiddle.PageObjects;
using System;

namespace DotNetFiddle.Tests
{
    public class NugetPackageTests
    {
        [ThreadStatic] public static MainPage MainPage;

        [SetUp]
        public void Setup()
        {
            DriverFactory.Build("chrome");
            MainPage = new MainPage();
            MainPage.Navigate();
        }

        [Test]
        public void ShouldRunScriptAndReturnHelloWorld()
        {
            MainPage.RunButton.Click();
            Assert.True(MainPage.OutputHasText("Hello World"));
        }

        [Test]
        public void ShouldAddPackages()
        {
            MainPage.SearchNugetPackages("nunit");
            MainPage.SelectNugetPackage("NUnit", "3.12.0");

            Assert.True(MainPage.NugetIsInstalled("NUnit"));
        }

        [TearDown]
        public void Teardown()
        {
            DriverFactory.Instance.Close();
            DriverFactory.Instance?.Quit();
        }
    }
}