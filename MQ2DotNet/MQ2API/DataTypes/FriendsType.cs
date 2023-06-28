﻿using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for the friends list.
    /// Last Verified: 2023-06-26
    /// </summary>
    [PublicAPI]
    [MQ2Type("friend")]
    public class FriendsType : MQ2DataType
    {
        internal FriendsType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _friend = new IndexedStringMember<int, BoolType, string>(this, "Friend");
        }

        /// <summary>
        /// Name of a friend by index (1 based) or true/false if a name is on your friend list
        /// </summary>
        private IndexedStringMember<int, BoolType, string> _friend;
    }
}