using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains all the data related to fellowship members.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-fellowshipmember/
    /// </summary>
    [PublicAPI]
    [MQ2Type("fellowshipmember")]
    public class FellowshipMemberType : MQ2DataType
    {
        internal FellowshipMemberType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Name = GetMember<StringType>("Name");
        }

        /// <summary>
        /// Zone information for the member's zone.
        /// </summary>
        public ZoneType Zone => GetMember<ZoneType>("Zone");

        /// <summary>
        /// Member's level.
        /// </summary>
        public uint? Level => GetMember<IntType>("Level");

        /// <summary>
        /// Member's class.
        /// </summary>
        public ClassType Class => GetMember<ClassType>("Class");

        /// <summary>
        /// How long since the member was last online.
        /// </summary>
        /// <remarks>Stores data in the <see cref="MQ2VarPtr.Dword"/> field.</remarks>
        public TimeSpan? LastOn => GetMember<TicksType>("LastOn");

        /// <summary>
        /// Player name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// TRUE if member has exp sharing enabled.
        /// </summary>
        public bool Sharing => GetMember<BoolType>("Sharing");
    }
}