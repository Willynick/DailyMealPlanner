using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Utility
{
    public class SelectedIndexClass
    {
        public static bool IsSelectedItem()
        {
            if (MainWindow.selectedCategory == null && MainWindow.selectedProduct == null && MainWindow.previousSelectedCategory == null && MainWindow.previousSelectedProduct == null)
            {
                return true;
            }
            return false;
        }

        public static bool IsSelectedMealTimeItem()
        {
            if (MainWindow.selectedMealTime == null && MainWindow.selectedProductFromMealTime == null && MainWindow.previousSelectedMealTime == null && MainWindow.previousSelectedProductFromMealTime == null)
            {
                return true;
            }
            return false;
        }

        public static bool IsSelectedProduct()
        {
            if ((MainWindow.selectedProduct != null || MainWindow.previousSelectedProduct != null) 
                && (((!(MainWindow.selectedProduct == MainWindow.previousSelectedProduct)) && MainWindow.selectedProduct != null) || MainWindow.selectedCategory == null))
            {

                if (MainWindow.selectedProduct == null)
                {
                    MainWindow.selectedProduct = MainWindow.previousSelectedProduct;
                }
                return true;
            }
            else
            {
                if (MainWindow.selectedCategory == null)
                {
                    MainWindow.selectedCategory = MainWindow.previousSelectedCategory;
                }
                return false;
            }
        }

        public static bool IsSelectedProductFromMealTime()
        {
            if ((MainWindow.selectedProductFromMealTime != null || MainWindow.previousSelectedProductFromMealTime != null) 
                && (((!(MainWindow.selectedProductFromMealTime == MainWindow.previousSelectedProductFromMealTime)) && MainWindow.selectedProductFromMealTime != null) || MainWindow.selectedMealTime == null))
            {

                if (MainWindow.selectedProductFromMealTime == null)
                {
                    MainWindow.selectedProductFromMealTime = MainWindow.previousSelectedProductFromMealTime;
                }
                return true;
            }
            else
            {
                if (MainWindow.selectedMealTime == null)
                {
                    MainWindow.selectedMealTime = MainWindow.previousSelectedMealTime;
                }
                return false;
            }
        }
    }
}
