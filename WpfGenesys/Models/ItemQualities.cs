using System;
using System.Collections.Generic;

namespace WpfGenesys.Models
{
    public enum ItemQuality
    {
        Accurate,   // +1 blue die per rank
        Blast,      // +rank damage, 2 advantage to activate aoe
        Breach,     // ignore rank points of vehicle armour / ignore 10*rank soak
        Burn,       // target takes weapon damage at the start of their turn for rank turns, 2 advantage to activate
        Concussive, // target staggered (can't perform actions) for rank # of rounds, 2 advantage to activate
        Cumbersome, // increase difficulty of checks if brawn < cumbersome
        Defensive,  // +1 melee defense per rank
        Deflection, // +1 ranged defense per rank
        Disorient,  // target is disoriented (add [black] to skill checks) for rank # of rounds, 2 advantage to activate
        Ensnare,    // target immobilized for rank # of rounds, 2 advantage to activate
        Inaccurate, // +1 black die per rank
        Inferior,   // no ranks, add threat to skill checks with weapon
        Knockdown,  // no ranks, 1 advantage to activate
        LimitedAmmo,// self-explanatory
        Linked,     // may spend 2 advantage up to rank times to do an additional hit
        Pierce,     // ignore rank soak when doing damage
        Prepare,    // must perform rank maneuvers before using the item
        Reinforced, // no ranks, weapon: immune to Sunder quality, armour: immune to pierce and breach
        Stun,       // inflict rank strain, 2 advantage to activate
        StunDamage, // no ranks, weapon deals strain damage (reduced by soak)
        Sunder,     // no ranks, damage an item wielded by the target one step (not armour), 1 advantage to activate (can activate multiple times)
        Superior,   // no ranks, 1 advantage to skill checks with weapon
        Unwieldy,   // increase difficulty of checks if agility < unwieldy
        Vicious     // +10*rank to critical hits
    }

    /// <summary>
    /// Stores all item qualities for one item.
    /// </summary>
    public sealed class ItemQualities
    {
        private Dictionary<ItemQuality, int> _qualities = new();

        public int this[ItemQuality quality]
        {
            get
            {
                if (_qualities.ContainsKey(quality))
                    return _qualities[quality];
                return _qualities[quality];
            }
            set
            {
                if (!_qualities.ContainsKey(quality))
                {
                    _qualities.Add(quality, value);
                    return;
                }

                _qualities[quality] = value;
            }
        }

        public ItemQualities Clone()
        {
            return new ItemQualities { _qualities = new Dictionary<ItemQuality, int>(_qualities) };
        }

        /// <summary>
        /// Adds a quality to an item, or adds ranks to the quality if it already exists.
        /// </summary>
        /// <param name="quality">Item quality.</param>
        /// <param name="rank">Rank of the quality, if applicable.</param>
        /// <returns>Returns true if the item did not already have the quality, false otherwise.</returns>
        public bool AddQuality(ItemQuality quality, int rank = 0)
        {
            if (!_qualities.ContainsKey(quality))
            {
                _qualities.Add(quality, rank);
                return true;
            }

            _qualities[quality] += rank;
            return false;
        }

        /// <summary>
        /// Removes a quality from an item (and thus all ranks in it).
        /// </summary>
        /// <param name="quality">Item quality.</param>
        /// <returns>Returns true if the quality was removed, false otherwise.</returns>
        public bool RemoveQuality(ItemQuality quality)
        {
            if (_qualities.ContainsKey(quality))
            {
                _qualities.Remove(quality);
                return true;
            }

            return false;
        }
    }
}
