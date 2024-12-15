using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITests.Settings;

namespace UITests.ChromeExtentions
{
    internal class MetamaskHandler
    {
        private readonly IWebDriver _driver;
        private readonly string _extensionId = "your-extension-id"; // Replace with your actual extension ID

        public MetamaskHandler(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void LogInViaMetaMask()
        {
            OpenExtension();
            SwitchToExtensionPopup();
            EnterPassword();
            SwitchBackToMainWindow();
        }

        private void OpenExtension()
        {
            Console.WriteLine("Opening extension...");
            ((IJavaScriptExecutor)_driver).ExecuteScript($"chrome.runtime.sendMessage('{Settings.MetaMaskId}', {{action: 'open-popup'}});");
        }

        private void SwitchToExtensionPopup()
        {
            // Wait for the extension popup to open
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.WindowHandles.Count > 1);

            // Switch to the most recent window (extension popup)
            _driver.SwitchTo().Window(_driver.WindowHandles[^1]);
            Console.WriteLine("Switched to extension popup.");
        }

        private void EnterPassword()
        {
            Console.WriteLine("Entering password...");

            // Locate the password field
            var passwordField = _driver.FindElement(By.CssSelector("input[type='password']"));

            // Enter the password
            passwordField.SendKeys(Settings.Password);

            // Locate and click the submit/login button
            var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            Console.WriteLine("Password entered and submitted.");
        }

        private void SwitchBackToMainWindow()
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            Console.WriteLine("Switched back to main browser window.");
        }
    }

}
