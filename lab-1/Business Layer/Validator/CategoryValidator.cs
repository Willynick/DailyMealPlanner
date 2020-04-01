using DailyMealPlanner.Business_Layer.CategoryData;
using DailyMealPlanner.Business_Layer.Validator;
using DailyMealPlanner.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Business_Layer.Validator
{
    public class CategoryValidator : Validator<CategoryInfo>
    {
        private static int count = 0;

        public CategoryValidator()
        {

        }

        public override bool Validate(CategoryInfo category)
        {

            bool validResult = true;

            Errors.Clear();

            string error = "";
            bool valid;


            valid = ValidateInfo(category.name, "name", out error);
            if (valid != true)
            {
                Errors.Add(new ValidationResult("Name", error));
                category.name = "Empty Name";
            }

            valid = ValidateInfo(category.description, "description", out error);
            if (valid != true)
            {
                Errors.Add(new ValidationResult("Description", error));
                category.description = "Empty Description";
            }

            count++;

            if (Errors.Count != 0)
            {
                validResult = false;
                WriteErrorsToLogFile();
            }
            return validResult;
        }

        public bool ValidateInfo(string info, string nameofCharacteristic, out string error)
        {

            bool isValid = true;
            error = "";
            if (info == "" || info == null)
            {
                error = nameofCharacteristic + " is empty";
                isValid = false;
            }

            return isValid;
        }

        public void WriteErrorsToLogFile()
        {
            DataBase.log.AppendFormat("IN CATEGORY: " + count + "\r\n");
            foreach (ValidationResult error in Errors)
            {
                DataBase.log.AppendFormat(error.ParameterName + ": " + error.Error + "\r\n");
            }
        }
    }
}
