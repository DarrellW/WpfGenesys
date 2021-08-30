using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGenesys.Models
{
    public class Skill
    {
        public string Name { get; set; }//if Name == null, choose
        public Characteristic DerivedCharacteristic { get; set; }
        public bool IsCombat { get; set; }
        public bool IsCareer { get; set; }
        public int Rank { get; set; }
        public string Category { get; set; }
        public string Modifiers { get; set; }

        /// <summary>
        /// Call from page or method caller thing in xaml using DataContext
        /// </summary>
        /// <param name="characteristics"></param>
        /// <returns></returns>
        public string GetRoll(Characteristics characteristics)
        {
            // TODO: 
            int characValue;
            if (DerivedCharacteristic == Characteristic.Brawn)
                characValue = characteristics.Brawn;
            else if (DerivedCharacteristic == Characteristic.Agility)
                characValue = characteristics.Agility;
            else if (DerivedCharacteristic == Characteristic.Intellect)
                characValue = characteristics.Intellect;
            else if (DerivedCharacteristic == Characteristic.Cunning)
                characValue = characteristics.Cunning;
            else if (DerivedCharacteristic == Characteristic.Will)
                characValue = characteristics.Willpower;
            else
                characValue = characteristics.Presence;

            int max, min;
            if (characValue > Rank)
            {
                max = characValue;
                min = Rank;
            }
            else
            {
                min = characValue;
                max = Rank;
            }

            string str = "";

            for (int i = 0; i < min; i++)
            {
                //add yellow
                str += "y";
            }
            for (int i = min; i < max; i++)
            {
                //add green
                str += "g";
            }

            //end of base shit
            // > for upgrade, < for downgrade
            // y for yellows, r for reds
            // g for greens, p for purples
            // b for blues, s for blacks
            // +/o for success, x for failure
            // v/a for advantage, q for threat
            // t/* for triumph, p for despair
            foreach (char character in Modifiers.Where(x => x == 'u'))
            {
                str = str.Replace('g', 'y');
            }
            Modifiers = Modifiers.Replace("u", "");
            return str + Modifiers;
        }
    }

    [Serializable]
    public enum Characteristic
    {
        Brawn,
        Agility,
        Intellect,
        Cunning,
        Will,
        Presence
    }

    public class Skills
    {
        List<Skill> _skills;
        public Skills()
        {
            _skills = new List<Skill>
            {
                new Skill { Name = "Alchemy", DerivedCharacteristic = Characteristic.Intellect, IsCombat = false, Category = "General" },
                new Skill { Name = "Athletics", DerivedCharacteristic = Characteristic.Brawn, IsCombat = false, Category = "General" },
                new Skill { Name = "Cool", DerivedCharacteristic = Characteristic.Presence, IsCombat = false, Category = "General" },
                new Skill { Name = "Coordination", DerivedCharacteristic = Characteristic.Agility, IsCombat = false, Category = "General" },
                new Skill { Name = "Discipline", DerivedCharacteristic = Characteristic.Will, IsCombat = false, Category = "General" },
                new Skill { Name = "Mechanics", DerivedCharacteristic = Characteristic.Intellect, IsCombat = false, Category = "General" },
                new Skill { Name = "Medicine", DerivedCharacteristic = Characteristic.Intellect, IsCombat = false, Category = "General" },
                new Skill { Name = "Perception", DerivedCharacteristic = Characteristic.Cunning, IsCombat = false, Category = "General" },
                new Skill { Name = "Resilience", DerivedCharacteristic = Characteristic.Brawn, IsCombat = false, Category = "General" },
                new Skill { Name = "Riding", DerivedCharacteristic = Characteristic.Agility, IsCombat = false, Category = "General" },
                new Skill { Name = "Skulduggery", DerivedCharacteristic = Characteristic.Cunning, IsCombat = false, Category = "General" },
                new Skill { Name = "Stealth", DerivedCharacteristic = Characteristic.Agility, IsCombat = false, Category = "General" },
                new Skill { Name = "Streetwise", DerivedCharacteristic = Characteristic.Cunning, IsCombat = false, Category = "General" },
                new Skill { Name = "Survival", DerivedCharacteristic = Characteristic.Cunning, IsCombat = false, Category = "General" },
                new Skill { Name = "Vigilance", DerivedCharacteristic = Characteristic.Will, IsCombat = false, Category = "General" },

                new Skill { Name = "Arcana", DerivedCharacteristic = Characteristic.Intellect, IsCombat = true, Category = "Magic" },
                new Skill { Name = "Divine", DerivedCharacteristic = Characteristic.Will, IsCombat = true, Category = "Magic" },
                new Skill { Name = "Primal", DerivedCharacteristic = Characteristic.Cunning, IsCombat = true, Category = "Magic" },
                new Skill { Name = "Runes", DerivedCharacteristic = Characteristic.Intellect, IsCombat = true, Category = "Magic" },
                new Skill { Name = "Verse", DerivedCharacteristic = Characteristic.Presence, IsCombat = true, Category = "Magic" },

                new Skill { Name = "Brawl", DerivedCharacteristic = Characteristic.Brawn, IsCombat = true, Category = "Combat" },
                new Skill { Name = "Melee-Heavy", DerivedCharacteristic = Characteristic.Brawn, IsCombat = true, Category = "Combat" },
                new Skill { Name = "Melee-Light", DerivedCharacteristic = Characteristic.Brawn, IsCombat = true, Category = "Combat" },
                new Skill { Name = "Ranged", DerivedCharacteristic = Characteristic.Agility, IsCombat = true, Category = "Combat" },

                new Skill { Name = "Charm", DerivedCharacteristic = Characteristic.Presence, IsCombat = false, Category = "Social" },
                new Skill { Name = "Coercion", DerivedCharacteristic = Characteristic.Will, IsCombat = false, Category = "Social" },
                new Skill { Name = "Deception", DerivedCharacteristic = Characteristic.Cunning, IsCombat = false, Category = "Social" },
                new Skill { Name = "Leadership", DerivedCharacteristic = Characteristic.Presence, IsCombat = false, Category = "Social" },
                new Skill { Name = "Negotiation", DerivedCharacteristic = Characteristic.Presence, IsCombat = false, Category = "Social" },

                new Skill { Name = "Adventuring", DerivedCharacteristic = Characteristic.Intellect, IsCombat = false, Category = "Knowledge" },
                new Skill { Name = "Forbidden", DerivedCharacteristic = Characteristic.Intellect, IsCombat = false, Category = "Knowledge" },
                new Skill { Name = "Lore", DerivedCharacteristic = Characteristic.Intellect, IsCombat = false, Category = "Knowledge" },
                new Skill { Name = "Geography", DerivedCharacteristic = Characteristic.Intellect, IsCombat = false, Category = "Knowledge" },


            };
        }
        public List<Skill> GetSkills()
        {
            return _skills;
        }
        public IEnumerable<string> GetNames()
        {
            return _skills.Select(x => x.Name);
        }
        public Skill this[string name]
        {
            get
            {
                return _skills.Single(x => x.Name == name);
            }
        }

        public bool CreateSkill(string name, Characteristic derivedCharacteristic, bool isCombat, string category = "Custom")
        {
            if (_skills.Exists(x => x.Name == name))
                return false;

            _skills.Add(new Skill
            {
                Name = name,
                DerivedCharacteristic = derivedCharacteristic,
                IsCombat = isCombat,
                Category = category
            });
            return true;
        }

        public bool RemoveSkill(string name)
        {
            if (!_skills.Exists(x => x.Name == name))
                return false;

            _skills.RemoveAll(x => x.Name == name);
            return true;
        }
    }
}
