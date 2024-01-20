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
    public class YearValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string stringValue && int.TryParse(stringValue, out int result))
            {
                if (result >= 1895 && result <= DateTime.Now.Year)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    MessageBox.Show("Год должен быть в диапазоне от 1895 до текущего года.");
                    return new ValidationResult(false, "Год должен быть в диапазоне от 1895 до текущего года.");
                }
            }
            MessageBox.Show("Значение не является допустимым числом.");

            return new ValidationResult(false, "Значение не является допустимым числом.");
        }
    }
}
