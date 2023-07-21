using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data related to a particular skill.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-skill/
    /// </summary>
    [MQ2Type("skill")]
    public class SkillType : MQ2DataType
    {
        public const int MAX_SKILLS = 300;

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
        /// Reuse timer for the ability in milliseconds (not time remaining)
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
        /// Returns TRUE if the skill uses the kick/bash/slam/backstab/frenzy timer
        /// </summary>
        public bool AltTimer => (uint?)GetMember<IntType>("AltTimer") > 0;
        
        /// <summary>
        /// Returns TRUE if the skill requires activation, IOW it is not passive.
        /// </summary>
        public bool Activated => GetMember<BoolType>("Activated");
        
        /// <summary>
        /// Skill has /autoskill on?
        /// </summary>
        public bool Auto => GetMember<BoolType>("Auto");

        /// <summary>
        /// Same as <see cref="Name"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}