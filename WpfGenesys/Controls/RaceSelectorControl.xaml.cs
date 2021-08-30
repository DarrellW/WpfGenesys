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

namespace WpfGenesys.Controls
{
    /// <summary>
    /// Interaction logic for RaceSelectorControl.xaml
    /// </summary>
    public partial class RaceSelectorControl : UserControl
    {
        //list of species
        //private List<Models.Race> _races;
        public RaceSelectorControl()
        {
            InitializeComponent();


            //var block = new TextBlock() { Text = Text };
            //Panel.Children.Add(block);
        }

        //public RaceSelectorControl(object data) : this()
        //{
        //    ListBoxTest.DataContext = data;
        //}

        #region shit doesn't work
        public static readonly DependencyProperty SpeciesProperty = DependencyProperty.Register(
            "Races", typeof(List<Models.Race>), typeof(RaceSelectorControl));

        public List<Models.Race> Species
        {
            get { return (List<Models.Race>)GetValue(SpeciesProperty); }
            set { SetValue(SpeciesProperty, value); }
        }
        #endregion

        public static readonly DependencyProperty ValueProperty =
    DependencyProperty.Register(
        "Value", typeof(decimal), typeof(RaceSelectorControl),
        new FrameworkPropertyMetadata(decimal.MinValue, new PropertyChangedCallback(OnValueChanged),
                                      new CoerceValueCallback(CoerceValue)));

        private static object CoerceValue(DependencyObject element, object value)
        {
            decimal newValue = (decimal)value;

            newValue = Math.Max(decimal.MinValue, Math.Min(decimal.MaxValue, newValue));

            return newValue;
        }

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            RaceSelectorControl control = (RaceSelectorControl)obj;

            RoutedPropertyChangedEventArgs<decimal> e = new RoutedPropertyChangedEventArgs<decimal>(
                (decimal)args.OldValue, (decimal)args.NewValue, ValueChangedEvent);
            control.OnValueChanged(e);
        }
        /// <summary>
        /// Identifies the ValueChanged routed event.
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            "ValueChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<decimal>), typeof(RaceSelectorControl));

        /// <summary>
        /// Occurs when the Value property changes.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<decimal> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }
        /// <summary>
        /// Raises the ValueChanged event.
        /// </summary>
        /// <param name="args">Arguments associated with the ValueChanged event.</param>
        protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<decimal> args)
        {
            RaiseEvent(args);
        }

        #region WhyAreThereSoManyStepsToMakingAProperty oh and it doesn't compile
        //public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        //    "Text", typeof(string), typeof(RaceSelectorControl),// new PropertyMetadata("Hi"),
        //    new PropertyMetadata("", OnTextChanged)
        //    );

        //public static void OnTextChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        //{
        //    if (source is RaceSelectorControl control)
        //    {
        //        RoutedPropertyChangedEventArgs<string> e = new RoutedPropertyChangedEventArgs<string>((string)args.OldValue, (string)args.NewValue, TextChangedEvent);
        //        control.OnTextChanged(e);
        //    }
        //}

        //protected virtual void OnTextChanged(RoutedPropertyChangedEventArgs<string> e)
        //{
        //    RaiseEvent(e);
        //}

        //public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent("TextChanged", RoutingStrategy.Bubble,
        //    typeof(RoutedPropertyChangedEventArgs<string>), typeof(RaceSelectorControl));

        //public event RoutedPropertyChangedEventArgs<string> TextChanged
        //{
        //    add { AddHandler(TextChangedEvent, value); }
        //    remove { RemoveHandler(TextChangedEvent, value); }
        //}

        //public string Text
        //{
        //    get { return (string)GetValue(TextProperty); }
        //    set { SetValue(TextProperty, value); }
        //}
        #endregion
    }
}
