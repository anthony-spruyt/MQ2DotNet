﻿using JetBrains.Annotations;
using MQ2DotNet.EQ;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a character race
    /// </summary>
    [PublicAPI]
    public class RaceType : MQ2DataType
    {
        internal RaceType(MQ2TypeVar typeVar) : base(typeVar)
        {
        }

        /// <summary>
        /// ID of the race, this should correspond to the <see cref="Race"/> enum
        /// </summary>
        public int? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Full name of the race e.g. Froglok
        /// </summary>
        public string Name => GetMember<StringType>("Name");
    }
}