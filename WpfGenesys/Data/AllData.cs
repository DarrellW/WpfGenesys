using System;
using System.Collections.Generic;
using System.Linq;
using WpfGenesys.Models;

namespace WpfGenesys.Data
{
    public static class AllData
    {
        public static Stats BaseStats { get; set; }
        public static Skills BaseSkills { get; set; }
        public static List<Equipment> Inventory { get; set; }
        public static TalentTable Talents { get; set; }

        public static (Stats Stats, Skills Skills, IEnumerable<Weapon>, IEnumerable<Armour>, IEnumerable<SituationalAbility>) GetDisplayedStats()
        {
            // Get all data
            Stats stats = (Stats)BaseStats.Clone();
            Skills skills = (Skills)BaseSkills.Clone();
            // TODO: Make copies of equipment
            List<Weapon> weapons = new();
            List<Armour> armour = new();
            IEnumerable<Talent> talents = Talents.GetAllTalents();
            List<SituationalAbility> textAbilities = new();
            List<Modifier> modifiers = new();

            // Extract item modifiers and abilities (effects) from equipment. TODO: IS THIS REQUIRED? Applies modifiers that would affect the equipment in question.
            // eg. A mace (Brawn+3 damage) with the Weighted Head attachment will be listed as Brawn+5 damage, and its Cumbersome quality incremented.
            foreach (Equipment item in Inventory.Where(x => x.IsEquipped))
            {
                EffectExtractor extractor = new(item);
                if (extractor.Equipment is Weapon wep)
                    weapons.Add(wep);
                else if (extractor.Equipment is Armour arm)
                    armour.Add(arm);
                textAbilities.AddRange(extractor.SituationalAbilities);
                modifiers.AddRange(extractor.Modifiers);
            }

            // Apply modifiers (from equipment and talents)
            foreach (Modifier modifier in modifiers.OrderBy(x => x is StatModifier ? 1 : 2))
            {
                if (modifier is SkillModifier skillMod)
                {
                    Skill sk = skills.Find(skillMod.TargetName);
                    
                    // TODO: check that these values are changing
                    sk.Rank += skillMod.Value;
                    if (skillMod.MakeCareer)
                        sk.IsCareer = true;
                    // TODO: Instead of changing the derived characteristic, add the option and pick the greater roll.
                    if (skillMod.NewDerivedCharacteristic != null)
                        sk.DerivedCharacteristic = (Characteristic)skillMod.NewDerivedCharacteristic;
                    if (!string.IsNullOrWhiteSpace(skillMod.Dice))
                    {
                        IEnumerable<char> chars = skillMod.Dice.ToLower().Distinct();
                        string str = skillMod.Dice;//.ToLower(); nuh
                        var uniqueCount = str.GroupBy(x => x).Select(g => new KeyValuePair<char, int>(g.Key, g.Count())).ToDictionary(x => x.Key, x => x.Value);
                        //switch

                        skillMod.Dice.Count(x => x == 'a');

                        //uniqueCount.First(x=>x.Key == 'y')
                        
                    }
                }
                else if (modifier is WeaponModifier wepMod)
                {
                    // TODO: 
                    for (int i = 0, len = weapons.Count; i < len; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(wepMod.WeaponType) && wepMod.WeaponType != weapons[i].WeaponType)
                            continue;

                        switch (wepMod.TargetName)
                        {
                            case nameof(Weapon.Damage):
                                weapons[i].BaseDamage += wepMod.Value;
                                break;
                            case nameof(Weapon.CriticalRating):
                                weapons[i].CriticalRating += wepMod.Value;
                                break;
                            case nameof(Weapon.Range):// TODO: Check this
                                weapons[i].Range = (RangeBand)wepMod.Value;
                                break;
                            case nameof(Weapon.SkillEmployed):// TODO: instead of storing "Skill", could store skill ID and use wepMod.Value
                                Skill sk = skills.Find(wepMod.SkillName);
                                weapons[i].SkillEmployed = sk;
                                break;
                            case nameof(Weapon.Defense):
                                weapons[i].BaseMeleeDefense += wepMod.Value;
                                weapons[i].BaseRangedDefense += wepMod.Value;
                                break;
                            case nameof(Weapon.BaseMeleeDefense):
                                weapons[i].BaseMeleeDefense += wepMod.Value;
                                break;
                            case nameof(Weapon.BaseRangedDefense):
                                weapons[i].BaseRangedDefense += wepMod.Value;
                                break;
                            case nameof(Weapon.BaseHardPoints):
                                weapons[i].BaseHardPoints += wepMod.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (modifier is ArmourModifier armMod)
                {
                    for (int i = 0, len = armour.Count; i < len; i++)
                    {
                        switch (armMod.TargetName)
                        {
                            case nameof(Armour.BaseSoak):// TODO: Change how armour, weapons, etc. handle their own data, or make "base" stats private or something and actually make "bonus" stats public.
                                armour[i].BaseSoak += armMod.Value;
                                break;
                            case nameof(Armour.Defense):
                                armour[i].BaseMeleeDefense += armMod.Value;
                                armour[i].BaseRangedDefense += armMod.Value;
                                break;
                            case nameof(Armour.BaseMeleeDefense):
                                armour[i].BaseMeleeDefense += armMod.Value;
                                break;
                            case nameof(Armour.BaseRangedDefense):
                                armour[i].BaseRangedDefense += armMod.Value;
                                break;
                            case nameof(Armour.BaseHardPoints):
                                armour[i].BaseHardPoints += armMod.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (modifier is StatModifier statMod)
                {
                    switch (statMod.TargetName)
                    {
                        case nameof(Characteristic.Brawn):
                            stats.Characteristics.Brawn += statMod.Value;
                            break;// TODO: Let UI handle or goto case "Soak";?
                        case nameof(Characteristic.Agility):
                            stats.Characteristics.Agility += statMod.Value;
                            break;
                        case nameof(Characteristic.Intellect):
                            stats.Characteristics.Intellect += statMod.Value;
                            break;
                        case nameof(Characteristic.Cunning):
                            stats.Characteristics.Cunning += statMod.Value;
                            break;
                        case nameof(Characteristics.Willpower):
                        case nameof(Characteristic.Will):
                            stats.Characteristics.Willpower += statMod.Value;
                            break;
                        case nameof(Characteristic.Presence):
                            stats.Characteristics.Presence += statMod.Value;
                            break;
                        case nameof(Stats.WoundThreshold):
                            stats.WoundThreshold += statMod.Value;
                            break;
                        case nameof(Stats.StrainThreshold):
                            stats.StrainThreshold += statMod.Value;
                            break;
                        case nameof(Stats.Defense):
                            stats.MeleeDefense += statMod.Value;
                            stats.RangedDefense += statMod.Value;
                            break;
                        case nameof(Stats.MeleeDefense):
                            stats.MeleeDefense += statMod.Value;
                            break;
                        case nameof(Stats.RangedDefense):
                            stats.RangedDefense += statMod.Value;
                            break;
                        case nameof(Stats.Soak)://Few modifiers will affect the soak value directly. eg. Armour master or w/e
                            stats.Soak += statMod.Value;
                            break;
                        default:
                            break;
                    }
                }
            }

            // TODO: I do not have to store soak. If I do, then base soak will include base brawn, so a modifier on brawn will also modifier soak (see StatsModifier above). If I don't, then do not modify soak when modifying brawn: just add brawn here... or do that from UI?
            stats.Soak += armour.Sum(x => x.SoakValue);// + stats.Characteristics.Brawn;
            stats.MeleeDefense += armour.Sum(x => x.MeleeDefense) + weapons.Sum(x => x.MeleeDefense);
            stats.RangedDefense += armour.Sum(x => x.RangedDefense) + weapons.Sum(x => x.RangedDefense);

            return (stats, skills, weapons, armour, textAbilities);
        }
    }


    public class EffectExtractor
    {
        public List<SituationalAbility> SituationalAbilities { get; private set; }
        public List<Modifier> Modifiers { get; private set; }
        public Equipment Equipment { get; private set; }

        public EffectExtractor(Equipment equipment)
        {
            var effects = equipment.ItemAttachments.Select(x => x.Effects2);
            //equipment.Qualities

            SituationalAbilities = new List<SituationalAbility>();
            Modifiers = new List<Modifier>();
            Equipment = equipment.Clone();

            foreach(IEffect effect in effects)
            {
                if (effect is SituationalAbility ability)
                    SituationalAbilities.Add(ability);
                else if (effect is Modifier modifier)
                {
                    Modifiers.Add(modifier);
                    //if (!modifier.TargetSelf)
                    //{
                    //    Modifiers.Add(modifier);
                    //    continue;
                    //}

                    ////apply to self
                    //if (modifier.Target == Modifier.TargetType.Damage && equipment is Weapon weapon)
                    //{
                    //    // TODO:
                    //    weapon.BaseDamage += modifier.Value;
                    //}
                }
            }
        }
    }

    public interface IEffect
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SituationalAbility : IEffect
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public abstract class Modifier : IEffect
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public string TargetName { get; set; }

        //public enum TargetType { MeleeDefense, RangedDefense, Soak, Damage, }
    }

    public class WeaponModifier : Modifier
    {
        public string WeaponType { get; set; }
        public string SkillName { get; set; }
    }

    public class ArmourModifier : Modifier
    {

    }

    public class StatModifier : Modifier
    {

    }

    public class SkillModifier : Modifier
    {
        public string Dice { get; set; }
        public bool MakeCareer { get; set; }
        public Characteristic? NewDerivedCharacteristic { get; set; }
    }

    public class TalentTable
    {
        private List<Talent>[] _tree;

        public List<Talent> this[int tier]
        {
            get
            {
                if (tier < 1 || tier > 5)
                    throw new ArgumentOutOfRangeException("Only tiers 1-5 exist.");
                return _tree[tier - 1];
            }
        }
        /// <summary>
        /// Add talent to tree. The previous tier must have at least 2 more talents.
        /// Note that it is possible to have multiple tier 5 talents with the same name but different rank.
        /// </summary>
        /// <param name="talent"></param>
        /// <returns>True if there is room, False otherwise.</returns>
        public bool AddTalent(Talent talent)
        {
            int i = talent.TrueTier - 1;
            if (i > 0 && _tree[i - 1].Count <= _tree[i].Count + 1)
                return false;

            _tree[i].Add(talent);
            return true;
        }

        public bool RemoveTalent(Talent talent)
        {
            int i = talent.TrueTier - 1;
            if (!_tree[i].Contains(talent))
                return false;

            _tree[i].Remove(talent);
            return true;
        }

        /// <summary>
        /// Get all unique talents. Only the highest rank of a ranked talent is returned.
        /// </summary>
        /// <returns></returns>
        public List<Talent> GetAllTalents()
        {
            List<Talent> newList = new();

            for (int i = 0; i < _tree.Length; i++)
            {
                newList.AddRange(_tree[i]);
            }

            foreach(var talent in newList.OrderByDescending(x => x.Rank).ThenByDescending(x => x.Tier).Distinct())
            {
                newList.RemoveAll(x => x.Name == talent.Name && x.Rank != talent.Rank);
            }

            return newList;
        }
    }

    public class Talent : IEquatable<Talent>
    {
        public int Tier { get; set; }
        public int Rank { get; set; }
        public bool CanRank { get; set; }
        public int TrueTier
        {
            get
            {
                int tier = Tier + Rank - 1;
                return (tier > 5) ? 5 : tier;
            }
        }
        public string Name { get; set; }

        public bool Equals(Talent other)
        {
            if (other is null) return false;

            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Talent);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * 10 + Rank;
        }
        // something here for what it affects
    }

    public class Stats : ICloneable
    {
        public Characteristics Characteristics { get; set; }

        public int WoundThreshold { get; set; }
        public int StrainThreshold { get; set; }

        // TODO:
        public int Defense { set { MeleeDefense = value; RangedDefense = value; } }

        public int MeleeDefense { get; set; }
        public int RangedDefense { get; set; }
        public int Soak { get; set; }

        public object Clone()
        {
            return new Stats { Characteristics = (Characteristics)Characteristics.Clone() };
        }
    }

    public class Skills : ICloneable
    {
        public Skill Find(string str)
        {
            return new Models.Skills()[str];
        }

        public object Clone()
        {
            return new Skills();
        }
    }

    public class Weapons
    {

    }
}
