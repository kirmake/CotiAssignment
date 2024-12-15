using RestSharp;
using Newtonsoft.Json.Linq;

namespace ApiTests.Helpers
{
    public class SpaceXApiHelper
    {
        private const string BaseUrl = "https://api.spacexdata.com/v4";
        private readonly RestClient _client;

        public SpaceXApiHelper()
        {
            _client = new RestClient(BaseUrl);
        }

        public string GetRocketIdByName(string name)
        {
            var rockets = GetRockets();
            var selectedRocket = rockets.FirstOrDefault(r => r["name"].ToString() == name);
            if (selectedRocket == null)
                throw new Exception("");
            return selectedRocket["id"].ToString();
        }

        public string GetSpecificRocketLaunch(string rocketId, int launchNumber)
        {
            var launches = GetLaunches();
            var rocketLaunches = launches
                .Where(l => l["rocket"].ToString() == rocketId)
                .OrderBy(l => DateTime.Parse(l["date_utc"].ToString()))
                .ToList();

            if (rocketLaunches.Count() < launchNumber)
                throw new Exception($"Less than {{launchNumber}} launches found for the rocket with id {{rocketId}}");

            return rocketLaunches[launchNumber - 1]["id"].ToString();
        }
        private JArray GetRockets()
        {
            var request = new RestRequest("/rockets", Method.Get);
            var response = _client.Execute(request);

            if (!response.IsSuccessful)
                throw new Exception($"Failed to fetch rockets: {response.ErrorMessage}");

            return JArray.Parse(response.Content);
        }

        public string[]? GetPayloadIdsForASpecificLaunch(string launchId)
        {
            var launchDetails = GetLaunchDetails(launchId);
            if (launchDetails == null)
                throw new Exception($"No details found for launchId {{launchId}}");
            if (launchDetails["payloads"] == null) 
                throw new Exception($"No payloads found for lunchId {{launchId}}");
            return launchDetails["payloads"].ToObject<string[]>();
        }

        public string GetPayloadName(string payloadId)
        {
            var payloadDetails = GetPayloadDetails(payloadId);
            if (payloadDetails == null)
                throw new Exception($"No details found for payload {{payloadId}}");
            return payloadDetails["name"].ToString();
        }

        private JArray GetLaunches()
        {
            var request = new RestRequest("/launches", Method.Get);
            var response = _client.Execute(request);

            if (!response.IsSuccessful)
                throw new Exception($"Failed to fetch launches: {response.ErrorMessage}");

            return JArray.Parse(response.Content);
        }

        private JObject GetLaunchDetails(string launchId)
        {
            var request = new RestRequest($"/launches/{launchId}", Method.Get);
            var response = _client.Execute(request);

            if (!response.IsSuccessful)
                throw new Exception($"Failed to fetch launch details: {response.ErrorMessage}");

            return JObject.Parse(response.Content);
        }

        private JObject GetPayloadDetails(string payloadId)
        {
            var request = new RestRequest($"/payloads/{payloadId}", Method.Get);
            var response = _client.Execute(request);

            if (!response.IsSuccessful)
                throw new Exception($"Failed to fetch payload details: {response.ErrorMessage}");

            return JObject.Parse(response.Content);
        }
    }
}
