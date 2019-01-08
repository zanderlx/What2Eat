using System;
using System.Net.Http;
using What2Eat.Models;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace What2Eat.Services
{
    public class YelpAPI
    {
        // Endpoint for searching for businesses using Yelp API
        private const string URL = "https://api.yelp.com/v3/businesses/search";
        readonly Random rand = new Random();

        // RootObject contains business array
        public RootObject RootObject { get; set; }
        public string FullApiResponse { get; set; }
        public string UserLatitude { get; set; }
        public string UserLongitude { get; set; }

        public YelpAPI()
        {
            // Stores all businesses retrieved from response
            RootObject = null;
            // Stores HttpResponseMessage as string
            FullApiResponse = "";
            // User position is not found yet
            UserLatitude = "";
            UserLongitude = "";
        }

        /// <summary>
        /// Uses an HttpClient to send a GET request to the Yelp API.
        /// Asynchronously waits for the request before continuing.
        /// </summary>
        /// <returns>Response from the api</returns>
        public async Task<string> ConsumeYelpApi()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(URL)
            };
        
            // Add authorization header needed for Yelp API
            client
                .DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", AuthData.Key);

            HttpResponseMessage response = client.GetAsync("?latitude="+UserLatitude+"&longitude="+UserLongitude).Result;
            // Return response only if 200 status code
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();     
            else
                return null;
        }

        /// <summary>
        /// Deserializes the api response which converts JSON into .NET objects.
        /// RootObject holds all the businesses in an array along with their details.
        /// </summary>
        /// <returns>An object holding all businesses and their details</returns>
        public RootObject GetBusinesses()
        {
            // Uses custom business classes to deserialize the json response
            return JsonConvert.DeserializeObject<RootObject>(FullApiResponse);
        }

        // TODO: Implement retrieval of random business
    }
}