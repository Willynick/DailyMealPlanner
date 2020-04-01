using DailyMealPlanner.Service_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DailyMealPlanner
{
    /// <summary>
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            InitializeComponent();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (this.IsVisible && MainWindow.currentWindow.IsVisible)
            {
                e.Cancel = true;
                this.Hide();

                if (!(ProductVM.getInstance().isProductFromMealTime || MainWindow.selectedMealTime != null))
                {
                    ((MainVM)MainWindow.currentWindow.DataContext).CategoriesShowChangesEvent(null, null);
                }
                else
                {
                    ((MainVM)MainWindow.currentWindow.DataContext).MealTimesShowChangesEvent(null, null);
                }
            }
        }
    }
}
