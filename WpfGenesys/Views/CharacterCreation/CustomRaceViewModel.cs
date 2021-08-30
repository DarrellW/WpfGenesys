using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfGenesys.Models;

namespace WpfGenesys.Views.CharacterCreation
{
    class CustomRaceViewModel
    {
        public string Name { get; set; }
        
        public int StartingExperience { get; set; }

        public int Brawn { get; set; }
        public int Agility { get; set; }
        public int Intellect { get; set; }
        public int Cunning { get; set; }
        public int Willpower { get; set; }
        public int Presence { get; set; }

        public int BaseStrainThreshold { get; set; }
        public int BaseWoundThreshold { get; set; }

        //Racial skills (career?, ranks)
        public List<Skill> RacialSkills { get; set; }
        //Racial abilities... if permanent effect, use ability/effect template. else simple string.
        //public WpfGenesys.Models.ItemAttachment.Affects or w/e
        //public string Description { get; set; }
    }
}
