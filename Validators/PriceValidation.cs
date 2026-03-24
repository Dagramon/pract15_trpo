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

            try
            {
                if (value == null || string.IsNullOrWhiteSpace(input))
                {
                    return new ValidationResult(false, "Значение не может быть пустым");
                }

                if (!decimal.TryParse(input, out decimal longValue))
                {
                    return new ValidationResult(false, "Цена состоит только из цифр");
                }

                if (Convert.ToDecimal(input) <= 0)
                {
                    return new ValidationResult(false, "Цена не может быть меньше либо равно нулю");
                }
                if (Convert.ToDecimal(input) > decimal.MaxValue)
                {
                    return new ValidationResult(false, "Слишком большое число");
                }
            } catch
            {
                return new ValidationResult(false, "Слишком большое число");
            }
            return ValidationResult.ValidResult;
        }
    }
}
