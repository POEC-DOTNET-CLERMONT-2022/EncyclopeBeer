using System;
using System.Globalization;
using System.Windows.Data;

namespace Ipme.WikiBeer.Wpf.Converters
{
    [ValueConversion(typeof(DateTime), typeof(int))]
    public class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var birthDate = (DateTime)value;
                var now = DateTime.Today;
                var age = now.Year - birthDate.Year;

                if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                {
                    age--;
                }

                return age;
            }
            else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
