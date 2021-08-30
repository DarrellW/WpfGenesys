using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace WpfGenesys.Common
{
    public class MultiIntToPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
                throw new NotSupportedException("Only supports 2-D points.");

            if (values[0] is not int int1)
                if (values[0] is not string string1 || !int.TryParse(string1, out int1))
                    throw new ArgumentException("values[0] is not an integer");

            if (values[1] is not int int2)
                if (values[1] is not string string2 || !int.TryParse(string2, out int2))
                    throw new ArgumentException("values[1] is not an integer");

            return new Point(int1, int2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is not Point point)
                throw new ArgumentException("value is not a Point");

            return new object[] { point.X, point.Y };
        }
    }

    public class AddMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length <= 0)
                throw new ArgumentException("No arguments");

            int sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is not int num)
                    if (values[i] is not string str || !int.TryParse(str, out num))
                        throw new ArgumentException("values[0] is not an integer");

                sum += num;
            }

            return sum;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
    public class ScaleMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length <= 0)
                throw new ArgumentException("No arguments");

            if (values[0] is not int product)
                if (values[0] is not string str || !int.TryParse(str, out product))
                    throw new ArgumentException("values[0] is not an integer");

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] is not int num)
                    if (values[i] is not string str || !int.TryParse(str, out num))
                        throw new ArgumentException("values[0] is not an integer");

                product *= num;
            }

            return product;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
    public class SubtractMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length <= 0)
                throw new ArgumentException("No arguments");

            if (values[0] is not int difference)
                if (values[0] is not string str || !int.TryParse(str, out difference))
                    throw new ArgumentException("values[0] is not an integer");

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] is not int num)
                    if (values[i] is not string str || !int.TryParse(str, out num))
                        throw new ArgumentException("values[0] is not an integer");

                difference -= num;
            }

            return difference;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }

    public class GetMinIntMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int min = Int32.MaxValue;

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] is not int num)
                    if (values[i] is not string str || !int.TryParse(str, out num))
                        throw new ArgumentException("values[0] is not an integer");

                if (num < min) min = num;
            }

            return min;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}
