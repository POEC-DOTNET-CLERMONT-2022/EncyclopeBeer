using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Ipme.WikiBeer.Wpf.Converters
{
    public class IngredientInBeerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            IList toto = values[1] as IList;
            Nullable<bool> result = toto.Contains(values[0]);
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
