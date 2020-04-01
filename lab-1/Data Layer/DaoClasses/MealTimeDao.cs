using DailyMealPlanner.Business_Layer;
using DailyMealPlanner.Business_Layer.CategoryData;
using DailyMealPlanner.Business_Layer.ProductData;
using DailyMealPlanner.Business_Layer.Validator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyMealPlanner.Data_Layer
{
    public class MealTimeDao : IMealTimeDao
    {
        DataBase dataBase = new DataBase();

        public void LoadIcons(ObservableCollection<MealTime> mealTimes)
        {
            dataBase.LoadIconsForMealtime(mealTimes);
        }

        public bool Add(MealTime mealtime)
        {
            MealTime mt = (MealTime)mealtime.Clone();
            mt.ValidateAllInformation();

            dataBase.getDailyRation().mealTimes.Add(mt);

            return mt.mealTimeValidator.ShowErrorMessages();
        }

        public void Remove(MealTime mealtime)
        {
            dataBase.getDailyRation().mealTimes.Remove(mealtime);
        }

        public bool Edit(MealTime oldMealTime, MealTime newMealTime)
        {
            ObservableCollection<MealTime> mealTimes = dataBase.getDailyRation().mealTimes;

            for (int i = 0; i < mealTimes.Count; i++)
            {
                if (mealTimes[i] == oldMealTime)
                {
                    MealTime mealTime = (MealTime)mealTimes[i].Clone();
                    mealTime.ValidateAllInformation();

                    mealTimes[i] = mealTime;

                    return mealTime.mealTimeValidator.ShowErrorMessages();
                }
            }

            return true;
        }

        public void AddProduct(MealTime mealtime, ProductClass product)
        {
            ProductClass newProduct = (ProductClass)product.Clone();
            newProduct.selectedMealTime = mealtime.name;
            mealtime.products.Add(newProduct);           
        }

        public void RemoveProduct(ProductClass product)
        {
            ObservableCollection<MealTime> mealtimes = dataBase.getDailyRation().mealTimes;

            bool isRemoved = false;

            for (int i = 0; i < mealtimes.Count; i++)
            {
                if (product.selectedMealTime == mealtimes[i].name)
                {
                    for (int j = 0; j < mealtimes[i].products.Count; j++)
                    {
                        if (mealtimes[i].products[j] == product)
                        {
                            mealtimes[i].products.Remove(product);
                            isRemoved = true;
                            break;
                        }
                    }
                    if (isRemoved == false)
                    {
                        break;
                    }
                }
            }
        }

        public double CalculateNumberOfCalories()
        {
            ObservableCollection<MealTime> mealtimes = dataBase.getDailyRation().mealTimes;

            double numberOfCalories = 0.0;

            for (int i = 0; i < mealtimes.Count; i++)
            {
                for (int j = 0; j < mealtimes[i].products.Count; j++)
                {
                    numberOfCalories += mealtimes[i].products[j].Calories;
                }
            }

            return numberOfCalories;
        }

        public ObservableCollection<MealTime> GetCollection()
        {
            return dataBase.getDailyRation().mealTimes;
        }

        public void GetMealtimesFile()
        {
            dataBase.GetMealtimesFile();
        }
    }
}
