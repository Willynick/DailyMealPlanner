using DailyMealPlanner.Business_Layer.CategoryData;
using DailyMealPlanner.Business_Layer.ProductData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Data_Layer
{
    interface IProductDao
    {
        bool Add(CategoryClass selectedCategory, ProductInfo product);
        void Remove(ProductClass selectedProduct);
        ObservableCollection<ProductClass> GetCollection(int indexOfCategory);
        bool Edit(ProductClass oldProduct, ProductInfo newProduct);
        bool EditFromMealTime(ProductClass oldProduct, ProductInfo newProduct);
    }
}
