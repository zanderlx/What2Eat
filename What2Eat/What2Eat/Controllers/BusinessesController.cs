using System.Collections.Generic;
using System.Web.Http;
using What2Eat.Services;

namespace What2Eat.Controllers
{
    public class BusinessesController : ApiController
    {
        // Object that contains data pertaining to the Yelp API response
        YelpAPI Api = new YelpAPI();

        // GET: api/Businesses
        public IEnumerable<Business> Get()
        {
            return Api.RootObject.businesses;
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
