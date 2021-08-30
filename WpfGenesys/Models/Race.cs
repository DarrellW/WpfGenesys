using System.Collections.Generic;

namespace WpfGenesys.Models
{
    public class Race
    {
        public Race(string name, Characteristics characteristics, int woundThreshold, int strainThreshold, int experience)
        {
            Name = name;
            BaseCharacteristics = characteristics;
            BaseWoundThreshold = woundThreshold;
            BaseStrainThreshold = strainThreshold;
            StartingExperience = experience;
        }
        public Race() { }

        internal int Id;
        public string Name { get; set; }
        public Race ParentRace { get; set; }
        public Characteristics BaseCharacteristics { get; set; }
        public int BaseWoundThreshold { get; set; }
        public int BaseStrainThreshold { get; set; }
        public int StartingExperience { get; set; }
        public List<Skill> RacialSkills { get; set; }
        public List<string> RacialAbilities { get; set; }

        public bool HasNoSubRaces
        {
            get { return SubRaces == null || SubRaces.Count <= 0; }
        }
        public bool HasSubRaces { get { return !HasNoSubRaces; } }
        public bool HasRacialSkills => RacialSkills != null && RacialSkills.Count > 0;
        public bool HasRacialAbilities => RacialAbilities != null && RacialAbilities.Count > 0;

        public List<Race> SubRaces { get; set; }

        public Race Copy()
        {
            // Copy over the data. Do not pass reference in case a change is made to either race... except the list of sub races
            var skills = new List<Skill>();
            foreach (Skill skill in RacialSkills)
            {
                skills.Add(new Skill
                {
                    Name = skill.Name,
                    Category = skill.Category,
                    DerivedCharacteristic = skill.DerivedCharacteristic,
                    IsCareer = skill.IsCareer,
                    IsCombat = skill.IsCombat,
                    Rank = skill.Rank
                });
            }
            var abilities = new List<string>();
            foreach (string ability in RacialAbilities)
            {
                abilities.Add(ability);
            }

            return new Race()
            {
                Name = Name,
                BaseCharacteristics = BaseCharacteristics,
                BaseWoundThreshold = BaseWoundThreshold,
                BaseStrainThreshold = BaseStrainThreshold,
                StartingExperience = StartingExperience,
                RacialSkills = skills,
                RacialAbilities = abilities,
                SubRaces = SubRaces
            };
        }
    }

    [System.Serializable]
    internal struct SpeciesInfo
    {
        public string Name;
        public string SpeciesParentName;
        public int Characteristics;
        public int Wound;
        public int Strain;
        public int Experience;
    }
}
