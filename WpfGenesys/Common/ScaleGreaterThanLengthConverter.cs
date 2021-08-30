using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfGenesys.Common
{
    public class ScaleGreaterThanLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int length && !int.TryParse((string)value, out length))
                throw new ArgumentException();
            if (parameter is not int max && !int.TryParse((string)parameter, out max))
                throw new ArgumentException();

            return length > max ? max / (double)length : 1.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back.");
        }
    }
}
