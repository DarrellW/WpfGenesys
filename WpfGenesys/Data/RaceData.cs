using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfGenesys.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGenesys.Data
{
    public class RaceData : ObservableCollection<Race>
    {
        public RaceData()
        {
            int id = 0;
            Add(new Race
            {
                Id = id++,
                Name = "Human",
                BaseCharacteristics = new Characteristics(),
                StartingExperience = 110,
                BaseStrainThreshold = 10,
                BaseWoundThreshold = 10,
                RacialAbilities = new List<string> { "Once per session as an out-of-turn incidental, a Human may move one Story Point from the Game Master's pool to the players' pool." },
                RacialSkills = new List<Skill> { new Skill { Name = "Non-career", Rank = 2 } }
            });
            var elf = new Race
            {
                Id = id++,
                Name = "Elf",
                BaseCharacteristics = new Characteristics() { Agility = 3, Willpower = 1 },
                StartingExperience = 90,
                BaseStrainThreshold = 9,
                BaseWoundThreshold = 10,
                SubRaces = new List<Race>
                {
                    new Race
                    {
                        Name = "Deep Elf",
                        RacialSkills = new List<Skill>
                        {
                            new Skill { Name = "Discipline", Rank = 1 },
                            new Skill { Name = "Forbidden", Rank = 2, IsCareer = true }
                        },
                        Id = id++
                    },
                    new Race
                    {
                        Name = "Free Cities Elf",
                        RacialSkills = new List<Skill>
                        {
                            new Skill { Name = "Streetwise", Rank = 1 }
                        },
                        RacialAbilities = new List<string>
                        {
                            "Melee and Ranged defense of 1."
                        },
                        Id = id++
                    },
                    new Race
                    {
                        Name = "Highborn Elf",
                        RacialSkills = new List<Skill>
                        {
                            new Skill { Name = "Negotiation" , Rank = 1 },
                            new Skill { Name = "Divine", Rank = 1, IsCareer = true }
                        },
                        Id = id++
                    },
                    new Race
                    {
                        Name = "Lowborn Elf",
                        RacialAbilities = new List<string>
                        {
                            "Melee and Ranged defense of 1."
                        },
                        RacialSkills = new List<Skill>
                        {
                            new Skill { Name = "Survival", Rank = 1 }
                        },
                        Id = id++
                    }
                }
            };
            foreach (var sub in elf.SubRaces)
                sub.ParentRace = elf;
            Add(elf);

            var dwarf = new Race
            {
                Id = id++,
                Name = "Dwarf",
                StartingExperience = 90,
                BaseWoundThreshold = 11,
                BaseStrainThreshold = 10,
                BaseCharacteristics = new Characteristics() { Agility = 1, Willpower = 3 },
                SubRaces = new List<Race>
                {
                    new Race
                    {
                        Id = id++,
                        Name = "Dunwarr Dwarf",
                        RacialSkills = new List<Skill>
                        {
                            new Skill { Name = "Resilience", Rank = 1 }
                        },
                        RacialAbilities = new List<string>
                        {
                            "When making skill checks, Dunwarr Dwarves remove up to 2 [black] imposed due to darkness.",
                            "Once per session, a Dwarf may spend a Story Point as an out-of-turn incidental immediately after suffering a Critical Injury and determining the result. If they do so, they count the result rolled as “01.”"
                        }
                    },
                    new Race
                    {
                        Id = id++,
                        Name = "Forge Dwarf",
                        RacialSkills = new List<Skill>
                        {
                            new Skill { Name = "Negotiation", Rank = 1 }
                        },
                        RacialAbilities = new List<string>
                        {
                            "Forge Dwarves add [black] to social skill checks targeting them.",
                             "Once per session, a Dwarf may spend a Story Point as an out-of-turn incidental immediately after suffering a Critical Injury and determining the result. If they do so, they count the result rolled as “01.”"
                        }
                    }
                }
            };
            foreach (var sub in dwarf.SubRaces)
                sub.ParentRace = dwarf;
            Add(dwarf);

            Add(new Race
            {
                Id = id++,
                Name = "Orc",
                StartingExperience = 100,
                BaseWoundThreshold = 12,
                BaseStrainThreshold = 8,
                BaseCharacteristics = new Characteristics() { Brawn = 3, Presence = 1}
            });
        }
    }

    public class RaceData2
    {
        private List<Race> _data;
        public List<Race> GetData()
        {
            if (_data != null)
                return _data;

            //otherwise load from file
            throw new NotImplementedException();
        }
    }

    // Actually this should be a Dictionary/Resource
    public class Ability
    {
        KeyValuePair<string, string> fuck;

        string Name;
        string Description;
        //public event  Idk;
    }
}
