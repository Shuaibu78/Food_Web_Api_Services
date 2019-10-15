using FoodEntityFW.Food_entityFramWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppForFoodProj
{
    /// <summary>
    /// Interaction logic for MonitorWindow.xaml
    /// </summary>
    public partial class MonitorWindow : Window
    {
        private WpfFood f = new WpfFood();
        private MainWindow mw;
        ViewModel viewModel = new ViewModel();

        public MonitorWindow(MainWindow mw)
        {
            DataContext = this.viewModel;
            InitializeComponent();

            this.mw = mw;

            //this.FoodNameTB.Text = viewModel.FoodObj.Name;
            //this.FoodGradeTB.Text = viewModel.FoodObj.Grade.ToString();
            //this.FoodIngridiantsTB.Text = viewModel.FoodObj.Ingridients;
            //this.FoodCaloriesTB.Text = viewModel.FoodObj.Calories.ToString();
        }

        //public WpfFood GetItem()
        //{     
        //   return viewModel.AddNewFoodWindow();
        //}
    }
}
