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
    /// Interaction logic for RaceBlockControl.xaml
    /// </summary>
    public partial class RaceBlockControl : UserControl, IDisposable
    {
        private static readonly SolidColorBrush _hoverColor = new(Color.FromArgb(255, 200, 255, 255));
        private static readonly SolidColorBrush _selectedColour = new(Color.FromArgb(255, 180, 235, 245));
        private static readonly SolidColorBrush _transparent = Brushes.Transparent;

        private bool _disposed = false;

        public bool Selected = false;

        public Models.Race Race { get { return (Models.Race)DataContext; } }

        public RaceBlockControl()
        {
            InitializeComponent();

            RaceBlockGrid.Background = _transparent;
            MouseEnter += RaceBlockControl_MouseEnter;
            MouseLeave += RaceBlockControl_MouseLeave;
        }

        private void RaceBlockControl_MouseLeave(object sender, MouseEventArgs e)
        {
            //if (sender is not RaceBlockControl block)
            //    return;

            if (!Selected)
                RaceBlockGrid.Background = _transparent;
        }

        private void RaceBlockControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (sender is not RaceBlockControl block)
            //    return;

            if (!Selected)
                RaceBlockGrid.Background = _hoverColor;
        }

        public void Select()
        {
            Selected = true;
            RaceBlockGrid.Background = _selectedColour;
        }

        public void Unselect()
        {
            Selected = false;
            RaceBlockGrid.Background = _transparent;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            MouseEnter -= RaceBlockControl_MouseEnter;
            MouseLeave -= RaceBlockControl_MouseLeave;
            _disposed = true;
        }
    }
}
