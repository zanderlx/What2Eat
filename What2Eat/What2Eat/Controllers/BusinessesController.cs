using System;
using System.Collections.Generic;
using System.Web.Http;
using What2Eat.Services;

namespace What2Eat.Controllers
{
    public class BusinessesController : ApiController
    {
        // Object that contains data pertaining to the Yelp API response
        YelpAPI Api = new YelpAPI();
        Random rand = new Random();

        // GET: api/Businesses
        public List<string> Get(string latitude, string longitude)
        {
            Api.UserLatitude = latitude;
            Api.UserLongitude = longitude;
            Api.FullApiResponse = Api.ConsumeYelpApi().Result;
            Api.RootObject = Api.GetBusinesses();
            IList<Business> businesses = Api.RootObject.businesses;
            int index = rand.Next(businesses.Count);
            List<string> details = new List<string>();
            details.Add(businesses[index].name);
            details.Add(businesses[index].image_url);
            details.Add(businesses[index].location.display_address[0] + " " + businesses[index].location.display_address[1]);
       
            return details;
        }

        // GET: api/Businesses/5
        // TODO: Fix routing tables to accomodate a random business

        // POST: api/Businesses
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Businesses/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Businesses/5
        public void Delete(int id)
        {
        }
    }
}
