using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfGenesys.Common
{
    class XYScaleMultiValueConverter : IMultiValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values">Desired Width and Height.</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">1/DesignWidth and 1/DesignHeight as a double[] size 2.</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //if (values.Length != 2)
            //    throw new NotSupportedException("");
            //if (parameter is not double[] invertedDimensions || invertedDimensions.Length != 2)
            //    throw new ArgumentException("Parameter must be of type double[2].");

            //// Desired dimensions
            //if (values[0] is not double double0 && !double.TryParse((string)values[0], out double0))
            //    throw new ArgumentException("");
            //if (values[1] is not double double1 && !double.TryParse((string)values[1], out double1))
            //    throw new ArgumentException("");

            double[] invertedDimensions = (double[])parameter;
            double scale0 = ((double)values[0]) * invertedDimensions[0];
            double scale1 = ((double)values[1]) * invertedDimensions[1];

            if (scale0 == double.NaN)
            {
                if (scale1 == double.NaN)
                    return 1.0;
                return scale1;
            }
            if (scale1 == double.NaN) return scale0;

            return scale0 < scale1 ? scale0 : scale1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            double[] invertedDimensions = (double[])parameter;

            double scale = (double)value;

            return new object[]
            {
                scale / invertedDimensions[0],
                scale / invertedDimensions[1]
            };
        }
    }
}
