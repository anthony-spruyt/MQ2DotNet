using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// This is the type for any buffs currently affecting you, both long duration and short duration buffs.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-buff/
    /// </summary>
    /// <remarks>
    /// SpellType inheritance would be nice but is problematic. BuffType.VarPtr is a PSPELLBUFF, but SpellType.VarPtr requires a PSPELL
    /// MQ2 system gets around this by finding the spell before calling the base class, but we don't have that luxury here.
    /// Use .Spell instead 
    /// </remarks>
    [PublicAPI]
    [MQ2Type("buff")]
    public class BuffType : MQ2DataType
    {
        internal BuffType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The ID of the buff or shortbuff slot.
        /// </summary>
        public int? ID => GetMember<IntType>("ID");

        /// <summary>
        /// The level of the person that cast the buff on you (not the level of the spell).
        /// </summary>
        public uint? Level => GetMember<IntType>("Level");

        /// <summary>
        /// The spell.
        /// </summary>
        public SpellType Spell => GetMember<SpellType>("Spell");

        /// <summary>
        /// The modifier to a bard song.
        /// </summary>
        public float? Mod => GetMember<FloatType>("Mod");

        /// <summary>
        /// The time remaining before the buff fades (not total duration).
        /// </summary>
        public TimeSpan? Duration => GetMember<TimeStampType>("Duration");

        /// <summary>
        /// The remaining damage absorption of the buff (if any).
        /// This is not entirely accurate, it will only show you to the Dar of your spell when it was initially cast, or what it was when you last zoned (whichever is more recent).
        /// </summary>
        public long? Dar => GetMember<Int64Type>("Dar");

        /// <summary>
        /// The total number of counters on the buff (disease, poison, curse, corruption).
        /// </summary>
        public long? TotalCounters => GetMember<Int64Type>("TotalCounters");

        /// <summary>
        /// The number of poison counters.
        /// </summary>
        public long? CountersDisease => GetMember<Int64Type>("CountersDisease");

        /// <summary>
        /// The number of disease counters.
        /// </summary>
        public long? CountersPoison => GetMember<Int64Type>("CountersPoison");

        /// <summary>
        /// The number of curse counters.
        /// </summary>
        public long? CountersCurse => GetMember<Int64Type>("CountersCurse");

        /// <summary>
        /// The number of corruption counters.
        /// </summary>
        public long? CountersCorruption => GetMember<Int64Type>("CountersCorruption");

        /// <summary>
        /// Number of hit counts remaining on the buff.
        /// </summary>
        public uint? HitCount => GetMember<IntType>("HitCount");

        /// <summary>
        /// Name of the caster who cast the buff, if available.
        /// </summary>
        public string Caster => GetMember<StringType>("Caster");

        /// <summary>
        /// Removes the named/partial name buff.
        /// </summary>
        public void Remove() => GetMember<MQ2DataType>("Remove");

        /// <summary>
        /// Same as <see cref="SpellType.Name"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}