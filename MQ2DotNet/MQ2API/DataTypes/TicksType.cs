using System;
using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a number of in game ticks
    /// </summary>
    [PublicAPI]
    [MQ2Type("ticks")]
    public class TicksType : MQ2DataType
    {
        internal TicksType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The hours component of "hh:mm:ss"
        /// </summary>
        [Obsolete("Use conversion to TimeSpan")]
        public uint? Hours => GetMember<IntType>("Hours");

        /// <summary>
        /// The minutes component of "hh:mm:ss"
        /// </summary>
        [Obsolete("Use conversion to TimeSpan")]
        public uint? Minutes => GetMember<IntType>("Minutes");

        /// <summary>
        /// The seconds component of "hh:mm:ss"
        /// </summary>
        [Obsolete("Use conversion to TimeSpan")]
        public uint? Seconds => GetMember<IntType>("Seconds");

        /// <summary>
        /// The total time in "hh:mm:ss"
        /// </summary>
        [Obsolete("Use conversion to TimeSpan")]
        public string TimeHMS => GetMember<StringType>("TimeHMS");

        /// <summary>
        /// The total time in "mm:ss"
        /// </summary>
        [Obsolete("Use conversion to TimeSpan")]
        public string Time => GetMember<StringType>("Time");

        /// <summary>
        /// The total number of minutes
        /// </summary>
        [Obsolete("Use conversion to TimeSpan")]
        public uint? TotalMinutes => GetMember<IntType>("TotalMinutes");

        /// <summary>
        /// The total number of seconds
        /// </summary>
        [Obsolete("Use conversion to TimeSpan")]
        public uint? TotalSeconds => GetMember<IntType>("TotalSeconds");

        /// <summary>
        /// The number of ticks
        /// </summary>
        [Obsolete("Use conversion to TimeSpan")]
        public uint? Ticks => GetMember<IntType>("Ticks");

        /// <summary>
        /// Implicit conversion to TimeSpan
        /// </summary>
        /// <param name="ticksType"></param>
        /// <returns></returns>
        public static implicit operator TimeSpan?(TicksType ticksType)
        {
            if (ticksType?.VarPtr == null)
            {
                return null;
            }

            // CharacterType::AltAbilityTimer
            // TODO: confirm but it looks like it is stored as seconds - reusetimer * 1000
            if (ticksType.VarPtr.Int64 > 0)
            {
                return TimeSpan.FromSeconds(ticksType.VarPtr.Int64);
            }
            // CharacterType::CombatAbilityTimer
            if (ticksType.VarPtr.Int > 0)
            {
                // value is the number of 6 second ticks
                return TimeSpan.FromSeconds(6 * ticksType.VarPtr.Int);
            }
            // CharacterType::Downtime
            // CharacterType::TributeTimer but it stores in MS
            // FellowshipMemberType::LastOn
            // FellowshipType::CampfireDuration
            // ItemType::Timer
            if (ticksType.VarPtr.Dword > 0)
            {
                // value is the number of 6 second ticks
                return TimeSpan.FromSeconds(6 * ticksType.VarPtr.Dword);
            }

            return TimeSpan.Zero;
        }
    }
}