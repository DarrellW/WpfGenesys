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
using WpfGenesys.Models;

namespace WpfGenesys.Views.CharacterCreation
{
    /// <summary>
    /// Interaction logic for CustomRaceView.xaml
    /// </summary>
    public partial class CustomRaceView : Page
    {
        public Race NewRace { get; set; }
        public CustomRaceView()
        {
            //Can't bind to struct because structs are readonly. Can I have multiple DataContexts?
            NewRace = new Race()
            {
                BaseCharacteristics = new Characteristics()
            };

            //DataContext = NewRace;
            InitializeComponent();


            DataContext = new CustomRaceViewModel
            {
                Name = "",
                Brawn = 2,
                Agility = 2,
                Intellect = 2,
                Cunning = 2,
                Willpower = 2,
                Presence = 2,
                BaseWoundThreshold = 10,
                BaseStrainThreshold = 10,
                StartingExperience = 90
            };
            //Brawn.Text = NewRace.BaseCharacteristics.Brawn.ToString();
            //Agility.Text = NewRace.BaseCharacteristics.Agility.ToString();
            //Intellect.Text = NewRace.BaseCharacteristics.Intellect.ToString();
            //Cunning.Text = NewRace.BaseCharacteristics.Cunning.ToString();
            //Willpower.Text = NewRace.BaseCharacteristics.Willpower.ToString();
            //Presence.Text = NewRace.BaseCharacteristics.Presence.ToString();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void Characteristic_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            textBox.SelectAll();
        }

        private void Characteristic_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            textBox.SelectAll();
        }

        private void Characteristic_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            textBox.SelectAll();
        }

        private void Characteristic_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            //textBox.
        }

        private void TextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void NewSkillButton_Click(object sender, RoutedEventArgs e)
        {
            var x = new Controls.EditableSkillControl();
            RacialSkillsPanel.Children.Add(x);
        }
    }
}
