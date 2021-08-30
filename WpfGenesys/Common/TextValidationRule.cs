using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfGenesys.Common
{
    public class TextValidationRule : ValidationRule
    {
        public int MaxLength { get; set; } = 30;
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int length = ((string)value).Length;

            if (length <= 0)
                return new ValidationResult(false, $"No text entered (length = {length}).");
            if (length > MaxLength)
                return new ValidationResult(false, $"Text is too long (length = {length}).");
            return ValidationResult.ValidResult;
        }
    }
}
