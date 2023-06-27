using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a time.
    /// Last Verified: 2023-06-28
    /// </summary>
    [PublicAPI]
    [MQ2Type("time")]
    public class TimeType : MQ2DataType
    {
        internal TimeType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }
        
        /// <summary>
        /// Hours since midnight
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public uint? Hour => GetMember<IntType>("Hour");
        
        /// <summary>
        /// Hour in 24h time
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public string Hour12 => GetMember<StringType>("Hour12");
        
        /// <summary>
        /// Minutes after the hour
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? Minute => GetMember<IntType>("Minute");
        
        /// <summary>
        /// Seconds after the minute
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? Second => GetMember<IntType>("Second");

        /// <summary>
        /// Milliseconds after the second
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? Millisecond => GetMember<IntType>("Millisecond");

        /// <summary>
        /// Day of the week (0 = Sunday)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public uint? DayOfWeek => GetMember<IntType>("DayOfWeek");
        
        /// <summary>
        /// Day of the month
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public uint? Day => GetMember<IntType>("Day");
        
        /// <summary>
        /// Month of the year
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public uint? Month => GetMember<IntType>("Month");
        
        /// <summary>
        /// Year
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? Year => GetMember<IntType>("Year");
        
        /// <summary>
        /// Time in "HH:mm:ss" format (24h time)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public string Time12 => GetMember<StringType>("Time12");
        
        /// <summary>
        /// Time in "hh:mm:ss" format (12h time)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public string Time24 => GetMember<StringType>("Time24");
        
        /// <summary>
        /// Date in "dd/MM/yyyy" format
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public string Date => GetMember<StringType>("Date");
        
        /// <summary>
        /// Before 7AM or after 9PM ?!
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public bool Night => GetMember<BoolType>("Night");
        
        /// <summary>
        /// Number of seconds since midnight
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? SecondsSinceMidnight => GetMember<IntType>("SecondsSinceMidnight");

        /// <summary>
        /// Number of milliseconds since midnight
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? MillisecondsSinceMidnight => GetMember<IntType>("MillisecondsSinceMidnight");

        /// <summary>
        /// Number of milliseconds since Epoch
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public long? MillisecondsSinceEpoch => GetMember<Int64Type>("MillisecondsSinceEpoch");

        /// <summary>
        /// Implicit conversion to nullable DateTimeOffset
        /// </summary>
        /// <param name="timeType"></param>
        public static implicit operator DateTimeOffset?(TimeType timeType)
        {
            return timeType.MillisecondsSinceEpoch == null ?
                (DateTimeOffset?)null :
                DateTimeOffset.FromUnixTimeMilliseconds(timeType.MillisecondsSinceEpoch.Value);
        }

        /// <summary>
        /// Implicit conversion to nullable DateTime
        /// </summary>
        /// <param name="timeType"></param>
        public static implicit operator DateTime?(TimeType timeType)
        {
            return timeType.MillisecondsSinceEpoch == null ?
                (DateTime?)null :
                DateTimeOffset.FromUnixTimeMilliseconds(timeType.MillisecondsSinceEpoch.Value).DateTime;
        }
    }
}