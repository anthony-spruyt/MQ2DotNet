using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a unit of clock time.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-time/
    /// </summary>
    [MQ2Type("time")]
    public class TimeType : MQ2DataType
    {
        internal TimeType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Hour (0-23)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public uint? Hour => GetMember<IntType>("Hour");

        /// <summary>
        /// Hour (0-11)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public string Hour12 => GetMember<StringType>("Hour12");

        /// <summary>
        /// Minute (0-59)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? Minute => GetMember<IntType>("Minute");

        /// <summary>
        /// Second (0-59)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? Second => GetMember<IntType>("Second");

        /// <summary>
        /// Milliseconds after the second
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? Millisecond => GetMember<IntType>("Millisecond");

        /// <summary>
        /// Day of the week (1=sunday to 7=saturday)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public uint? DayOfWeek => GetMember<IntType>("DayOfWeek");

        /// <summary>
        /// Day of the month
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public uint? Day => GetMember<IntType>("Day");

        /// <summary>
        /// Month of the year (1-12)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public uint? Month => GetMember<IntType>("Month");

        /// <summary>
        /// Year
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public int? Year => GetMember<IntType>("Year");

        /// <summary>
        /// Time in 12-hour format (HH:MM:SS)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public string Time12 => GetMember<StringType>("Time12");

        /// <summary>
        /// Time in 24-hour format (HH:MM:SS)
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public string Time24 => GetMember<StringType>("Time24");

        /// <summary>
        /// Date in the format MM/DD/YYYY
        /// </summary>
        [Obsolete("Use conversion to DateTime")]
        public string Date => GetMember<StringType>("Date");

        /// <summary>
        /// Gives true if the current hour is considered "night" in EQ (7:00pm-6:59am)
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
#pragma warning disable CS0618 // Type or member is obsolete
            return timeType.MillisecondsSinceEpoch == null ?
                (DateTimeOffset?)null :
                DateTimeOffset.FromUnixTimeMilliseconds(timeType.MillisecondsSinceEpoch.Value);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Implicit conversion to nullable DateTime
        /// </summary>
        /// <param name="timeType"></param>
        public static implicit operator DateTime?(TimeType timeType)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return timeType.MillisecondsSinceEpoch == null ?
                (DateTime?)null :
                DateTimeOffset.FromUnixTimeMilliseconds(timeType.MillisecondsSinceEpoch.Value).DateTime;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Same as <see cref="Time24"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}