using DailyMealPlanner.Business_Layer;
using DailyMealPlanner.Business_Layer.CategoryData;
using DailyMealPlanner.Business_Layer.ProductData;
using DailyMealPlanner.Business_Layer.UserData;
using DailyMealPlanner.Data_Layer;
using DailyMealPlanner.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Data_Layer
{
    public class DataAccess
    {
        private ICategoryDao categoryDao = new CategoryDao();
        private IProductDao productDao = new ProductDao();
        private IMealTimeDao mealTimeDao = new MealTimeDao();
        private IUserDao userDao = new UserDao();

        public bool AddCategory(CategoryInfo category)
        {
            return categoryDao.Add(category);
        }

        public void RemoveCategory(CategoryClass category)
        {
            categoryDao.Remove(category);
        }

        public bool EditCategory(CategoryClass oldCategory, CategoryInfo newCategory)
        {
            return categoryDao.Edit(oldCategory, newCategory);
        }

        public bool AddProduct(CategoryClass category, ProductInfo product)
        {
            return productDao.Add(category, product);
        }

        public void RemoveProduct(ProductClass product)
        {
            productDao.Remove(product);
        }

        public bool EditProduct(ProductClass oldProduct, ProductInfo newProduct)
        {
            return productDao.Edit(oldProduct, newProduct);
        }

        public ObservableCollection<CategoryClass> GetCategories()
        {
            return categoryDao.GetCollection();
        }

        public ObservableCollection<ProductClass> GetProducts(int indexOfCategory)
        {
            return productDao.GetCollection(indexOfCategory);
        }
        
        public void LoadIconsForMealtime(ObservableCollection<MealTime> mealTimes)
        {
            mealTimeDao.LoadIcons(mealTimes);
        }

        public ObservableCollection<CategoryClass> SearchInfo(string info)
        {
            return categoryDao.SearchInfo(info);
        }

        public bool AddMealTime(MealTime mealtime)
        {
            return mealTimeDao.Add(mealtime);
        }

        public void RemoveMealTime(MealTime mealtime)
        {
            mealTimeDao.Remove(mealtime);
        }

        public bool EditMealTime(MealTime oldMealTime, MealTime newMealTime)
        {
            return mealTimeDao.Edit(oldMealTime, newMealTime);
        }

        public void AddProductToMealTime(MealTime mealtime, ProductClass product)
        {
            mealTimeDao.AddProduct(mealtime, product);
        }

        public bool EditProductFromMealTime(ProductClass oldProduct, ProductInfo newProduct)
        {
            return productDao.EditFromMealTime(oldProduct, newProduct);
        }

        public void RemoveProductFromMealTime(ProductClass product)
        {
            mealTimeDao.RemoveProduct(product);
        }

        public double CalculateNumberOfCalories()
        {
            return mealTimeDao.CalculateNumberOfCalories();
        }

        public ObservableCollection<MealTime> GetMealTimes()
        {
            return mealTimeDao.GetCollection();
        }

        public void GetProductsFile()
        {
            categoryDao.GetProductsFile();
        }

        public void GetMealtimesFile()
        {
            mealTimeDao.GetMealtimesFile();
        }

        public void GetPDFFile()
        {
            DataBase dataBase = new DataBase();
            dataBase.GetPDFFile(mealTimeDao.CalculateNumberOfCalories);
        }

        public void SetWeight(double weight)
        {
            userDao.SetWeight(weight);
        }

        public void SetHeight(double height)
        {
            userDao.SetHeight(height);
        }

        public void SetAge(int age)
        {
            userDao.SetAge(age);
        }

        public void SetDailyActivity(DailyActivity dailyActivity)
        {
            userDao.SetDailyActivity(dailyActivity);
        }

        public User GetUser()
        {
            return userDao.GetUser();
        }
    }
}
