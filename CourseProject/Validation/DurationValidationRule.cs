using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace CourseProject.Validation
{
    internal class DurationValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string stringValue && int.TryParse(stringValue, out int result))
            {
                if (result >= 0 && result <= int.MaxValue)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    MessageBox.Show("Длительность должна быть целым положительным числом.");
                    return new ValidationResult(false, "Длительность должна быть целым положительным числом.");
                }
            }
            MessageBox.Show("Значение не является допустимым числом.");

            return new ValidationResult(false, "Значение не является допустимым числом.");
        }
    }
}
