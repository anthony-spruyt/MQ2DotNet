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
