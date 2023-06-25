using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// TODO: new data type.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("dztimer")]
    public class DZTimerType : MQ2DataType
    {
        internal DZTimerType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string ExpeditionName => GetMember<StringType>("ExpeditionName");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string EventName => GetMember<StringType>("EventName");

        /// <summary>
        /// TODO: new member
        /// Stores the value in <see cref="MQ2VarPtr.Int64"/> as ms.
        /// </summary>
        public TimeSpan? Timer => GetMember<TimeStampType>("Timer");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? EventID => GetMember<IntType>("EventID");
    }
}