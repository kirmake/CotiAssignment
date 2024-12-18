﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using UITests.Settings;

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

        public static IWebDriver CreateChromeDriverWithMetaMask(bool vpnRequired=false)
        {

            var options = new ChromeOptions();
            var chromeProfile = vpnRequired ? Profiles.VpnProfileName : Profiles.DefaultProfileName;
            options.AddArguments("--start-maximized");
            options.AddArgument($"user-data-dir={Path.Combine(Directory.GetCurrentDirectory(), "ChromeProfiles", chromeProfile)}");

            options.AddArgument("--ignore-certificate-errors");
            options.AddArguments("disable-blink-features=AutomationControlled");
            options.AddExtension(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ChromeExtentions\\metamask.crx"));
            
            return new ChromeDriver(options);
        }
    }
}
