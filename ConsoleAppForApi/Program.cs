using FoodEntityFW;
using FoodEntityFW.Food_entityFramWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppForApi
{
    class Program
    {
        public static HttpClient webClient = new HttpClient();
        private const string URL = "http://localhost:50358/api/foody";

        static void Main(string[] args)
        {
            webClient.BaseAddress = new Uri(URL);
            webClient.DefaultRequestHeaders.Accept.Clear();
            webClient.DefaultRequestHeaders.Accept.Add(
           new MediaTypeWithQualityHeaderValue("application/json"));

            AddRecordes();
            ShowRecordes();
        }

        static async Task<Uri> CreateFoodAsync(Food msg, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/foody", msg);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static public void ShowRecordes()
        {
            HttpResponseMessage httpResponseMessage = webClient.GetAsync("").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var dataObject = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Food>>().Result;
                foreach (var item in dataObject)
                {
                    Console.WriteLine("{0} ",item.Name);
                    Console.WriteLine("{0} ",item.ID);
                    Console.WriteLine("{0} ",item.Grade);
                    Console.WriteLine("{0} ",item.Calories);
                    Console.WriteLine("{0} ",item.Ingridients);
                }
                Console.ReadLine();
            }
         else
         {
              Console.WriteLine("{0} ({1})", (int)httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase);
         }
        }

        static public void AddRecordes()
        {
            HttpClient client_post = new HttpClient();

            client_post.BaseAddress = new Uri(URL);
            client_post.DefaultRequestHeaders.Accept.Clear();
            client_post.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Food food = new Food
            {
                ID = 10,
                Name = "Shrimps",
                Calories = 300,
                Ingridients = "Shrimps, Butter",
                Grade = 10,
            };

            var response_post = client_post.PostAsJsonAsync(
                 "", food).Result;

            Console.WriteLine(response_post);
        }

    }
}
