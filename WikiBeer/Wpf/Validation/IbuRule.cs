using System;
using System.Globalization;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.Validation
{
    public class IbuRule : ValidationRule
    {
        private String _errorMessage = String.Empty;

        private float _maxValue = 150;
        private float _minValue = 0;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var valueAsString = value as string;
            float ibu;

            if (valueAsString == String.Empty || valueAsString == null)
            {
                ibu = float.NaN;
            }
            else
            {
                ibu = float.Parse(valueAsString);
            }

            if (ibu <= _minValue || ibu >= _maxValue)
            {
                return new ValidationResult(false, this.ErrorMessage);
            }

            return new ValidationResult(true, null);
        }
    }
}
