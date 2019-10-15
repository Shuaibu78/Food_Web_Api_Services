using FoodEntityFW;
using FoodEntityFW.Food_entityFramWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiFood.Controllers
{
    public class FoodyController : ApiController
    {
        // GET api/messages
        public HttpResponseMessage Get()
        {
            List<Food> foods = new List<Food>();
            DAOManager dAOManager = new DAOManager();

            foods = dAOManager.Foods();

            if (foods.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, foods);
            return msg;
        }

        // GET api/messages/5
        [HttpGet]
        public HttpResponseMessage Get([FromUri] int id)
        {
            DAOManager dAOManager = new DAOManager();
            List<Food> foods = new List<Food>();
            foods = dAOManager.Foods();

            Food result = foods.FirstOrDefault(m => m.ID == id);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, result);
            return msg;
        }

        [Route("api/messages/byfoodName/{name}")]
        [HttpGet]
        public IEnumerable<Food> getbyFoodname([FromUri] string name)
        {
            DAOManager dAOManager = new DAOManager();
            List<Food> foods = new List<Food>();
            foods = dAOManager.Foods();           

            IEnumerable<Food> result = foods.Where(m => m.Name.ToUpper().Contains(name.ToUpper()));
            return result;
        }

        [HttpPost]
        // POST api/messages
        public HttpResponseMessage Post([FromBody]Food food)
        {
            DAOManager dAOManager = new DAOManager();
            if (food == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            dAOManager.AddToDb(food);
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [HttpPut]
        // PUT api/messages/5
        public HttpResponseMessage Put(int id, [FromBody]Food food)
        {
            DAOManager dAOManager = new DAOManager();
            List<Food> foods = new List<Food>();
            foods = dAOManager.Foods();
            if (food == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            Food result = foods.FirstOrDefault(m => m.ID == id);
            dAOManager.UpdateDB(food, id);
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [HttpDelete]
        // DELETE api/messages/5
        public HttpResponseMessage Delete(int id)
        {
            DAOManager dAOManager = new DAOManager();
            List<Food> foods = new List<Food>();
            foods = dAOManager.Foods();

            Food result = foods.FirstOrDefault(m => m.ID == id);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            dAOManager.RemoveFromDB(id);
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // query string
        // .../api/foody/search?name=p&cal=30
        [Route("api/foody/search")]
        [HttpGet]
        public IEnumerable<Food> getbyfilter(string name = "", int cal = 0)
        {
            DAOManager dAOManager = new DAOManager();
            List<Food> foods = new List<Food>();
            foods = dAOManager.Foods();

            if (name == "" && cal != 0)
                return foods.Where(m => m.Calories > cal);
            if (name != "" && cal == 0)
                return foods.Where(m => m.Name.ToUpper() == name.ToUpper());
            return foods.Where(m => m.Calories > cal && m.Name.ToUpper().Contains(name.ToUpper()));
        }

        // .../api/foody/biggerthanCal?2500
        [Route("api/foody/biggerthanCal")]
        [HttpGet]
        public IEnumerable<Food> biggerthanCal(int cal)
        {
            DAOManager dAOManager = new DAOManager();
            List<Food> foods = new List<Food>();
            foods = dAOManager.Foods();

            // how do i add condition that checks if cal = 0 or smaller? i cannot add HttpResponse - because function returns IEnumerable
            return foods.Where(m => m.Calories > cal);
        }
    }
}
