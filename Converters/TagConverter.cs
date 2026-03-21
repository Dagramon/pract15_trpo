using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pract15_trpo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace pract15_trpo.Converters
{
    class TagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tagCollection = (IEnumerable)value;
            var items = tagCollection.Cast<Tag>().ToList();

            if (items.Count == 0)
                return "Нет тегов";

            string tags = "";

            foreach (var item in items)
            {
                tags += $"#{item.Name} ";
            }
            return tags;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
