using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfGenesys.Common
{
    public class IntegerRule : ValidationRule
    {
        public virtual int Min { get; set; } = 0;
        public virtual int Max { get; set; } = int.MaxValue;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is not string str)
                return new ValidationResult(false, "Value is not a string.");

            if (!int.TryParse(str, out int integer))
                return new ValidationResult(false, $"Value, {str}, is not an integer.");

            if (integer < Min || integer > Max)
                return new ValidationResult(false, $"Value must be between {Min} and {Max}.");

            return ValidationResult.ValidResult;
        }
    }
}
