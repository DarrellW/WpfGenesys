using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfGenesys.Common
{
    public class BooleanConverter<T> : IValueConverter
    {
        public T True { get; set; }
        public T False { get; set; }
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return value is bool boolVal && boolVal ? True : False;
        }
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return value is T t && EqualityComparer<T>.Default.Equals(t, True);
        }
    }

    public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed) { }
    }

    public class CountConverter<T> : IValueConverter
    {
        public T HasItemsValue { get; set; }
        public T EmptyValue { get; set; }

        public CountConverter(T hasItemsValue, T emptyValue)
        {
            HasItemsValue = hasItemsValue;
            EmptyValue = emptyValue;
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return value is int intVal && intVal > 0 ? HasItemsValue : EmptyValue;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return (value is T t && EqualityComparer<T>.Default.Equals(t, HasItemsValue)) ? 1 : 0;
        }
    }

    public sealed class CountToVisibilityConverter : CountConverter<Visibility>
    {
        public CountToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed) { }
    }

    public class IntegerToVisibilityConverter : IValueConverter
    {
        public int HiddenValue { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int intValue) return null;

            return intValue == HiddenValue ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Visibility visibility) return null;

            return visibility == Visibility.Visible ? HiddenValue + 1 : HiddenValue;
        }
    }

    public class CountANDBoolToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is int count)
                {
                    if (count <= 0)
                        return Visibility.Collapsed;
                }
                else if (values[i] is bool boolean)
                {
                    if (!boolean)
                        return Visibility.Collapsed;
                }
                else return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }

    public class ObjectNotNullVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}
