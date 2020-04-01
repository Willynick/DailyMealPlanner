using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyMealPlanner.Business_Layer.Validator
{

    public abstract class Validator<T> where T : class
    {
        // Список ошибок
        public List<ValidationResult> Errors { get; private set; }

        public Validator()
        {
            Errors = new List<ValidationResult>();
        }

        // Валидация объекта
        public abstract bool Validate(T objectForValidation);

        public bool ShowErrorMessages()
        {
            if (this.Errors.Count != 0)
            {
                MessageBox.Show("There are some errors. These values cannot be assigned");

                foreach (ValidationResult validationResult in this.Errors)
                {
                    MessageBox.Show(validationResult.ParameterName + ": " + validationResult.Error);
                }

                return false;
            }

            return true;
        }
    }

    // Результат валидации
    public class ValidationResult
    {
        // Имя проверяемого параметра
        public string ParameterName { get; private set; }

        // Текст ошибки валидации
        public string Error { get; private set; }

        public ValidationResult(string parameterName, string errorMessage)
        {
            ParameterName = parameterName;
            Error = errorMessage;
        }
 
    }
}
