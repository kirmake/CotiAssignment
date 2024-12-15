using ApiTests.Helpers;

namespace ApiTests.Tests
{
    [TestFixture]
    public class SpaceXApiTests
    {
        private SpaceXApiHelper _apiHelper;

        [SetUp]
        public void Setup()
        {
            _apiHelper = new SpaceXApiHelper();
        }

        [Test]
        public void GetCorrectPayloadNamesForSecondLaunchOfFalcon9()
        {
            const int _launchNumber = 2;
            const string _rocketName = "Falcon 9"; 
            var expectednames = new string[] { "Cubesats", "COTS Demo Flight 1" };

            var rocketId = _apiHelper.GetRocketIdByName(_rocketName);
            Assert.IsNotNull(rocketId, $"Rocket '{_rocketName}' not found.");

            var lauchId = _apiHelper.GetSpecificRocketLaunch(rocketId, _launchNumber);

            var payloadIds = _apiHelper.GetPayloadIdsForASpecificLaunch(lauchId);
            var payloadNames = payloadIds.Select(_apiHelper.GetPayloadName).ToArray();

            Assert.IsTrue(expectednames.All(expected => payloadNames.Contains(expected)),
                $"The array does not contain all expected elements. Expected: {string.Join(", ", expectednames)};" +
                $" Actual: {string.Join(", ", payloadNames)}");
        }
    }
}
