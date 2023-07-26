using MQ2DotNet.EQ;
using MQ2DotNet.MQ2API.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public static class SpellTypeExtensions
    {
        public static readonly SpellCategory[] BuffSpellCategories = new SpellCategory[]
        {
            SpellCategory.AEGOLISM,
            SpellCategory.AGILITY,
            SpellCategory.ARMOR_CLASS,
            SpellCategory.ATTACK,
            SpellCategory.CHARISMA,
            SpellCategory.DAMAGE_SHIELD,
            SpellCategory.DEFENSIVE,
            SpellCategory.DEXTERITY,
            SpellCategory.ENDURANCE,
            SpellCategory.FAST,
            SpellCategory.HASTE,
            SpellCategory.HASTE_SPELL_FOCUS,
            SpellCategory.HEALTH,
            SpellCategory.HEALTH_MANA,
            SpellCategory.HP_BUFFS,
            SpellCategory.HP_TYPE_ONE,
            SpellCategory.HP_TYPE_TWO,
            SpellCategory.LEVITATE,
            SpellCategory.MANA,
            SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            SpellCategory.REGEN,
            SpellCategory.RESIST_BUFF,
            SpellCategory.RUNE,
            SpellCategory.SHIELDING,
            SpellCategory.SPELLSHIELD,
            SpellCategory.SPELL_FOCUS,
            SpellCategory.SPELL_GUARD,
            SpellCategory.STAMINA,
            SpellCategory.STATISTIC_BUFFS,
            SpellCategory.STRENGTH,
            SpellCategory.SYMBOL,
            //SpellCategory.UTILITY_BENEFICIAL,
            //SpellCategory.VISION,
            SpellCategory.WISDOM_INTELLIGENCE
        };

        public static bool IsBuff(this SpellType @this)
        {
            return @this.Beneficial && BuffSpellCategories.Contains(@this.GetSpellCategory());
        }

        public static SpellCategory GetSpellCategory(this SpellType @this)
        {
            return (SpellCategory)@this.CategoryID.GetValueOrDefault(uint.MaxValue);
        }

        public static IEnumerable<ReagentInfo> GetReagents(this SpellType @this)
        {
            for (int i = 1; i <= 4; i++)
            {
                var itemID = @this.GetReagentID(i).GetValueOrDefault(0u);

                if (itemID > 0u && itemID < uint.MaxValue)
                {
                    var count = @this.GetReagentCount(i).GetValueOrDefault(0u);

                    yield return new ReagentInfo(itemID, count);
                }
                else
                {
                    itemID = @this.GetNoExpendReagentID(i).GetValueOrDefault(0u);

                    if (itemID > 0u && itemID < uint.MaxValue)
                    {
                        var count = @this.GetReagentCount(i).GetValueOrDefault(0u);

                        yield return new ReagentInfo(itemID, count);
                    }
                }
            }
        }

        public static bool HasCastTime(this SpellType @this)
        {
            return @this.CastTime.HasValue && @this.CastTime.Value > TimeSpan.Zero;
        }
    }
}
