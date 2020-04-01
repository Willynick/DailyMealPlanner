using DailyMealPlanner.Business_Layer.ProductData;
using DailyMealPlanner.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Business_Layer.Validator
{
    public class ProductValidator : Validator<ProductInfo>
    {
        private static int count = 0;

        public ProductValidator()
        {

        }

        public override bool Validate(ProductInfo product)
        {

            bool validResult = true;

            Errors.Clear();

            string error = "";
            bool valid;

            valid = ValidateName(product.Name, out error);
            if (valid != true)
            {
                Errors.Add(new ValidationResult("Name", error));
                product.Name = "Empty Name";
            }

            valid = ValidateInfo(product.Gramms, "gramms", out error);
            if (valid != true)
            {
                Errors.Add(new ValidationResult("Gramms", error));
                product.Gramms = 1.0;
            }

            valid = ValidateInfo(product.Protein, "protein", out error);
            if (valid != true)
            {
                Errors.Add(new ValidationResult("Protein", error));
                product.Protein = 0.0;
            }

            valid = ValidateInfo(product.Fats, "fats", out error);
            if (valid != true)
            {
                Errors.Add(new ValidationResult("Fats", error));
                product.Fats = 0.0;
            }

            valid = ValidateInfo(product.Carbs, "carbs", out error);
            if (valid != true)
            {
                Errors.Add(new ValidationResult("Carbs", error));
                product.Carbs = 0.0;
            }

            valid = ValidateInfo(product.Calories, "calories", out error);
            if (valid != true)
            {
                Errors.Add(new ValidationResult("Calories", error));
                product.Calories = 0.0;
            }

            count++;

            if (Errors.Count != 0)
            {
                validResult = false;
                WriteErrorsToLogFile();
            }

            return validResult;
        }

        public bool ValidateInfo(double info, string nameofCharacteristic, out string error)
        {

            bool isValid = true;
            error = "";
            if (info < 0.0)
            {
                error = nameofCharacteristic + " less than 0.0";
                isValid = false;
            }

            return isValid;
        }

        public bool ValidateName(string name, out string error)
        {
            bool isValid = true;
            error = "";
            if (name == "" || name == null)
            {
                error = "name is empty";
                isValid = false;
            }

            return isValid;
        }

        public void WriteErrorsToLogFile()
        {
            DataBase.log.AppendFormat("IN PRODUCT: " + count + "\r\n");
            foreach (ValidationResult error in Errors)
            {
                DataBase.log.AppendFormat(error.ParameterName + ": " + error.Error + "\r\n");
            }
        }
    }
}
