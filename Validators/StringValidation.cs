using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pract15_trpo.Validators
{
    class StringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = value.ToString();

            if (value == null || string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Значение не может быть пустым");
            }
            if (input.Length < 2)
            {
                return new ValidationResult(false, "Строка не может быть из 1 символа");
            }
            if (input.Length > 100)
            {
                return new ValidationResult(false, "Строка не может быть больше 100 символов");
            }
            return ValidationResult.ValidResult;
        }
    }
}
