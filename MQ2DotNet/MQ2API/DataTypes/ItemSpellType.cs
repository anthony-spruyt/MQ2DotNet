using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a spell effect on an item.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("itemspell")]
    public class ItemSpellType : MQ2DataType
    {
        internal ItemSpellType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? SpellID => GetMember<IntType>("SpellID");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? RequiredLevel => GetMember<IntType>("RequiredLevel");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? EffectType => GetMember<IntType>("EffectType");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? EffectiveCasterLevel => GetMember<IntType>("EffectiveCasterLevel");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? MaxCharges => GetMember<IntType>("MaxCharges");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? CastTime => GetMember<IntType>("CastTime");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? TimerID => GetMember<IntType>("TimerID");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? RecastType => GetMember<IntType>("RecastType");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? ProcRate => GetMember<IntType>("ProcRate");
        
        /// <summary>
        /// Same as <see cref="OtherName"/>
        /// </summary>
        public string OverrideName => OtherName;
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public string OtherName => GetMember<StringType>("OtherName");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? OtherID => GetMember<IntType>("OtherID");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public string OverrideDescription => GetMember<StringType>("OverrideDescription");

        /// <summary>
        /// The spell
        /// </summary>
        public SpellType Spell => GetMember<SpellType>("Spell");
    }
}