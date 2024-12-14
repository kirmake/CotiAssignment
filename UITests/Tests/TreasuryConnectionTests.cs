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

        [SetUp]
        public void SetUp()
        {
            _driver = DriverFactory.CreateChromeDriver();
            _treasuryPage = new TreasuryPage(_driver);
        }

        [Test]
        public void TestTreasuryConnection()
        {
            try
            {
                _treasuryPage.Open();
                _treasuryPage.ClickConnect();
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
