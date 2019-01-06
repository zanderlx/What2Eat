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

        // RootObject containss business array
        public RootObject RootObject { get; set; }
        public string FullApiResponse { get; set; }

        public YelpAPI()
        {
            // Stores HttpResponseMessage as string
            FullApiResponse = ConsumeYelpApi().Result;
            // Stores all businesses retrieved from response
            RootObject = GetBusinesses();
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
                // Sets the base address of the client the endpoint we want
                BaseAddress = new Uri(URL)
            };
            
            // Add authorization header needed for Yelp API
            client.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", AuthData.Key);

            HttpResponseMessage response = client.GetAsync("?location=cerritos,ca").Result;
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