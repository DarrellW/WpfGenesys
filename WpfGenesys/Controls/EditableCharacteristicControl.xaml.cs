using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfGenesys.Common;

namespace WpfGenesys.Controls
{
    /// <summary>
    /// Interaction logic for EditableCharacteristicControl.xaml
    /// </summary>
    public partial class EditableCharacteristicControl : UserControl
    {
        public static readonly DependencyProperty HasErrorsProperty = DependencyProperty.Register("HasErrors", typeof(bool), typeof(EditableCharacteristicControl), new PropertyMetadata(false));
        public bool HasErrors
        {
            get => (bool)GetValue(HasErrorsProperty);
            set => SetValue(HasErrorsProperty, value);
        }

        public static readonly DependencyProperty CharacteristicNameProperty = DependencyProperty.Register("CharacteristicName", typeof(string), typeof(EditableCharacteristicControl), new PropertyMetadata("INTELLECT"));
        public string CharacteristicName
        {
            get => (string)GetValue(CharacteristicNameProperty);
            set => SetValue(CharacteristicNameProperty, value);
        }

        public static readonly DependencyProperty CharacteristicValueProperty = DependencyProperty.Register("CharacteristicValue", typeof(int), typeof(EditableCharacteristicControl), new PropertyMetadata(1));
        public int CharacteristicValue
        {
            get => (int)GetValue(CharacteristicValueProperty);
            set => SetValue(CharacteristicValueProperty, value);
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(EditableCharacteristicControl), new PropertyMetadata(1.0));
        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }
        public static readonly DependencyProperty SizeXProperty = DependencyProperty.Register("SizeX", typeof(double), typeof(EditableCharacteristicControl), new PropertyMetadata(80.0));
        public double SizeX
        {
            get => (double)GetValue(SizeXProperty);
            set => SetValue(SizeXProperty, value);
        }
        public static readonly DependencyProperty SizeYProperty = DependencyProperty.Register("SizeY", typeof(double), typeof(EditableCharacteristicControl), new PropertyMetadata(60.0));
        public double SizeY
        {
            get => (double)GetValue(SizeYProperty);
            set => SetValue(SizeYProperty, value);
        }

        public static readonly DependencyProperty EnabledProperty = DependencyProperty.Register("Enabled", typeof(bool), typeof(EditableCharacteristicControl), new PropertyMetadata(false));
        public bool Enabled
        {
            get => (bool)GetValue(EnabledProperty);
            set => SetValue(EnabledProperty, value);
        }

        private readonly static double[] _invertedDimensions = { 0.0125, 0.0167 };

        //public class StringCoordinateSizeTypeConverter : System.ComponentModel.TypeConverter
        //{
        //    public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        //    {
        //        return sourceType == typeof(string);
        //    }

        //    public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        //    {
        //        return ((CoordinateSize)(string)value);
        //    }

        //    public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
        //    {
        //        return destinationType == typeof(CoordinateSize);
        //    }

        //    public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        //    {
        //        return value == null ? null : ((string)(CoordinateSize)value);
        //    }
        //}

        //[System.ComponentModel.TypeConverter(typeof(StringCoordinateSizeTypeConverter))]
        //public struct CoordinateSize
        //{
        //    private bool _scaleByWidth;
        //    private double _scale;

        //    public CoordinateSize(string value)
        //    {
        //        string[] strs = value.Replace(" ", string.Empty).Split(",");
        //        if (strs.Length != 2) throw new ArgumentException("Must be of form y, 50 or 60,X");

        //        if (double.TryParse(strs[0], out double size))
        //        {
        //            _scaleByWidth = strs[1].ToLower() == "x";
        //        }
        //        else
        //        {
        //            _scaleByWidth = strs[0].ToLower() == "x";
        //            size = double.Parse(strs[1]);
        //        }
        //        _scale = _scaleByWidth ? size * 0.0125 : size * 0.0167;
        //    }

        //    public double Scale
        //    {
        //        get { return _scale; }
        //        set { _scale = value; }
        //    }

        //    public static explicit operator CoordinateSize(string str) => new CoordinateSize(str);
        //    public static implicit operator string(CoordinateSize cs) => (cs._scaleByWidth ? "x" : "y") + cs._scale;
        //}

        public EditableCharacteristicControl()
        {
            InitializeComponent();

            var scaleBinding = new MultiBinding
            {
                Converter = new XYScaleMultiValueConverter(),
                ConverterParameter = _invertedDimensions,
                Mode = BindingMode.OneWay
            };
            scaleBinding.Bindings.Add(new Binding
            {
                Mode = BindingMode.OneWay,
                RelativeSource = RelativeSource.Self,
                Path = new PropertyPath("SizeX")
            });
            scaleBinding.Bindings.Add(new Binding
            {
                Mode = BindingMode.OneWay,
                RelativeSource = RelativeSource.Self,
                Path = new PropertyPath("SizeY")
            });

            this.SetBinding(ScaleProperty, scaleBinding);

            
            //Validation.AddErrorHandler(this, (s, args) =>
            //{
            //    if (args.Action == ValidationErrorEventAction.Added)
            //    {
            //        this.ToolTip = args.Error.ErrorContent;
            //        HasErrors = true;
            //    }
            //    else
            //    {
            //        this.ToolTip = null;
            //        HasErrors = false;
            //    }
            //});
        }
    }
}
