﻿using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a zone.
    /// Last Verified: 2023-06-28
    /// </summary>
    [PublicAPI]
    [MQ2Type("zone")]
    public class ZoneType : MQ2DataType
    {
        internal ZoneType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Long name of the zone e.g. "The Plane of Knowledge"
        /// </summary>
        public string Name => GetMember<StringType>("Name");
        
        /// <summary>
        /// Short name of the zone e.g. "PoKnowledge"
        /// </summary>
        public string ShortName => GetMember<StringType>("ShortName");
        
        /// <summary>
        /// Zone ID
        /// </summary>
        public int? ID => GetMember<IntType>("ID");
        
        /// <summary>
        /// Zone flags, see ZONELIST::ZoneFlags in eqdata.h
        /// </summary>
        public long? ZoneFlags => GetMember<Int64Type>("ZoneFlags");
    }
}