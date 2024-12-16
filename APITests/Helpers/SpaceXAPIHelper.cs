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
            Console.WriteLine($"Fetching Rocket ID for Rocket Name: {name}");
            var rockets = GetRockets();
            var selectedRocket = rockets.FirstOrDefault(r => r["name"].ToString() == name);

            if (selectedRocket == null)
            {
                Console.WriteLine($"Rocket '{name}' not found.");
                throw new Exception($"Rocket '{name}' not found.");
            }

            var rocketId = selectedRocket["id"].ToString();
            Console.WriteLine($"Rocket ID for '{name}': {rocketId}");
            return rocketId;
        }

        public string GetSpecificRocketLaunch(string rocketId, int launchNumber)
        {
            Console.WriteLine($"Fetching Launch #{launchNumber} for Rocket ID: {rocketId}");
            var launches = GetLaunches();
            var rocketLaunches = launches
                .Where(l => l["rocket"].ToString() == rocketId)
                .OrderBy(l => DateTime.Parse(l["date_utc"].ToString()))
                .ToList();

            if (rocketLaunches.Count < launchNumber)
            {
                Console.WriteLine($"Less than {launchNumber} launches found for Rocket ID: {rocketId}");
                throw new Exception($"Less than {launchNumber} launches found for Rocket ID: {rocketId}");
            }

            var launchId = rocketLaunches[launchNumber - 1]["id"].ToString();
            Console.WriteLine($"Launch ID for Launch #{launchNumber}: {launchId}");
            return launchId;
        }

        private JArray GetRockets()
        {
            var request = new RestRequest("/rockets", Method.Get);
            Console.WriteLine($"Sending GET request to {BaseUrl}/rockets");

            var response = _client.Execute(request);
            LogResponse(response);

            if (!response.IsSuccessful)
                throw new Exception($"Failed to fetch rockets: {response.ErrorMessage}");

            return JArray.Parse(response.Content);
        }

        public string[] GetPayloadIdsForASpecificLaunch(string launchId)
        {
            Console.WriteLine($"Fetching Payload IDs for Launch ID: {launchId}");
            var launchDetails = GetLaunchDetails(launchId);

            if (launchDetails["payloads"] == null)
            {
                Console.WriteLine($"No payloads found for Launch ID: {launchId}");
                throw new Exception($"No payloads found for Launch ID: {launchId}");
            }

            var payloadIds = launchDetails["payloads"].ToObject<string[]>();
            Console.WriteLine($"Payload IDs for Launch ID {launchId}: {string.Join(", ", payloadIds)}");
            return payloadIds;
        }

        public string GetPayloadName(string payloadId)
        {
            Console.WriteLine($"Fetching Payload Name for Payload ID: {payloadId}");
            var payloadDetails = GetPayloadDetails(payloadId);

            var payloadName = payloadDetails["name"].ToString();
            Console.WriteLine($"Payload Name for ID {payloadId}: {payloadName}");
            return payloadName;
        }

        private JArray GetLaunches()
        {
            var request = new RestRequest("/launches", Method.Get);
            Console.WriteLine($"Sending GET request to {BaseUrl}/launches");

            var response = _client.Execute(request);
            LogResponse(response);

            if (!response.IsSuccessful)
                throw new Exception($"Failed to fetch launches: {response.ErrorMessage}");

            return JArray.Parse(response.Content);
        }

        private JObject GetLaunchDetails(string launchId)
        {
            var request = new RestRequest($"/launches/{launchId}", Method.Get);
            Console.WriteLine($"Sending GET request to {BaseUrl}/launches/{launchId}");

            var response = _client.Execute(request);
            LogResponse(response);

            if (!response.IsSuccessful)
                throw new Exception($"Failed to fetch launch details: {response.ErrorMessage}");

            return JObject.Parse(response.Content);
        }

        private JObject GetPayloadDetails(string payloadId)
        {
            var request = new RestRequest($"/payloads/{payloadId}", Method.Get);
            Console.WriteLine($"Sending GET request to {BaseUrl}/payloads/{payloadId}");

            var response = _client.Execute(request);
            LogResponse(response);

            if (!response.IsSuccessful)
                throw new Exception($"Failed to fetch payload details: {response.ErrorMessage}");

            return JObject.Parse(response.Content);
        }

        private void LogResponse(RestResponse response)
        {
            Console.WriteLine($"Response Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content: {response.Content}");
        }
    }
}
