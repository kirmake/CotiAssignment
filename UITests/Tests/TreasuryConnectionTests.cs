using OpenQA.Selenium;
using UITests.Pages;
using UITests.Utilities;


namespace UITests.Tests
{
    [TestFixture]
    public class TreasuryTest
    {

        //TODO fix issue
 #pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        private IWebDriver _driver;
        private TreasuryPage _treasuryPage;
        private MetamaskHandler _metaMaskHandler;

        [SetUp]
        public void SetUp()
        {
            _driver = DriverFactory.CreateChromeDriverWithMetaMask();
            _treasuryPage = new TreasuryPage(_driver);
            _metaMaskHandler = new UITests.Pages.MetamaskHandler(_driver);
        }

        [Test]
        public void TestTreasuryConnection()
        {
            _metaMaskHandler.LogInViaMetaMask();
            _treasuryPage.Open();
            _treasuryPage.ClickConnect();
            _treasuryPage.AgreeAndConnectViaMetaMask();
            _metaMaskHandler.ConfirmConnectionViaMetaMask();
            Assert.True(_treasuryPage.IsUnauthorisedPopUpPresented(), "Unauthorised PopUp is not presented");
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
        }
    }
}
