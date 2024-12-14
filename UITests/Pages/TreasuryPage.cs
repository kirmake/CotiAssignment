using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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

        private By ConnectButton => By.CssSelector("[class^='WalletConnectButton_wrapper'] button");

        public void ClickConnect()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ConnectButton)).Click();
        }
    }
}
