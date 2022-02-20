using System;
using System.Globalization;
using System.Windows.Data;

namespace Ipme.WikiBeer.Wpf.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class CertifiedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var certified = (bool)value;
                string result;
                if (certified)
                {
                    result = "Certified User";
                }
                else
                {
                    result = "User";
                }

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
