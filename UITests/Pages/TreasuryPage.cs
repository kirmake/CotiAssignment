using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using UITests.URLs;

namespace UITests.Pages
{
    public class TreasuryPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public TreasuryPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void Open()
        {
            _driver.Navigate().GoToUrl(Urls.TreasuryPageUrl);
        }

        public void ClickConnect()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ConnectButton)).MoveAndClick();
        }

        public bool IsUnauthorisedPopUpPresented()
        {
            try
            {
                IWebElement element = _wait.Until(ExpectedConditions.ElementExists(UnauthorisedPopUp));
                return element != null;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsWalletConnectionAddressPresented()
        {
            try
            {
                IWebElement element = _wait.Until(ExpectedConditions.ElementExists(WalletConnectionAddress));
                return element != null;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void AgreeAndConnectViaMetaMask()
        {
            var metaMaskWrapper = _driver.FindElement(MetaMaskWrapper);
            var agreeCheckBox = metaMaskWrapper.FindElement(MetaMaskCheckbox);
            agreeCheckBox.MoveAndClick();

            var metamaskSelectButton = metaMaskWrapper.FindElement(MetaMaskSelectButton);
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(MetaMaskSelectButton)).MoveAndClick();
        }
        private By ConnectButton => By.CssSelector("[class^='WalletConnectButton_wrapper'] button");
        private By MetaMaskWrapper => By.XPath(".//div[contains(@class, 'ConnectType_wrapper') and .//div[text()='MetaMask']]");
        private By MetaMaskCheckbox => By.XPath(".//label[contains(@class, 'CheckBox_container')]//span");
        private By ConnectTypeModal => By.CssSelector("[class^='ModalContainer_container']");
        private By MetaMaskSelectButton => By.XPath(".//button[text()='SELECT']");
        private By UnauthorisedPopUp => By.XPath(".//div[contains(@class, 'UnauthorizedCountryModal_wrapper')]");
        private By WalletConnectionAddress => By.XPath(".//div[contains(@class, 'WalletConnectButton_address')]");

    }
}
