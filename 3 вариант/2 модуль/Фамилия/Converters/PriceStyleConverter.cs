using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Фамилия.DataBase;

namespace Фамилия.Converters
{
    internal class PriceStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as Goods;
            if (item == null)
                return targetType == typeof(Brush) ? Brushes.Black : null;

            bool hasDiscount = item.Discount > 0;

            if (targetType == typeof(Brush))
                return hasDiscount ? Brushes.Red : Brushes.Black;

            if (targetType == typeof(TextDecorationCollection))
                return hasDiscount ? TextDecorations.Strikethrough : null;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
