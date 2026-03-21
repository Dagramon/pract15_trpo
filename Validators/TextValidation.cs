using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pract15_trpo.Validators
{
    class TextValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = value.ToString();

            if (value == null || string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Значение не может быть пустым");
            }
            if (input.Length < 3)
            {
                return new ValidationResult(false, "Строка должна быть не меньше 3");
            }
            if (input.Length > 255)
            {
                return new ValidationResult(false, "Строка должна быть не больше 255");
            }
            return ValidationResult.ValidResult;
        }
    }
}
