using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfGenesys.Models
{
    public interface IItem
    {
        public string Name { get; }
        public int Encumberance { get; }
        public int Price { get; }
        public int Rarity { get; }
    }

    public abstract class Equipment : IItem
    {
        public bool IsEquipped { get; set; }
        public string Name { get; set; }
        public int Encumberance { get; set; }
        public int Price { get; set; }
        public int Rarity { get; set; }

        #region Base
        internal int BaseMeleeDefense { get; set; }
        internal int BaseRangedDefense { get; set; }
        internal int BaseHardPoints { get; set; }
        #endregion

        // TODO:
        public int Defense { set { BaseMeleeDefense = value; BaseRangedDefense = value; } }

        public virtual int MeleeDefense => BaseMeleeDefense + Qualities[ItemQuality.Defensive];
        public virtual int RangedDefense => BaseRangedDefense + Qualities[ItemQuality.Deflection];
        public virtual int RemainingHardPoints => BaseHardPoints - (ItemAttachments == null ? 0 : ItemAttachments.Sum(x => x.HardPointsNeeded));

        public ItemQualities Qualities { get; set; } = new();
        public List<ItemAttachment> ItemAttachments { get; set; }

        public abstract Equipment Clone();
    }

    public sealed class Armour : Equipment
    {
        #region Base
        internal int BaseSoak { get; set; }
        #endregion

        #region Bonus
        private int _bonusMeleeDefense;
        private int _bonusRangedDefense;
        private int _bonusSoak;
        #endregion

        public override int MeleeDefense =>
            +base.MeleeDefense
            + (ItemAttachments == null ? 0 : ItemAttachments.Count(x => x.Name == "Ironbound Rune"));
        public override int RangedDefense =>
            +base.RangedDefense
            + (ItemAttachments == null ? 0 : ItemAttachments.Count(x => x.Name == "Ironbound Rune" || x.Name == "Deflective Plating")
            + 2 * ItemAttachments.Count(x => x.Name == "Twilight Rune"));
        public int SoakValue =>
            +BaseSoak
            + (ItemAttachments == null ? 0 : ItemAttachments.Count(x => x.Name == "Ironbound Rune"));

        public override Equipment Clone()
        {
            return new Armour
            {
                IsEquipped = IsEquipped,
                Name = Name,
                Encumberance = Encumberance,
                Price = Price,
                Rarity = Rarity,
                BaseMeleeDefense = BaseMeleeDefense,
                BaseRangedDefense = BaseRangedDefense,
                BaseHardPoints = BaseHardPoints,
                Qualities = Qualities.Clone(),
                ItemAttachments = new List<ItemAttachment>(ItemAttachments),

                BaseSoak = BaseSoak
            };
        }
    }

    public sealed class Weapon : Equipment
    {
        // Damage will appear as "+{value}" or simply "{value}", depending on whether or not it is added to Brawn (or other)
        public bool DamageDoesNotIncludeCharacteristic { get; set; }

        #region Base
        public int BaseDamage { get; set; }
        public int BaseCriticalRating { get; set; }
        #endregion

        public int Damage => BaseDamage + (ItemAttachments == null ? 0 :
            + 2 * ItemAttachments.Count(x => x.Name == "Weighted Head")
            + 2 * ItemAttachments.Count(x => x.Name == "Ynfernael Corruption"));

        public int CriticalRating { get; set; }

        public string WeaponType { get; set; }

        public RangeBand Range { get; set; }
        public Skill SkillEmployed { get; set; }

        public override Equipment Clone()
        {
            return new Weapon
            {
                IsEquipped = IsEquipped,
                Name = Name,
                Encumberance = Encumberance,
                Price = Price,
                Rarity = Rarity,
                BaseMeleeDefense = BaseMeleeDefense,
                BaseRangedDefense = BaseRangedDefense,
                BaseHardPoints = BaseHardPoints,
                Qualities = Qualities.Clone(),
                ItemAttachments = new List<ItemAttachment>(ItemAttachments),

                DamageDoesNotIncludeCharacteristic = DamageDoesNotIncludeCharacteristic,
                BaseDamage = BaseDamage,
                BaseCriticalRating = BaseCriticalRating,
                CriticalRating = CriticalRating,
                WeaponType = WeaponType,
                Range = Range,
                SkillEmployed = SkillEmployed
            };
        }
    }

    public sealed class ItemAttachment : IItem
    {
        public string Name { get; set; }
        public int Encumberance => 0;
        public int Price { get; set; }
        public int Rarity { get; set; }

        public int HardPointsNeeded { get; set; }

        public List<Data.IEffect> Effects2 { get; set; }

        public List<Affects> Effects { get; set; }
        public enum Affects { Damage, Soak, /*Defense ->*/ Quality, Skill }
    }
}
