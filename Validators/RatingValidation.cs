using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace pract15_trpo.Validators
{
    class RatingValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = value.ToString();

            if (value == null || string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Значение не может быть пустым");
            }

            if (!float.TryParse(input, out float rating))
            {
                return new ValidationResult(false, "Неккоректый формат");
            }

            if (rating < 0.0 || rating > 5.0)
            {
                return new ValidationResult(false, "Рейтинг должен быть от 0.0 до 5.0");
            }

            if (input.Length > 3)
            {
                return new ValidationResult(false, "Неккоректный формат для рейтинга");
            }
            return ValidationResult.ValidResult;
        }
    }
}
