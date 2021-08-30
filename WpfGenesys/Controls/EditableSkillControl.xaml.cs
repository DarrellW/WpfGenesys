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

namespace WpfGenesys.Controls
{
    /// <summary>
    /// Interaction logic for EditableSkillControl.xaml
    /// </summary>
    public partial class EditableSkillControl : UserControl
    {
        //public static readonly DependencyProperty IsCareerProperty = DependencyProperty.Register("IsCareer", typeof(bool), typeof(EditableCharacteristicControl), new PropertyMetadata(false));
        //public bool IsCareer
        //{
        //    get => (bool)GetValue(IsCareerProperty);
        //    set => SetValue(IsCareerProperty, value);
        //}

        //public static readonly DependencyProperty SkillProperty = DependencyProperty.Register("Skill", typeof(string), typeof(EditableCharacteristicControl), new PropertyMetadata(""));
        //public string Skill
        //{
        //    get => (string)GetValue(SkillProperty);
        //    set => SetValue(SkillProperty, value);
        //}
        public int Rank { get; set; }

        public Skill GetSelectedSkill()
        {
            if (SkillCombobox.SelectedItem is not string name
                || CareerCheckbox.IsChecked is not bool isChecked
                || !Validation.GetHasError(RankBox)) return null;
                
            return new Skill { IsCareer = isChecked, Name = name, Rank = Rank };
        }

        public EditableSkillControl()
        {
            InitializeComponent();
            DataContext = Rank;
        }
    }
}
