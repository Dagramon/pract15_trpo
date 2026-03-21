using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pract15_trpo.Validators
{
    class IntValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = value.ToString();

            if (value == null || string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Значение не может быть пустым");
            }

            if (!int.TryParse(input, out int longValue))
            {
                return new ValidationResult(false, "Количество это число");
            }

            if (Convert.ToInt32(input) <= 0)
            {
                return new ValidationResult(false, "Количество не может быть меньше либо равно нулю");
            }
            if (Convert.ToInt32(input) > int.MaxValue)
            {
                return new ValidationResult(false, "Слишком большое число");
            }
            return ValidationResult.ValidResult;
        }
    }
}
