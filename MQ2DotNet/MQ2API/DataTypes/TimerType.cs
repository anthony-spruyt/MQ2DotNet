using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a timer.
    /// Last Verified: 2023-06-28
    /// </summary>
    [PublicAPI]
    [MQ2Type("timer")]
    public class TimerType : MQ2DataType
    {
        internal TimerType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// TODO: new method
        /// </summary>
        public void Expire() => GetMember<MQ2DataType>("Expire");

        /// <summary>
        /// TODO: new method
        /// </summary>
        public void Reset() => GetMember<MQ2DataType>("Reset");

        /// <summary>
        /// TODO: new method
        /// </summary>
        /// <param name="duration"></param>
        public void Set(TimeSpan duration) => GetMember<MQ2DataType>("Set", $"{duration.TotalSeconds}s");

        /// <summary>
        /// Current value of the timer in 100ms intervals
        /// </summary>
        public uint? Value => GetMember<IntType>("Value");
        
        /// <summary>
        /// Original value of the timer in 100ms, from when the variable was first created
        /// </summary>
        public uint? OriginalValue => GetMember<IntType>("OriginalValue");
    }
}