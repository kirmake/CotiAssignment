using OpenQA.Selenium;
using UITests.Pages;
using UITests.Utilities;


namespace UITests.Tests
{
    [TestFixture]
    public class TreasuryTest
    {
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        private IWebDriver _driver;
        private TreasuryPage _treasuryPage;
        private MetamaskPage _metaMaskPage;

        [SetUp]
        public void SetUp()
        {
            var isVpnRequred = TestContext.CurrentContext.Test.Properties.Get("VPNRequired") as string;
            _driver = DriverFactory.CreateChromeDriverWithMetaMask(isVpnRequred == "true");
            _treasuryPage = new TreasuryPage(_driver);
            _metaMaskPage = new Pages.MetamaskPage(_driver);
        }

        [Test]
        public void TestTreasuryConnection()
        {
            RunMetamaskConnectionWorkflow();
            Assert.True(_treasuryPage.IsUnauthorisedPopUpPresented(), "Unauthorised PopUp is not presented");
        }

        [Test]
        [Property("VPNRequired", "true")]
        public void TestTreasuryConnectionWithVPN()
        {
            RunMetamaskConnectionWorkflow();
            Assert.True(_treasuryPage.IsWalletConnectionAddressPresented(), "Wallet Connection Address is not presented");
        }

        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(3000); //just for video recording
            _driver?.Quit();
        }

        private void RunMetamaskConnectionWorkflow()
        {
            _metaMaskPage.LogInViaMetaMask();
            _treasuryPage.Open();
            _treasuryPage.ClickConnect();
            _treasuryPage.AgreeAndConnectViaMetaMask();
            _metaMaskPage.ConfirmConnectionViaMetaMask();
        }
    }
}
