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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppForFoodProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DataGrid dg;
        MonitorWindow mw;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel vm = new ViewModel();
            this.DataContext = vm;
            DataGrid2.ItemsSource = vm.NewFoodList;
            dg = DataGrid2;

            mw = new MonitorWindow(this);
            //mw.FoodNameTB.Text = vm.FoodNameTB;
            //mw.FoodCaloriesTB.Text =  vm.FoodCaloriesTB.ToString();
            //mw.FoodIngridiantsTB.Text = vm.FoodIngridiantsTB;
            //mw.FoodGradeTB.Text = vm.FoodGradeTB;

            mw.FoodNameTB.Text = vm.FoodObj.Name;
            mw.FoodGradeTB.Text = vm.FoodObj.Grade.ToString();
            mw.FoodIngridiantsTB.Text = vm.FoodObj.Ingridients;
            mw.FoodCaloriesTB.Text = vm.FoodObj.Calories.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            mw.Show();
        }
    }
}
