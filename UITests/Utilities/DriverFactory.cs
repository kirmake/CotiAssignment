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

        public static IWebDriver CreateChromeDriverWithMetaMask()
        {
            var options = new ChromeOptions();
            options.AddArguments("--start-maximized");
            options.AddArgument($"user-data-dir={Path.Combine(Directory.GetCurrentDirectory(), "ChromeProfiles", "TestProfile")}"); 
            options.AddArguments("disable-blink-features=AutomationControlled");
            options.AddExtension(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ChromeExtentions\\metamask.crx"));
            
            return new ChromeDriver(options);
        }
    }
}
