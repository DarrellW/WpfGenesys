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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();

        }



        #region Navigation
        public Page[] Pages;
        public int CurrentPage = -1;

        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage <= 0)
                return;

            MainFrame.Navigate(Pages[--CurrentPage]);
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage >= Pages.Length)
                return;

            MainFrame.Navigate(Pages[++CurrentPage]);
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            CurrentPage = PageButtons.Children.IndexOf(button);
            MainFrame.Navigate(Pages[CurrentPage]);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            HomeGrid.Visibility = Visibility.Collapsed;

            // Alternatively, Type Pages = new Type[]{typeof(RaceSelectView),typeof(CareerSelectView)}; Then, Navigate(new Pages[0]() ??
            Pages = new Page[]
            {
                new CharacterCreation.RaceSelectView(),
                new CharacterCreation.CareerSelectView()
            };

            for (int i = 0; i < Pages.Length; i++)
            {
                Button newButton = new();

                newButton.SetBinding(WidthProperty, new Binding { RelativeSource = RelativeSource.Self, Path = new PropertyPath("ActualHeight") });
                newButton.Loaded += NewButton_Loaded;
                newButton.Click += NavigationButton_Click;

                PageButtons.Children.Add(newButton);
            }

            CurrentPage = 0;
            MainFrame.Navigate(Pages[0]);

            Navigation.Visibility = Visibility.Visible;
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
                MainFrame.Navigate(new Edit.CharacterView());
            }

            Navigation.Visibility = Visibility.Visible;
        }
        
        /// <summary>
        /// Called once the button is loaded to make the corners round (by changing template, border radius property).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is not Button newButton)
                return;

            newButton.Loaded -= NewButton_Loaded;

            var buttonBorder = (Border) newButton.Template.FindName("border", newButton);
            buttonBorder.SetBinding(Border.CornerRadiusProperty, new Binding { RelativeSource = RelativeSource.Self, Path = new PropertyPath("ActualHeight") });
        }
        #endregion

        public void LoadNewPage<T>(T e)
        {
            //usage
            List<Type> pagesAndStuff = new()
            {
                typeof(CharacterCreation.RaceSelectView)
            };

            var type = pagesAndStuff.First();
            //option 1
            var first = Activator.CreateInstance(type);

            //reflection
            //pagesAndStuff.First().GetConstructor(signature).Invoke(args)
        }
    }
}
