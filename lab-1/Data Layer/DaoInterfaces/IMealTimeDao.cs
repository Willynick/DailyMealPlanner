using DailyMealPlanner.Business_Layer;
using DailyMealPlanner.Business_Layer.ProductData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Data_Layer
{
    interface IMealTimeDao
    {
        void LoadIcons(ObservableCollection<MealTime> mealTimes);
        void AddProduct(MealTime mealtime, ProductClass product);
        void RemoveProduct(ProductClass product);
        ObservableCollection<MealTime> GetCollection();
        bool Add(MealTime mealtime);
        bool Edit(MealTime oldMealTime, MealTime newMealTime);
        void Remove(MealTime mealtime);
        double CalculateNumberOfCalories();
        void GetMealtimesFile();
    }
}
