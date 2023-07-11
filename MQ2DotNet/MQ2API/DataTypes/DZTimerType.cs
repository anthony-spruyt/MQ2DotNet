﻿using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Provides information about a dynamic zone lockout timer.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-dztimer/
    /// </summary>
    [PublicAPI]
    [MQ2Type("dztimer")]
    public class DZTimerType : MQ2DataType
    {
        internal DZTimerType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            EventName = GetMember<StringType>("EventName");
            ExpeditionName = GetMember<StringType>("ExpeditionName");
        }

        /// <summary>
        /// The name of the expedition
        /// </summary>
        public string ExpeditionName { get; }

        /// <summary>
        /// The name of the event
        /// </summary>
        public string EventName { get; }

        /// <summary>
        /// The timestamp indicating when this lockout expires.
        /// </summary>
        public TimeSpan? Timer => GetMember<TimeStampType>("Timer");

        /// <summary>
        /// ID of the event. These values are only unique per Expedition. Non-event lockouts (Replay Timer) will have a -1 event id.
        /// </summary>
        public int? EventID => GetMember<IntType>("EventID");

        /// <summary>
        /// Returns the string formatted as "ExpeditionName|EventName"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}