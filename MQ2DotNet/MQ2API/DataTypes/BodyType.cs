﻿// ReSharper disable UnusedMember.Global

namespace MQ2DotNet.MQ2API.DataTypes
{
    public class BodyType : MQ2DataType
    {
        internal BodyType(MQ2TypeVar typeVar) : base(typeVar)
        {
        }

        /// <summary>
        /// ID of the body type, internal use only?
        /// </summary>
        public IntType ID => GetMember<IntType>("ID");

        /// <summary>
        /// Description e.g. Humanoid
        /// </summary>
        public StringType Name => GetMember<StringType>("Name");
    }
}