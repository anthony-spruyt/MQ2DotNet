using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a spell effect on an item.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-itemspell/
    /// </summary>
    [PublicAPI]
    [MQ2Type("itemspell")]
    public class ItemSpellType : MQ2DataType
    {
        internal ItemSpellType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// ID of the Spell.
        /// </summary>
        public uint? SpellID => GetMember<IntType>("SpellID");

        /// <summary>
        /// Level required for the spell to be usable.
        /// </summary>
        public uint? RequiredLevel => GetMember<IntType>("RequiredLevel");

        /// <summary>
        /// The type of item spell effect.
        /// </summary>
        public uint? EffectType => GetMember<IntType>("EffectType");

        /// <summary>
        /// Effective level that is used to cast the spell.
        /// </summary>
        public uint? EffectiveCasterLevel => GetMember<IntType>("EffectiveCasterLevel");

        /// <summary>
        /// The maximum number of charges supported by this spell.
        /// </summary>
        public uint? MaxCharges => GetMember<IntType>("MaxCharges");

        /// <summary>
        /// Spell cast time.
        /// TODO: determine the unit type (milliseconds, seconds etc.)
        /// </summary>
        public uint? CastTime => GetMember<IntType>("CastTime");

        /// <summary>
        /// Timer ID of the spell.
        /// TODO: determine the unit type (milliseconds, seconds etc.)
        /// </summary>
        public uint? TimerID => GetMember<IntType>("TimerID");

        /// <summary>
        /// Recast type of the spell.
        /// </summary>
        public uint? RecastType => GetMember<IntType>("RecastType");

        /// <summary>
        /// Combat effect proc rate.
        /// </summary>
        public uint? ProcRate => GetMember<IntType>("ProcRate");

        /// <summary>
        /// Overrides the normal spell name string, if set. Same as <see cref="OtherName"/>
        /// </summary>
        public string OverrideName => OtherName;

        /// <summary>
        /// Overrides the normal spell name string, if set. Same as <see cref="OverrideName"/>
        /// </summary>
        public string OtherName => GetMember<StringType>("OtherName");

        /// <summary>
        /// TODO: What is this? Not in the online doco at https://docs.macroquest.org/reference/data-types/datatype-itemspell/#members.
        /// </summary>
        public uint? OtherID => GetMember<IntType>("OtherID");

        /// <summary>
        /// Overrides the normal spell description string, if set.
        /// </summary>
        public string OverrideDescription => GetMember<StringType>("OverrideDescription");

        /// <summary>
        /// The spell.
        /// </summary>
        public SpellType Spell => GetMember<SpellType>("Spell");
    }
}