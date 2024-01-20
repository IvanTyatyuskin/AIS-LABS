using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace CourseProject.Validation
{
    internal class VotesValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string stringValue && int.TryParse(stringValue, out int result))
            {
                if (result >= 1 && result <= int.MaxValue)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    MessageBox.Show("Количество голосов должно быть целым положительным числом.");
                    return new ValidationResult(false, "Количество голосов должно быть целым положительным числом.");
                }
            }
            MessageBox.Show("Значение не является допустимым числом.");

            return new ValidationResult(false, "Значение не является допустимым числом.");
        }
    }
}
