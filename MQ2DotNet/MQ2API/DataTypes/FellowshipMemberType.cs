﻿// ReSharper disable UnusedMember.Global

namespace MQ2DotNet.MQ2API.DataTypes
{
    // TODO: Check and handle indexed members for FellowshipMemberType
    public class FellowshipMemberType : MQ2DataType
    {
        internal FellowshipMemberType(MQ2TypeVar typeVar) : base(typeVar)
        {
        }

        /// <summary>
        /// Zone the member is currently in
        /// </summary>
        public ZoneType Zone => GetMember<ZoneType>("Zone");

        /// <summary>
        /// Member's level
        /// </summary>
        public int Level => GetMember<IntType>("Level");

        /// <summary>
        /// Member's class
        /// </summary>
        public ClassType Class => GetMember<ClassType>("Class");

        /// <summary>
        /// How long since the member was last online
        /// </summary>
        public TicksType LastOn => GetMember<TicksType>("LastOn");

        /// <summary>
        /// Member's name
        /// </summary>
        public string Name => GetMember<StringType>("Name");
    }
}