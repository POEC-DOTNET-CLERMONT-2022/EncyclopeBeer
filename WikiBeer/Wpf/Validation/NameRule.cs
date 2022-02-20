using System;
using System.Globalization;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.Validation
{
    public class NameRule : ValidationRule
    {
        private String _errorMessage = String.Empty;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;

            if (String.IsNullOrEmpty(str))
            //if (str == null || str == string.Empty)
            {
                    return new ValidationResult(false, this.ErrorMessage);
            }

            return new ValidationResult(true, null);
        }
    }
}
