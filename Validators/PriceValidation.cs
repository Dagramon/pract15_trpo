using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pract15_trpo.Validators
{
    class PriceValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = value.ToString();

            if (value == null || string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Значение не может быть пустым");
            }

            if (!double.TryParse(input, out double longValue))
            {
                return new ValidationResult(false, "Цена состоит только из цифр");
            }

            if (Convert.ToDouble(input) <= 0)
            {
                return new ValidationResult(false, "Цена не может быть меньше либо равно нулю");
            }
            if (Convert.ToDouble(input) > double.MaxValue)
            {
                return new ValidationResult(false, "Слишком большое число");
            }
            return ValidationResult.ValidResult;
        }
    }
}
