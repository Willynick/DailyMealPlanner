using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DailyMealPlanner.Business_Layer.ProductData
{
    [Serializable]
    public class ProductInfo
    {
        public string Name { get; set; }
        public double Gramms { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }

        [XmlIgnore]
        public string selectedMealTime = "";

        public ProductInfo()
        {

        }

        public ProductInfo(string name, double gramms, double protein, double fats, double carbs, double calories)
        {
            this.Name = name;
            this.Gramms = gramms;
            this.Protein = protein;
            this.Fats = fats;
            this.Carbs = carbs;
            this.Calories = calories;
        }

        public void FillInfo (ProductClass product)
        {
            this.Name = product.Name;
            this.Gramms = product.Gramms;
            this.Protein = product.Protein;
            this.Fats = product.Fats;
            this.Carbs = product.Carbs;
            this.Calories = product.Calories;
            this.selectedMealTime = product.selectedMealTime;
        }
    }
}
