using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace UITests.Utilities
{
    internal class DriverFactory
    {
        public static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("--start-maximized");
            options.AddArguments("disable-blink-features=AutomationControlled");
            return new ChromeDriver(options);
        }
    }
}
