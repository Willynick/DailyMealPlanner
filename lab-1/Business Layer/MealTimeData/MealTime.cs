using DailyMealPlanner.Business_Layer.ProductData;
using DailyMealPlanner.Business_Layer.Validator;
using DailyMealPlanner.Data_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace DailyMealPlanner.Business_Layer
{
    [Serializable]
    public class MealTime : ICloneable
    {
        [XmlIgnore]
        public MealTimeValidator mealTimeValidator { get; set; } = new MealTimeValidator();

        public MealTime()
        {

        }

        public MealTime(string name)
        {
            this.name = name;
            ValidateAllInformation();
        }

        public object Clone()
        {
            ObservableCollection<ProductClass> copyProducts = new ObservableCollection<ProductClass>();

            for (int i = 0; i < this.products.Count; i++)
            {
                copyProducts.Add((ProductClass)this.products[i].Clone());
            }
            return new MealTime
            {
                name = this.name,
                products = copyProducts,
                IconUri = this.IconUri
            };
        }

        public void ValidateAllInformation()
        {

            bool isParamsValid = mealTimeValidator.Validate(this);
        }

        public string name { get; set; }

        [XmlElement]
        public ObservableCollection<ProductClass> products { get; set; } = new ObservableCollection<ProductClass>();

        [XmlIgnore]
        public string IconUri { get; set; } = "/Images/mealtime.png";

        public void ChangeProductWeight(double gramms, int index)
        {
            ProductValidator productValidator = new ProductValidator();
            string error = "";
            bool isValid = productValidator.ValidateInfo(gramms, "Gramms", out error);

            if (isValid == false)
            {
                productValidator.Errors.Add(new ValidationResult("Gramms", error));                
            }

            products[index].Gramms = gramms;
        }

        public void AddProduct(ProductClass product)
        {
            products.Add(product);
        }
        public void RemoveProduct(int index)
        {
            products.RemoveAt(index);
        }
    }
}
