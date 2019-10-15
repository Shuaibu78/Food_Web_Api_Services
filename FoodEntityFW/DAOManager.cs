using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodEntityFW
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Food_entityFramWork
    {
        public class DAOManager
        {
            public List<Food> Foods()
            {
                using (FoodDBEntities FoodEntiites = new FoodDBEntities())
                {
                    List<Food> foods = (from c in FoodEntiites.Foods
                                        select c).ToList();
                    // List<Food> countriesMS = FoodEntiites.Food.ToList();
                    return foods;
                }
            }

            public void AddToDb(Food f)
            {
                using (FoodDBEntities FoodEntiites = new FoodDBEntities())
                {
                    FoodEntiites.Foods.Add(f);
                    FoodEntiites.SaveChanges();
                }
            }

            public void UpdateDB(Food food, int id)
            {
                using (FoodDBEntities FoodEntiites = new FoodDBEntities())
                {
                    Food result = FoodEntiites.Foods.SingleOrDefault(b => b.ID == id);
                    if (result != null)
                    {
                        result.Calories = food.Calories;
                        result.Grade = food.Grade;
                        result.ID = food.ID;
                        result.Ingridients = food.Ingridients;
                        result.Name = food.Name;
                        FoodEntiites.SaveChanges();
                    }
                }
            }

            public void RemoveFromDB(int id)
            {
                using (FoodDBEntities FoodEntiites = new FoodDBEntities())
                {
                    Food result = FoodEntiites.Foods.FirstOrDefault(b => b.ID == id);
                    if (result != null)
                        FoodEntiites.Foods.Remove(result);
                    FoodEntiites.SaveChanges();

                }
            }
        }
    }

}
