using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a skill.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("skill")]
    public class SkillType : MQ2DataType
    {
        internal SkillType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Name of the skill
        /// </summary>
        public string Name => GetMember<StringType>("Name");
        
        /// <summary>
        /// Skill number
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");
        
        /// <summary>
        /// Reuse time for the ability in milliseconds (not time remaining)
        /// </summary>
        public TimeSpan? ReuseTime => GetMember<IntType>("ReuseTime");
        
        /// <summary>
        /// Minimum level for your class
        /// </summary>
        public uint? MinLevel => GetMember<IntType>("MinLevel");
        
        /// <summary>
        /// Skill cap based on your current level and class
        /// </summary>
        public uint? SkillCap => GetMember<IntType>("SkillCap");
        
        /// <summary>
        /// TODO: Is this the common timer number (shared by other skills, but not the same as AA timers)
        /// </summary>
        public uint? AltTimer => GetMember<IntType>("AltTimer");
        
        /// <summary>
        /// Returns TRUE if the skill has been activated
        /// </summary>
        public bool Activated => GetMember<BoolType>("Activated");
        
        /// <summary>
        /// Skill has /autoskill on?
        /// </summary>
        public bool Auto => GetMember<BoolType>("Auto");
    }
}