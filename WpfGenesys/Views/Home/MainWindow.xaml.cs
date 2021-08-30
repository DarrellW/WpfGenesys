using System;
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

namespace WpfGenesys.Views.Home
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FormNavigation _navigation;
        private NavigationService _navigationService;

        public MainWindow()
        {
            InitializeComponent();

            _navigation = new FormNavigation();

            //NavigationService.GetNavigationService
            _navigationService = CharacterCreationFrame.NavigationService;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private int _currentWindow;

        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new CharacterCreation.RaceSelectView());
            _currentWindow--;
            //NavigationService.GetNavigationService().CanNavigate/Navigate
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            CharacterCreationFrame.Content = new CharacterCreation.RaceSelectView();
            _currentWindow++;
        }

        private void RaceButton_Click(object sender, RoutedEventArgs e)
        {
            CharacterCreation.RaceSelectView view = new();

            _navigationService.Navigate(view);
        }

        private void CareerButton_Click(object sender, RoutedEventArgs e)
        {
            CharacterCreation.CareerSelectView view = new();

            _navigationService.Navigate(view);
        }
    }
}
