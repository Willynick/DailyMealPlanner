using DailyMealPlanner.Business_Layer.CategoryData;
using DailyMealPlanner.Business_Layer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DailyMealPlanner.Business_Layer.ProductData;
using DailyMealPlanner.Business_Layer.Validator;
using System.Collections.ObjectModel;
using System.Windows;
using DailyMealPlanner.Business_Layer.UserData;
using DailyMealPlanner.Service_Layer;
using DailyMealPlanner.Utility.PDFClass;

namespace DailyMealPlanner.Data_Layer
{
    public class DataBase
    {
        public static StringBuilder log = new StringBuilder();

        private static Db db = null;
        private static DailyRation dailyRation = null;
        private static User user = null;

        public Db getInstance()
        {
            if (db == null)
            {
                ReadData();
            }

            return db;
        }

        public DataBase()
        {

        }

        public DailyRation getDailyRation()
        {
            if (dailyRation == null)
            {
                dailyRation = new DailyRation();
                LoadIconsForMealtime(dailyRation.mealTimes);
            }
            return dailyRation;
        }
        public User getUser()
        {
            if (user == null)
            {
                user = new User();
            }
            return user;
        }

        private void ReadData()
        {
            
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Db));

                db = new Db();

                using (FileStream fs = new FileStream("FoodProducts.xml", FileMode.OpenOrCreate))
                {
                    db = (Db)formatter.Deserialize(fs);
                }

                for (int i = 0; i < db.Category.Count(); i++)
                {
                    db.categories.Add(new CategoryClass(db.Category[i]));

                    for (int j = 0; j < db.Category[i].Product.Count(); j++)
                    {
                        db.categories[i].products.Add(new ProductClass(db.Category[i].Product[j]));
                    }
                }
            }
            catch
            {
                log.AppendFormat("\nError during file deserialization");
            }

            WriteErrorsToLogFile();

            LoadIcons();
        }

        public void LoadIconsForMealtime(ObservableCollection<MealTime> mealTimes)
        {
            foreach (MealTime mealtime in mealTimes)
            {
                switch(mealtime.name)
                {
                    case "Breakfast" :
                        mealtime.IconUri = "/Images/coffee-break.png"; break;
                    case "Lunch":
                        mealtime.IconUri = "/Images/lunch.png"; break;
                    case "Dinner":
                        mealtime.IconUri = "/Images/dinner.png"; break;
                    default :
                        mealtime.IconUri = "/Images/mealtime.png"; break;
                }
            }
        }

        private void LoadIcons()
        {
            foreach (CategoryClass category in db.categories)
            {
                switch(category.name)
                {
                    case "Алкоголь" :
                        category.IconUri = "/Images/cocktail.png"; break;
                    case "Готовые блюда":
                        category.IconUri = "/Images/breakfast.png"; break;
                    case "Грибы":
                        category.IconUri = "/Images/mushrooms.png"; break;
                    case "Каши, крупы":
                        category.IconUri = "/Images/porridge.png"; break;
                    case "Колбаса":
                        category.IconUri = "/Images/salami.png"; break;
                    case "Компоты":
                        category.IconUri = "/Images/compote.png"; break;
                    case "Масла":
                        category.IconUri = "/Images/butter.png"; break;
                    case "Молочные":
                        category.IconUri = "/Images/milk.png"; break;
                    case "Замороженные овощи":
                        category.IconUri = "/Images/vegetables.png"; break;
                    case "Мучное":
                        category.IconUri = "/Images/fast-food.png"; break;
                    case "Мясо":
                        category.IconUri = "/Images/meat.png"; break;
                    case "Овощи":
                        category.IconUri = "/Images/vegetables.png"; break;
                    case "Орехи":
                        category.IconUri = "/Images/nuts.png"; break;
                    case "Прочее":
                        category.IconUri = "/Images/dinner2.png"; break;
                    case "Рыба":
                        category.IconUri = "/Images/fish.png"; break;
                    case "Сладости":
                        category.IconUri = "/Images/cupcake.png"; break;
                    case "Соки":
                        category.IconUri = "/Images/juice.png"; break;
                    case "Супы":
                        category.IconUri = "/Images/soup.png"; break;
                    case "Сухофрукты":
                        category.IconUri = "/Images/dried-fruit.png"; break;
                    case "Сыры":
                        category.IconUri = "/Images/cheese.png"; break;
                    case "Творог":
                        category.IconUri = "/Images/tvorog.png"; break;
                    case "Фрукты":
                        category.IconUri = "/Images/fruits.png"; break;
                    case "Хлеб":
                        category.IconUri = "/Images/bread.png"; break;
                    case "Ягоды":
                        category.IconUri = "/Images/berry.png"; break;
                    case "Яйца":
                        category.IconUri = "/Images/eggs.png"; break;
                }
            }
        }

        public void WriteErrorsToLogFile()
        {
            string writePath = @"LogOfErrors.txt";
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(log.ToString());
            }
        }

        public void GetProductsFile()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Db));

            using (FileStream fs = new FileStream("NewFoodProducts.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, getInstance());
            }
        }

        public void GetMealtimesFile()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(DailyRation));

            using (FileStream fs = new FileStream("MealTimes.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, getDailyRation());
            }
        }

        public void GetPDFFile(Func<double> CalculateNumberOfCalories)
        {
            PDFFileCreator.GetPDFFile(user, dailyRation, CalculateNumberOfCalories);
        }  
    }
}
