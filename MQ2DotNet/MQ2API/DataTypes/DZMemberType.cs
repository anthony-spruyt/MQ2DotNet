﻿using JetBrains.Annotations;
using MQ2DotNet.EQ;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a member of a dynamic zone.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("dzmember")]
    public class DZMemberType : MQ2DataType
    {
        internal DZMemberType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The name of the member
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Returns true if the dzmember can successfully enter the dz. where x is either index or the name.
        /// </summary>
        public bool Flagged => GetMember<BoolType>("Flagged");

        /// <summary>
        /// The status of the member - one of the following: Unknown, Online, Offline, In Dynamic Zone, Link Dead.
        /// TODO: test enum conversion.
        /// </summary>
        public DZStatus? Status => GetMember<StringType>("Status");
    }
}