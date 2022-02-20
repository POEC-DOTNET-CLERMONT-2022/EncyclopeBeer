using System;
using System.Globalization;
using System.Windows.Data;

namespace Ipme.WikiBeer.Wpf.Converters
{
    [ValueConversion(typeof(string), typeof(float))]
    public class StringFloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                float result = (float)value;
                return result;
            }
            else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
