using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Фамилия.DataBase;

namespace Фамилия.Converters
{
    internal class DiscountColorConverter : IValueConverter
    {
        private static readonly SolidColorBrush GreenBrush = new SolidColorBrush(Color.FromRgb(0x2E, 0x8B, 0x57));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as Goods;
            if (item == null)
                return Brushes.White;

            if (item.Orders.Count <= 0)
                return Brushes.LightBlue;

            if (item.Discount > 15)
                return GreenBrush;

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
