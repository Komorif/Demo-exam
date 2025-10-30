using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Фамилия.Converters
{
    internal class DiscountCalculationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,  object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] == null || values[1] == null)
                return "0.00";

            double price = System.Convert.ToDouble(values[0]);
            double discount = System.Convert.ToDouble(values[1]);

            double finalPrice = price - (price * discount / 100);

            // если цена "со скидкой"
            if (parameter?.ToString() == "Discounted")
            {
                if (discount == 0)
                    return price.ToString("F2");

                return finalPrice.ToString("F2");
            }

            // если цена "цена изначально"
            if (parameter?.ToString() == "Initially")
            {
                if (discount == 0)
                    return null;

                return finalPrice.ToString("F2");
            }

            return finalPrice.ToString("F2");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
