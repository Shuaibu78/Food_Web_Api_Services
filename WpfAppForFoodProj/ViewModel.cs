using FoodEntityFW;
using FoodEntityFW.Food_entityFramWork;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppForFoodProj
{
    class ViewModel : INotifyPropertyChanged
    {
        DataGrid dg;

        private ObservableCollection<WpfFood> newFoodList;
        public ObservableCollection<WpfFood> NewFoodList
        {
            get
            {
                return newFoodList;
            }
            set
            {
                newFoodList = value;
                OnPropertyChanged("newFoodList");
            }
        }

        public static HttpClient webClient2 = new HttpClient();
        private const string URL = "http://localhost:50358/api/foody";

        public DelegateCommand MyDelegate { get; set; }
        public DelegateCommand MyAddNewFoodDelegate { get; set; }

        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private string foodnametb;
        public string FoodNameTB
        {
            get
            {
                return foodnametb;
            }
            set
            {
                foodnametb = value;
                OnPropertyChanged("foodnametb");
            }
        }

        private int foodcaloriestb;
        public int FoodCaloriesTB
        {
            get
            {
                return foodcaloriestb;
            }
            set
            {
                foodcaloriestb = value;
                OnPropertyChanged("foodcaloriestb");
            }
        }

        private string foodingridiantstb;
        public string FoodIngridiantsTB
        {
            get
            {
                return foodingridiantstb;
            }
            set
            {
                foodingridiantstb = value;
                OnPropertyChanged("foodingridiantstb");
            }
        }

        private string foodgradetb;
        public string FoodGradeTB
        {
            get
            {
                return foodgradetb;
            }
            set
            {
                foodgradetb = value;
                OnPropertyChanged("foodgradetb");
            }
        }

        private WpfFood foodObj;
        public WpfFood FoodObj
        {
            get
            {
                return foodObj;
            }
            set
            {
                foodObj = value;
                OnPropertyChanged("FoodObj");
            }
        }
        public ViewModel()
        {
            newFoodList = new ObservableCollection<WpfFood>();
            foodObj = new WpfFood();
            MyDelegate = new DelegateCommand(Execute, CanExecute);
            MyAddNewFoodDelegate = new DelegateCommand(AddExecute, AddCanExecute);

            Task.Run(() =>
            {
                while (true)
                {
                   MyDelegate.RaiseCanExecuteChanged(); // go check the enable/disable
                   MyAddNewFoodDelegate.RaiseCanExecuteChanged(); 
                    Thread.Sleep(500);
                }            
            });
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute()
        {
            Print();
            OnPropertyChanged("newFoodList");
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private bool AddCanExecute()
        {
            return true;
        }

        private void AddExecute()
        {
            if (foodObj != null)
            {
                Task.Run(() =>
                {
                    AddNewFoodWindow(foodObj);
                    OnPropertyChanged("AddNewFood");
                    MessageBox.Show($"{foodObj.Name} Was Posted Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                });
            }
        }

        public async void AddNewFoodWindow(WpfFood foodObj)
        {
            string query = "/post";
            WpfFood returnFood = new WpfFood();
            HttpClient client_post = new HttpClient();
            client_post.BaseAddress = new Uri(URL + query);
            //client_post.DefaultRequestHeaders.Accept.Clear();
            client_post.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response_post =await client_post.PostAsJsonAsync(client_post.BaseAddress, foodObj);

            HttpResponseMessage response = client_post.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects =  response.Content.ReadAsAsync<WpfFood>().Result;
                NewFoodList.Add(dataObjects);
                returnFood = dataObjects;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client_post.Dispose();
            //return returnFood;
        }

        public void Print()
        {
            webClient2.BaseAddress = new Uri(URL);
            webClient2.DefaultRequestHeaders.Accept.Clear();
            webClient2.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = webClient2.GetAsync("").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var dataObject = httpResponseMessage.Content.ReadAsAsync<IEnumerable<WpfFood>>().Result;
                foreach (var item in dataObject)
                {
                    WpfFood food = new WpfFood();
                    newFoodList.Add(item);
                }
            }
        }
    }


    public class WpfFood : INotifyPropertyChanged
    {

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }

        private int id;
        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged("ID");
            }
        }

        private Nullable<int> calories;
        public Nullable<int> Calories
        {
            get
            {
                return this.calories;
            }
            set
            {
                this.calories = value;
                OnPropertyChanged("Calories");
            }
        }

        private Nullable<int> grade;
        public Nullable<int> Grade
        {
            get
            {
                return this.grade;
            }
            set
            {
                this.grade = value;
                OnPropertyChanged("Grade");
            }
        }

        private string ingridients;
        public string Ingridients
        {
            get
            {
                return this.ingridients;
            }
            set
            {
                this.ingridients = value;
                OnPropertyChanged("Ingridients");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"Food name {Name}, {ID}, {Ingridients}, {Grade}";
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
