using System;
using System.Globalization;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.Validation
{
    public class IbuRule : ValidationRule
    {
        private float _MaxValue = 150;
        private float _MinValue = 1;

        private String _errorMessage = String.Empty;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var ibu = (float)value;

            if (ibu !>= _MinValue && ibu !<= _MinValue)
            //if (str == null || str == string.Empty)
            {
                return new ValidationResult(false, this.ErrorMessage);
            }

            return new ValidationResult(true, null);
        }
    }
}
