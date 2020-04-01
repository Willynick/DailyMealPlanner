using DailyMealPlanner.Business_Layer;
using DailyMealPlanner.Business_Layer.CategoryData;
using DailyMealPlanner.Business_Layer.ProductData;
using DailyMealPlanner.Data_Layer;
using DailyMealPlanner.Service_Layer;
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

namespace DailyMealPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ReadingData readingData = new ReadingData();

            currentWindow = this;
        }

        public static MainWindow currentWindow;

        public static ProductClass previousSelectedProduct = null;
        public static ProductClass previousSelectedProductFromMealTime = null;
        public static CategoryClass previousSelectedCategory = null;
        public static MealTime previousSelectedMealTime = null;

        public static CategoryClass selectedCategory = null;
        public static ProductClass selectedProduct = null;
        public static MealTime selectedMealTime = null;
        public static ProductClass selectedProductFromMealTime = null;

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tree.Items.Count >= 0)
            {
                var tree = sender as TreeView;
                if (tree.SelectedItem is CategoryClass)
                {
                    selectedCategory = tree.SelectedItem as CategoryClass;
                }
                else if (tree.SelectedItem is ProductClass)
                {
                    if (((TreeView)sender).Name == "tree")
                    {
                        selectedProduct = tree.SelectedItem as ProductClass;
                    }
                    else
                    {
                        selectedProductFromMealTime = tree.SelectedItem as ProductClass;

                        ((MainVM)DataContext).WeightSelectedProduct = selectedProductFromMealTime.Gramms;
                        ((MainVM)DataContext).UpdateCharacteristicsOfProduct();
                    }
                }
                else
                {
                    selectedMealTime = tree.SelectedItem as MealTime;
                }
            }
        }
    }
}
