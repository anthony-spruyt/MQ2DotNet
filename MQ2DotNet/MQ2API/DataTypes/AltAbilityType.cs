using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an AA.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("altability")]
    public class AltAbilityType : MQ2DataType
    {
        internal AltAbilityType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// First line of button label (if any)
        /// </summary>
        public string Name => GetMember<StringType>("Name");
        
        /// <summary>
        /// Description as it appears in the AA window
        /// </summary>
        public string Description => GetMember<StringType>("Description");

        /// <summary>
        /// Returns name of the category that this AA belongs to.
        /// TODO: create an enum for this.
        /// </summary>
        public string Category => GetMember<StringType>("Category");

        /// <summary>
        /// Short name of the ability
        /// </summary>
        public string ShortName => GetMember<StringType>("ShortName");

        /// <summary>
        /// Second line of button label (if any)
        /// </summary>
        public string ShortName2 => GetMember<StringType>("ShortName2");

        /// <summary>
        /// ID of the ability, for use with /alt activate
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Group ID of the ability, for use with /alt activate.
        /// Instead use <see cref="ID"/>
        /// </summary>
        [Obsolete]
        public uint? GroupID => ID;

        /// <summary>
        /// Reuse time in seconds
        /// </summary>
        public uint? ReuseTime => GetMember<IntType>("ReuseTime");

        /// <summary>
        /// Reuse time in seconds after modifiers have been applied
        /// </summary>
        public uint? MyReuseTime => GetMember<IntType>("MyReuseTime");

        /// <summary>
        /// Minimum level to train
        /// </summary>
        public uint? MinLevel => GetMember<IntType>("MinLevel");

        /// <summary>
        /// Base cost to train
        /// </summary>
        public uint? Cost => GetMember<IntType>("Cost");

        /// <summary>
        /// Spell used by the ability (if any)
        /// </summary>
        public SpellType Spell => GetMember<SpellType>("Spell");

        /// <summary>
        /// Required ability (if any)
        /// </summary>
        public AltAbilityType RequiresAbility => GetMember<AltAbilityType>("RequiresAbility");

        /// <summary>
        /// Points required in <see cref="RequiresAbility"/>
        /// </summary>
        public uint? RequiresAbilityPoints => GetMember<IntType>("RequiresAbilityPoints");

        /// <summary>
        /// Max rank available in this ability
        /// </summary>
        public uint? MaxRank => GetMember<IntType>("MaxRank");

        /// <summary>
        /// Current rank in this ability
        /// </summary>
        public uint? Rank => GetMember<IntType>("Rank");

        /// <summary>
        /// Deprecated, use <see cref="Rank"/>
        /// </summary>
        [Obsolete]
        public uint? AARankRequired => Rank;

        /// <summary>
        /// Type (1-6) TODO: Document properly and create enum
        /// </summary>
        public uint? Type => GetMember<IntType>("Type");

        /// <summary>
        /// TODO: Document properly
        /// </summary>
        public uint? Flags => GetMember<IntType>("Flags");

        /// <summary>
        /// TODO: Document properly
        /// </summary>
        public uint? Expansion => GetMember<IntType>("Expansion");

        /// <summary>
        /// True if the ability does not require activation
        /// </summary>
        public bool Passive => GetMember<BoolType>("Passive");

        /// <summary>
        /// Returns the amount of points spent on an AA
        /// </summary>
        public int? PointsSpent => GetMember<IntType>("PointsSpent");
        
        /// <summary>
        /// TODO: What is AltAbilityType.Index
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public uint? Index => GetMember<IntType>("Index");

        /// <summary>
        /// Returns true/false on if the Alternative Ability can be trained
        /// </summary>
        public bool CanTrain => GetMember<BoolType>("CanTrain");

        /// <summary>
        /// Returns the next index number of the Alternative Ability
        /// </summary>
        public uint? NextIndex => GetMember<IntType>("NextIndex");
    }
}