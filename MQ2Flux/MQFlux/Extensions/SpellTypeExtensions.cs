using MQ2DotNet.EQ;
using MQ2DotNet.MQ2API.DataTypes;
using System;
using System.Collections.Generic;

namespace MQFlux.Extensions
{
    public readonly struct ReagentInfo
    {
        public uint ItemID { get; }
        public uint RequiredCount { get; }

        public ReagentInfo(uint itemID, uint count)
        {
            ItemID = itemID;
            RequiredCount = count;
        }
    }

    /// <summary>
    /// TODO: move these to the <see cref="SpellType"/> class when they have been tested.
    /// </summary>
    public static class SpellTypeExtensions
    {
        public static bool WillLand(this SpellType spell, CharacterType me)
        {
            if (!spell.WillLand)
            {
                return false;
            }

            foreach (var buff in me.Buffs)
            {
                if (!WillLand(spell, buff.Spell))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool WillLand(this SpellType spell, SpawnType spawn)
        {
            if (!spawn.BuffsPopulated)
            {
                throw new ArgumentException("The buffs are not populated yet for spawn", nameof(spawn));
            }

            if (!spell.StacksTarget)
            {
                return false;
            }

            foreach (var buff in spawn.Buffs)
            {
                if (!WillLand(spell, buff.Spell))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Does spell 1 stack with spell 2.
        /// Checks for exceptions where <see cref="SpellType.WillLand"/> and <see cref="SpellType.StacksTarget"/> returns <see cref="true"/> incorrectly.
        /// </summary>
        /// <param name="spell1"></param>
        /// <param name="spell2"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static bool WillLand(SpellType spell1, SpellType spell2)
        {
            if (spell1.Name.Contains("Endure") && spell2.Name.Contains("Resist"))
            {
                return false;
            }

            if (spell1.Name.Contains("Strength of Stone") && spell2.Name.Contains("Furious Strength"))
            {
                return false;
            }

            if (spell1.Name.Contains("Armor of Protection") && spell2.Name.Contains("Talisman of Altuna"))
            {
                return false;
            }

            if (spell1.Name.Contains("Pack Regeneration") && spell2.Name.Contains("Chloroplast"))
            {
                return false;
            }

            return true;
        }

        public static SpellCategory GetSpellCategory(this SpellType spell)
        {
            return (SpellCategory)spell.CategoryID.GetValueOrDefault(uint.MaxValue);
        }

        public static IEnumerable<ReagentInfo> GetReagents(this SpellType spell)
        {
            for (int i = 1; i <= 4; i++)
            {
                var itemID = spell.GetReagentID(i).GetValueOrDefault(0u);

                if (itemID > 0u && itemID < uint.MaxValue)
                {
                    var count = spell.GetReagentCount(i).GetValueOrDefault(0u);

                    yield return new ReagentInfo(itemID, count);
                }
                else
                {
                    itemID = spell.GetNoExpendReagentID(i).GetValueOrDefault(0u);

                    if (itemID > 0u && itemID < uint.MaxValue)
                    {
                        var count = spell.GetReagentCount(i).GetValueOrDefault(0u);

                        yield return new ReagentInfo(itemID, count);
                    }
                }
            }
        }

        public static bool HasCastTime(this SpellType spell)
        {
            return spell.CastTime.HasValue && spell.CastTime.Value > TimeSpan.Zero;
        }
    }
}
