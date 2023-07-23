using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Information about cached buffs on a player. Data must be populated on a player by first targeting them.
    /// 
    /// "Cached Buffs" used to be a new way to access buff information on players without needing to re-target them.
    /// Now, this functionality is fully integrated into MacroQuest and available through the buff datatype.
    /// As a result, using cached buffs is now discouraged.
    /// You should only used this type if you need access to its "unique" style of buff lookup.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-cachedbuff/
    /// </summary>
    [MQ2Type("cachedbuff")]
    public class CachedBuffType : SpellType
    {
        internal CachedBuffType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Returns the name of the caster who applied the buff, same as <see cref="Caster"/>
        /// </summary>
        [Obsolete]
        public string CasterName => Caster;

        /// <summary>
        /// Returns the name of the caster who applied the buff
        /// </summary>
        public string Caster => GetMember<StringType>("Caster");

        /// <summary>
        /// Returns the amount of buffs catched, or -1 it none
        /// </summary>
        public int? Count => GetMember<IntType>("Count");

        /// <summary>
        /// Returns the buff slot the target had the buff in.
        /// </summary>
        public int? Slot => GetMember<IntType>("Slot");

        /// <summary>
        /// Access the spell.
        /// </summary>
        public SpellType Spell => GetMember<SpellType>("Spell");

        /// <summary>
        /// Returns the buff's spell ID.
        /// </summary>
        public int? SpellID => GetMember<IntType>("SpellID");

        /// <summary>
        /// Original duration of the buff.
        /// </summary>
        public TimeSpan? OriginalDuration => GetMember<TimeStampType>("OriginalDuration");

        /// <summary>
        /// Returns the duration of the buff.
        /// </summary>
        public new TimeSpan? Duration => GetMember<TimeStampType>("Duration");

        /// <summary>
        /// How long it has been since this information was refreshed.
        /// </summary>
        public TimeSpan? Staleness => GetMember<TimeStampType>("Staleness");
    }
}