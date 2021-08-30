using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfGenesys.Common
{
    public class PercentageConverter : MarkupExtension, IValueConverter
    {
        private static PercentageConverter _instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return System.Convert.ToDouble(value) / System.Convert.ToDouble(parameter);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ??= new PercentageConverter();
        }
    }
}
