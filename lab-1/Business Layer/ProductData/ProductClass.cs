using DailyMealPlanner.Business_Layer.Validator;
using DailyMealPlanner.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace DailyMealPlanner.Business_Layer.ProductData
{
    [Serializable]
    public class ProductClass : ICloneable
    {
        public ProductValidator productValidator { get; set; } = new ProductValidator();

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double gramms;
        public double Gramms
        {
            get { return gramms; }
            set { gramms = value; }
        }

        private double protein;
        public double Protein
        {
            get { return protein; }
            set { protein = value; }
        }

        private double fats;
        public double Fats
        {
            get { return fats; }
            set { fats = value; }
        }

        private double carbs;
        public double Carbs
        {
            get { return carbs; }
            set { carbs = value; }
        }

        private double calories;
        public double Calories
        {
            get { return calories; }
            set { calories = value; }
        }

        [XmlIgnore]
        private double caloriesForOneGramm = 0.0;
        public double CalloriesForOneGramm
        {
            get { return caloriesForOneGramm; }
            set { caloriesForOneGramm = value; }
        }

        [XmlIgnore]
        public string selectedMealTime = "";

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private void Update(ProductInfo product)
        {
            this.name = product.Name;
            this.gramms = product.Gramms;
            this.protein = product.Protein;
            this.fats = product.Fats;
            this.carbs = product.Carbs;
            this.calories = product.Calories;
            this.selectedMealTime = product.selectedMealTime;

            if (this.CalloriesForOneGramm == 0.0)
            {
                this.CalloriesForOneGramm = product.Calories * 1.0 / product.Gramms;
            }
        }

        private ProductClass()
        {

        }

        public ProductClass(string name, double gramms, double protein, double fats, double carbs, double calories)
        {
            ProductInfo product = new ProductInfo(name, gramms, protein, fats, carbs, calories);

            ValidateAllInformation(product);
        }

        public ProductClass(ProductInfo product)
        {
            ValidateAllInformation(product);
        }

        public void ValidateAllInformation(ProductInfo product)
        {
            bool isParamsValid = productValidator.Validate(product);

            Update(product);
        }

        public void ChangeName(string name)
        {
            string error = "";

            bool isParamsValid = productValidator.ValidateName(name, out error);

            if (isParamsValid == false)
            {
                MessageBox.Show(productValidator.Errors[0].Error);
            }
            else
            {
                this.name = name;
            }
        }

        public void ChangeInfo(double info, string nameofCharacteristic)
        {
            string error = "";

            bool isParamsValid = productValidator.ValidateInfo(info, nameofCharacteristic, out error);

            if (isParamsValid == false)
            {
                MessageBox.Show(productValidator.Errors[0].Error);
            }
            else
            {
                Update(info, nameofCharacteristic);
            }
        }

        public void Update(double info, string nameofCharacteristic)
        {
            switch(nameofCharacteristic)
            {
                case "Gramms":
                    this.gramms = info; break;
                case "Protein":
                    this.protein = info; break;
                case "Fats":
                    this.fats = info; break;
                case "Carbs":
                    this.carbs = info; break;
                case "Calories":
                    this.calories = info;  break;
            }
        }

        //----------------------------------------------------------------------------

    }
}
