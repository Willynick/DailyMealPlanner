using DailyMealPlanner.Business_Layer;
using DailyMealPlanner.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Business_Layer.Validator
{
    public class MealTimeValidator : Validator<MealTime>
    {
        private static int count = 0;

        public MealTimeValidator()
        {

        }

        public override bool Validate(MealTime mealTime)
        {

            bool validResult = true;

            Errors.Clear();

            string error = "";
            bool valid;


            valid = ValidateName(mealTime.name, out error);
            if (valid != true)
            {
                Errors.Add(new ValidationResult("Name", error));
                mealTime.name = "Empty Name";
            }

            count++;

            if (Errors.Count != 0)
            {
                validResult = false;
                WriteErrorsToLogFile();
            }

            return validResult;
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
            DataBase.log.AppendFormat("IN MEALTIME: " + count + "\r\n");
            foreach (ValidationResult error in Errors)
            {
                DataBase.log.AppendFormat(error.ParameterName + ": " + error.Error + "\r\n");
            }
        }
    }
}
