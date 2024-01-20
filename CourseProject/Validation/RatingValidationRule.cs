using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CourseProject.Validation
{
    internal class RatingValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string decimalSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;

            if (value is string stringValue && float.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
            {
                // Проверяем наличие от 0 до 1 знака после запятой (используя десятичный разделитель InvariantCulture)
                string[] parts = stringValue.Split(decimalSeparator);

                if ((parts.Length == 2 && parts[1].Length == 1 && result >= 0 && result <= 10) ||
                    (parts.Length == 1 && result >= 0 && result <= 10))
                {
                    // Число либо с одной десятичной цифрой, либо целое (и оба варианта подходят)
                    return ValidationResult.ValidResult;
                }
                else
                {
                    MessageBox.Show("Рейтинг должен быть числом с одним знаком после запятой или без десятичной части и находится в диапазоне от 0 до 10.");
                    return new ValidationResult(false, "Рейтинг должен быть числом с одним знаком после запятой или без десятичной части и находится в диапазоне от 0 до 10.");
                }
            }
            MessageBox.Show("Значение не является допустимым числом.");

            return new ValidationResult(false, "Значение не является допустимым числом.");
        }
    }
}
