using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DailyMealPlanner.Business_Layer.UserData;
using DailyMealPlanner.Data_Layer;
using DailyMealPlanner.Utility;

namespace DailyMealPlanner.Business_Layer.Validator
{
    public class UserValidator : Validator<UserInfo>
    {
        public UserValidator()
        {

        }

        public override bool Validate(UserInfo user)
        {
            Errors.Clear();
            bool validResult = true;

            bool valid;

            valid = ValidateWeight(user.Weight);
            if (!valid)
            {
                user.Weight = 0.0;
            }

            valid = ValidateHeight(user.Height);
            if (!valid)
            {
                user.Height = 0.0;
            }

            valid = ValidateAge(user.Age);
            if (!valid)
            {
                user.Age = 0;
            }

            if (Errors.Count != 0)
            {
                validResult = false;
                WriteErrorsToLogFile();
            }

            return validResult;
        }

        public bool ValidateWeight(double weight)
        {
            bool isValid = true;
            string error = "";
            if (weight <= 0 || weight > 500)
            {
                error = "weight less than 0 or greater than 500 kg";
                isValid = false;

                Errors.Add(new ValidationResult("Weight", error));
            }

            return isValid;
        }

        public bool ValidateHeight(double height)
        {
            bool isValid = true;
            string error = "";
            if (height <= 0 || height > 300)
            {
                error = "height less than 0 or greater than 300 cm";
                isValid = false;

                Errors.Add(new ValidationResult("Height", error));
            }

            return isValid;
        }

        public bool ValidateAge(double age)
        {

            bool isValid = true;
            string error = "";
            if (age <= 0 || age > 300)
            {
                error = "age less than 0 or greater than 300 years";
                isValid = false;

                Errors.Add(new ValidationResult("Age", error));
            }

            return isValid;
        }

        public void WriteErrorsToLogFile()
        {
            DataBase.log.AppendFormat("IN USER: " + "\r\n");
            foreach (ValidationResult error in Errors)
            {
                DataBase.log.AppendFormat(error.ParameterName + ": " + error.Error + "\r\n");
            }
        }
    }
}
