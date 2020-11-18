using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace DotNetFiddle.PageObjects
{
    public static class DriverFactory
    {
        [ThreadStatic] public static IWebDriver Instance;

        public static void Build(string name)
        {
            Instance = name switch
            {
                "chrome" => new ChromeDriver(),
                "firefox" => new FirefoxDriver(),
                _ => throw new ArgumentException($"'{name}' is not a supported browser"),
            };
        }
    }
}