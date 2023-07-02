﻿using System;
using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// A timestamp represented in milliseconds.
    /// This type is also a mess and uses different VarPtr members.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-timestamp/
    /// </summary>
    [PublicAPI]
    [MQ2Type("timestamp")]
    public class TimeStampType : MQ2DataType
    {
        internal TimeStampType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }
        
        ///// <summary>
        ///// The hours component of "hh:mm:ss"
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public long? Hours => GetMember<Int64Type>("Hours");
        //
        ///// <summary>
        ///// The minutes component of "hh:mm:ss"
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public long? Minutes => GetMember<Int64Type>("Minutes");
        //
        ///// <summary>
        ///// The seconds component of "hh:mm:ss"
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public long? Seconds => GetMember<Int64Type>("Seconds");
        //
        ///// <summary>
        ///// The total time in "hh:mm:ss"
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public string TimeHMS => GetMember<StringType>("TimeHMS");
        //
        ///// <summary>
        ///// The total time in "mm:ss"
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public string Time => GetMember<StringType>("Time");
        //
        ///// <summary>
        ///// The total number of minutes
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public long? TotalMinutes => GetMember<Int64Type>("TotalMinutes");
        //
        ///// <summary>
        ///// The total number of seconds
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public long? TotalSeconds => GetMember<Int64Type>("TotalSeconds");
        //
        ///// <summary>
        ///// Number of milliseconds
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public long? Raw => GetMember<Int64Type>("Raw");
        //
        ///// <summary>
        ///// Number of seconds
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public float? Float => GetMember<FloatType>("Float");
        //
        ///// <summary>
        ///// Equivalent number of ticks
        ///// </summary>
        //[Obsolete("Use conversion to TimeSpan")]
        //public long? Ticks => GetMember<Int64Type>("Ticks");
        
        /// <summary>
        /// Implicit conversion to TimeSpan
        /// </summary>
        /// <param name="timestampType"></param>
        /// <returns></returns>
        public static implicit operator TimeSpan? (TimeStampType timestampType)
        {
            if (timestampType == null)
            {
                return null;
            }

            TimeSpan? timespan = null;

            // First try UInt64
            // MQBuffType::Duration
            // MQ2ItemType::CastTime
            // CharacterType::GemTimer
            // CharacterType::LastZoned
            // CharacterType::AbilityTimer
            // CharacterType::CastTimeLeft
            // DZTimerType::Timer
            // ItemType::CastTime
            // TaskType::Timer
            // SpellType::CastTime
            // SpellType::RecoveryTime
            // SpellType::FizzleTime
            // SpellType::RecastTime
            // SpellType::MyCastTime
            if (timestampType.VarPtr.UInt64 > 0)
            {
                timespan = TimeSpan.FromMilliseconds(timestampType.VarPtr.UInt64);
            }
            // then UInt32
            // this was the previous implementation
            // SpawnType::TimeBeenDead
            else if (timestampType.VarPtr.Dword > 0)
            {
                timespan = TimeSpan.FromMilliseconds(timestampType.VarPtr.Dword);
            }

            return timespan;
        }
    }
}