using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtensions;
using UITests.Settings;

namespace UITests.Pages
{
    public class MetamaskHandler
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly string _extensionId = "your-extension-id"; // Replace with your actual extension ID

        public MetamaskHandler(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        }

        public void LogInViaMetaMask()
        {
            OpenExtension();
            SwitchToMetaMaskPopup();
            EnterPassword();
            SwitchBackToMainWindow();
        }

        public void ConfirmConnectionViaMetaMask()
        {
            SwitchToMetaMaskPopup();
            Console.WriteLine("Switched to extension popup.");
            _driver.FindElement(By.XPath("//button[@data-testid=\"confirm-footer-button\"]")).JavaScriptClick();
            SwitchBackToMainWindow();
        }


        private void OpenExtension()
        {
            Console.WriteLine("Opening extension...");
            string extensionUrl = $"chrome-extension://{MetaMaskSettings.MetaMaskId}/popup.html";
            _driver.Navigate().GoToUrl(extensionUrl);
        }

        private void SwitchToMetaMaskPopup()
        {
            _driver.SwitchToWindowByExactTitle("MetaMask");
            Console.WriteLine("Switched to extension popup.");
        }



        private void EnterPassword()
        {
            Console.WriteLine("Entering password...");

            // Can't use WaitHelpers because of the FireMoth
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

            _wait.Until(driver =>
            {
                return (bool)js.ExecuteScript("return !!document.querySelector('#password');");
            });

            _driver.FindElement(By.Id("password")).SendKeys(MetaMaskSettings.MetaMaskPassword);

            _driver.FindElement(By.CssSelector("button[data-testid=\"unlock-submit\"]")).MoveAndClick();

            Console.WriteLine("Password entered and submitted.");
        }

        private void SwitchBackToMainWindow()
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            Console.WriteLine("Switched back to main browser window.");
        }
    }

}
