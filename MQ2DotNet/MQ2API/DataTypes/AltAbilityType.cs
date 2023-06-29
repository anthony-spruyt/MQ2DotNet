using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains all the data related to alternate abilities
    /// Last Verified: 2023-06-30
    /// https://docs.macroquest.org/reference/data-types/datatype-altability/
    /// </summary>
    [PublicAPI]
    [MQ2Type("altability")]
    public class AltAbilityType : MQ2DataType
    {
        internal AltAbilityType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Basic description
        /// </summary>
        public string Description => GetMember<StringType>("Description");

        /// <summary>
        /// The name of the category that this AA belongs to.
        /// TODO: create an enum for this.
        /// </summary>
        public string Category => GetMember<StringType>("Category");

        /// <summary>
        /// First line of button label (if any)
        /// </summary>
        public string ShortName => GetMember<StringType>("ShortName");

        /// <summary>
        /// Second line of button label (if any)
        /// </summary>
        public string ShortName2 => GetMember<StringType>("ShortName2");

        /// <summary>
        /// ID
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// ID of the AA group that this AA belongs to
        /// </summary>
        [Obsolete]
        public uint? GroupID => ID;

        /// <summary>
        /// Reuse time in seconds
        /// </summary>
        public TimeSpan? ReuseTime
        {
            get
            {
                var dword = (uint?)GetMember<IntType>("ReuseTime");

                if (dword.HasValue)
                {
                    return TimeSpan.FromSeconds(dword.Value);
                }

                return null;
            }
        }

        /// <summary>
        /// Reuse time (in seconds) that takes into account any hastened AA abilities
        /// </summary>
        public TimeSpan? MyReuseTime
        {
            get
            {
                var dword = (uint?)GetMember<IntType>("MyReuseTime");

                if (dword.HasValue)
                {
                    return TimeSpan.FromSeconds(dword.Value);
                }

                return null;
            }
        }

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
        /// Returns the Rank of the AA
        /// </summary>
        public uint? Rank => GetMember<IntType>("Rank");

        /// <summary>
        /// Rank required to train
        /// </summary>
        public uint? AARankRequired => Rank;

        /// <summary>
        /// Type (1-6)
        /// TODO: Document properly and create enum
        /// </summary>
        public uint? Type => GetMember<IntType>("Type");

        /// <summary>
        /// Flags value (Currently unknown?).
        /// </summary>
        public uint? Flags => GetMember<IntType>("Flags");

        /// <summary>
        /// Expansion level for the ability.
        /// </summary>
        public uint? Expansion => GetMember<IntType>("Expansion");

        /// <summary>
        /// Returns true/false on if the Alternative Ability is passive
        /// </summary>
        public bool Passive => GetMember<BoolType>("Passive");

        /// <summary>
        /// Returns the amount of points spent on an AA
        /// </summary>
        public int? PointsSpent => GetMember<IntType>("PointsSpent");

        /// <summary>
        /// Returns the index number of the Alternative Ability
        /// </summary>
        public uint? Index => GetMember<IntType>("Index");

        /// <summary>
        /// Returns true/false on if the Alternative Ability can be trained
        /// </summary>
        public bool CanTrain => GetMember<BoolType>("CanTrain");

        /// <summary>
        /// Returns the next index number of the Alternative Ability
        /// </summary>
        public uint? NextIndex => GetMember<IntType>("NextIndex");

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