using OpenQA.Selenium;
using UITests.Utilities;

namespace UITests.Tests
{
    [TestFixture]
    public class TreasuryTest
    {

        //TODO fix issue
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        private IWebDriver _driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

        [SetUp]
        public void SetUp()
        {
            _driver = DriverFactory.CreateChromeDriver();
        }

        [Test]
        public void TestTreasuryConnection()
        {
            try
            {
                _driver.Navigate().GoToUrl("https://treasury.coti.io/");
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
