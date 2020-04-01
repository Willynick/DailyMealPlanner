using DailyMealPlanner.Business_Layer.CategoryData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Data_Layer
{
    interface ICategoryDao
    {
        bool Add(CategoryInfo selectedCategory);
        void Remove(CategoryClass selectedCategory);
        ObservableCollection<CategoryClass> GetCollection();
        ObservableCollection<CategoryClass> SearchInfo(string info);
        bool Edit(CategoryClass oldCategory, CategoryInfo newCategory);
        void GetProductsFile();
    }
}
