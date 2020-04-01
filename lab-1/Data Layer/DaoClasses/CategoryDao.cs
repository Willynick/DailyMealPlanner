using DailyMealPlanner.Business_Layer.CategoryData;
using DailyMealPlanner.Business_Layer.Validator;
using DailyMealPlanner.Service_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyMealPlanner.Data_Layer
{
    public class CategoryDao : ICategoryDao
    {
        DataBase dataBase = new DataBase();

        public bool Add(CategoryInfo selectedCategory)
        {
            CategoryClass category = new CategoryClass(selectedCategory);
            dataBase.getInstance().categories.Add(category);

            return category.categoryValidator.ShowErrorMessages();
        }

        public void Remove(CategoryClass selectedCategory)
        {
            dataBase.getInstance().categories.Remove(selectedCategory);
        }

        public bool Edit(CategoryClass oldCategory, CategoryInfo newCategory)
        {
            ObservableCollection<CategoryClass> categories = dataBase.getInstance().categories;

            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i] == oldCategory)
                {
                    CategoryClass category = (CategoryClass)dataBase.getInstance().categories[i].Clone();
                    category.ValidateAllInformation(newCategory);

                    dataBase.getInstance().categories[i] = category;

                    return category.categoryValidator.ShowErrorMessages();
                }
            }

            return true;
        }

        public ObservableCollection<CategoryClass> GetCollection()
        {
            return dataBase.getInstance().categories;
        }

        public ObservableCollection<CategoryClass> SearchInfo(string info)
        {
            ObservableCollection<CategoryClass> categories = dataBase.getInstance().categories;

            ObservableCollection<CategoryClass> newCategories = new ObservableCollection<CategoryClass>();

            newCategories.Add(new CategoryClass("Found Products", "", true));

            for (int i = 0; i < categories.Count; i++)
            {
                if (((categories[i].name).ToLower()).Contains(info.ToLower()))
                {
                    newCategories.Add(categories[i]);
                }

                for (int j = 0; j < categories[i].products.Count; j++)
                {
                    if ((categories[i].products[j].Name).ToLower().Contains(info.ToLower()))
                    {
                        newCategories[0].products.Add(categories[i].products[j]);
                    }
                }
            }
            return newCategories;
        }

        public void GetProductsFile()
        {
            dataBase.GetProductsFile();
        }
    }
}
