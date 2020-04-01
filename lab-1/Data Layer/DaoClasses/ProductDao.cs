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
    public class ProductDao : IProductDao
    {
        DataBase dataBase = new DataBase();

        public bool Add(CategoryClass selectedCategory, ProductInfo product)
        {
            ObservableCollection<CategoryClass> category = dataBase.getInstance().categories;

            ProductClass newProduct = new ProductClass(product);

            for (int i = 0; i < category.Count; i++)
            {
                if (category[i] == selectedCategory)
                {
                    category[i].products.Add(newProduct);
                }
            }

            return newProduct.productValidator.ShowErrorMessages();
        }
        public void Remove(ProductClass selectedProduct)
        {
            ObservableCollection<CategoryClass> category = dataBase.getInstance().categories;

            for (int i = 0; i < category.Count; i++)
            {
                for (int j = 0; j < category[i].products.Count; j++)
                {
                    if (category[i].products[j] == selectedProduct)
                    {
                        category[i].products.Remove(selectedProduct);
                    }
                }
            }
        }

        public bool Edit(ProductClass oldProduct, ProductInfo newProduct)
        {
            ObservableCollection<CategoryClass> categories = dataBase.getInstance().categories;

            for (int i = 0; i < categories.Count; i++)
            {             
                for (int j = 0; j < categories[i].products.Count; j++)
                {
                    if (categories[i].products[j] == oldProduct)
                    {
                        ProductClass product = (ProductClass)categories[i].products[j].Clone();
                        product.ValidateAllInformation(newProduct);

                        categories[i].products[j] = product;

                        return product.productValidator.ShowErrorMessages();
                    }
                }
            }

            return true;
        }

        public bool EditFromMealTime(ProductClass oldProduct, ProductInfo newProduct)
        {
            ObservableCollection<MealTime> mealtimes = dataBase.getDailyRation().mealTimes;

            for (int i = 0; i < mealtimes.Count; i++)
            {
                for (int j = 0; j < mealtimes[i].products.Count; j++)
                {
                    if (mealtimes[i].products[j] == oldProduct)
                    {
                        ProductClass product = (ProductClass)mealtimes[i].products[j].Clone();
                        product.ValidateAllInformation(newProduct);

                        mealtimes[i].products[j] = product;

                        return product.productValidator.ShowErrorMessages();
                    }
                }
            }

            return true;
        }

        public ObservableCollection<ProductClass> GetCollection(int indexOfCategory)
        {
            return dataBase.getInstance().categories[indexOfCategory].products;
        }
    }
}
