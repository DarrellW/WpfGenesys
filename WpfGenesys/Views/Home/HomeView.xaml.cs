using Microsoft.Win32;
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

namespace WpfGenesys.Views.Home
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class HomeView : Page
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CharacterCreation.RaceSelectView());
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.DefaultExt = ".xml";
            dialog.Filter = "XML files (.xml)|*.xml";
            //dialog.InitialDirectory = "";

            if (dialog.ShowDialog() == true)
            {
                string file = dialog.FileName;

                //open & read file

                //navigate to preview page
                NavigationService.Navigate(new Edit.CharacterView());
            }
        }
    }
}
