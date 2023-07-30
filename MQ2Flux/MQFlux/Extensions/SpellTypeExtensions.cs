using MQ2DotNet.EQ;
using MQ2DotNet.MQ2API.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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

    public class BuffsNotPopulatedException : ApplicationException
    {
        public BuffsNotPopulatedException()
        {
        }

        public BuffsNotPopulatedException(string message) : base(message)
        {
        }

        public BuffsNotPopulatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BuffsNotPopulatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// TODO: move these to the <see cref="SpellType"/> class when they have been tested.
    /// </summary>
    public static class SpellTypeExtensions
    {
        public static IEnumerable<IGrouping<string, SpellType>> GroupBySpellLine(this IEnumerable<SpellType> spells)
        {
            return spells.GroupBy(i => $"{i.CategoryName}_{i.Subcategory}_{i.SpellIcon}_{i.TargetCategory}");
        }

        public static IEnumerable<SpellType> HighestLevelSpellLineSpells(this IEnumerable<IGrouping<string, SpellType>> spellsLines)
        {
            return spellsLines.Select
            (
                i => i
                    .OrderByDescending(j => j.Level.GetValueOrDefault(0))
                    .First()
            );
        }

        public static IEnumerable<SpellType> HighestLevelSpellLineSpells(this IEnumerable<SpellType> spells)
        {
            return spells
                .GroupBySpellLine()
                .HighestLevelSpellLineSpells();
        }

        public static IEnumerable<SpellType> AreaOfEffectGroupBuffs(this IEnumerable<SpellType> spells)
        {
            return spells.Where
            (
                i =>
                    i.Beneficial &&
                    i.TargetCategory == TargetCategory.Group_v1 ||
                    i.TargetCategory == TargetCategory.Group_v2
            );
        }

        public static IEnumerable<SpellType> SingleTargetFriendlyBuffs(this IEnumerable<SpellType> spells)
        {
            return spells.Where
            (
                i =>
                    i.Beneficial &&
                    (
                        i.TargetCategory == TargetCategory.Single_Friendly_or_Targets_Target ||
                        i.TargetCategory == TargetCategory.Single
                    )
            );
        }

        public static IEnumerable<SpellType> SingleTargetGroupBuffs(this IEnumerable<SpellType> spells)
        {
            return spells.Where
            (
                i =>
                    i.Beneficial &&
                    (
                        i.TargetCategory == TargetCategory.Single_in_Group ||
                        i.TargetCategory == TargetCategory.Single_Friendly_or_Targets_Target ||
                        i.TargetCategory == TargetCategory.Single
                    )
                );
        }

        public static IEnumerable<SpellType> SelfBuffs(this IEnumerable<SpellType> spells)
        {
            return spells.Where
            (
                i =>
                    i.Beneficial &&
                    i.TargetCategory == TargetCategory.Self
            );
        }

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
                throw new BuffsNotPopulatedException("The buffs are not populated yet for spawn");
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
            if (spell1.Name.Contains("Endure") && spell2.Name.Contains("Resist") && spell1.Name.Replace("Endure ", string.Empty) == spell2.Name.Replace("Resist ", string.Empty))
            {
                return false;
            }

            if (spell1.Name == "Furious Strength" && spell2.Name == "Tumultuous Strength")
            {
                return false;
            }

            if (spell1.Name == "Strength of Stone" && spell2.Name == "Tumultuous Strength")
            {
                return false;
            }

            if (spell1.Name == "Strength of Stone" && spell2.Name == "Furious Strength")
            {
                return false;
            }

            if (spell1.Name == "Armor of Protection" && spell2.Name == "Talisman of Altuna")
            {
                return false;
            }

            if (spell1.Name == "Pack Regeneration" && spell2.Name == "Chloroplast")
            {
                return false;
            }

            if (spell1.Name == "Regeneration" && spell2.Name == "Chloroplast")
            {
                return false;
            }

            if (spell1.Name == "Protection of Diamond" && spell2.Name == "Temperance")
            {
                return false;
            }

            return true;
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
