using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfGenesys.Data;

namespace WpfGenesys.Models
{
    public class Character
    {
        public Character(int woundThreshold, int strainThreshold, int meleeDefense = 0, int rangedDefense = 0)
        {
            _startingWoundThreshold = woundThreshold;
            _startingStrainThreshold = strainThreshold;
            _permanentMeleeDefense = meleeDefense;
            _permanentRangedDefense = rangedDefense;
        }

        public string CharacterName { get; set; }
        public string SpeciesName { get; set; }
        public string CareerName { get; set; }

        public Characteristics Characteristics { get; set; }
        public Armour EquippedArmour { get; set; }
        public List<Weapon> EquippedWeapons { get; set; }

        #region Wound+Strain Thresholds
        private readonly int _startingWoundThreshold;
        internal int WoundThresholdBonus { get; set; }
        private readonly int _startingStrainThreshold;
        internal int StrainThresholdBonus { get; set; }
        public int WoundThreshold => _startingWoundThreshold + WoundThresholdBonus;
        public int StrainThreshold => _startingStrainThreshold + StrainThresholdBonus;
        #endregion

        #region Soak, Defense (including armour)
        public int Soak => Characteristics.Brawn + (EquippedArmour?.SoakValue ?? 0);

        private int _permanentMeleeDefense;
        private int _permanentRangedDefense;
        public int MeleeDefense => _permanentMeleeDefense + (EquippedArmour == null ? 0 : EquippedArmour.MeleeDefense);
        public int RangedDefense => _permanentRangedDefense + (EquippedArmour?.RangedDefense ?? 0);//Equivalent, slightly slower than above
        #endregion
    }

    public enum RangeBand
    {
        Engaged,
        Short,
        Medium,
        Long
    }
}
