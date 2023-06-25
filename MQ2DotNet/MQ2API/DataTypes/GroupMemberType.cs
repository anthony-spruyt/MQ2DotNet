﻿using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a member of a group.
    /// Last Verified: 2023-06-26
    /// </summary>
    [PublicAPI]
    [MQ2Type("groupmember")]
    public class GroupMemberType : MQ2DataType
    {
        internal GroupMemberType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The name of the group member. This works even if they are not in the same zone as you
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// TRUE if the member is the group's leader, FALSE otherwise
        /// Note in MQ client source -> TODO: GroupMembers: use IsGroupLeader
        /// </summary>
        public bool Leader => GetMember<BoolType>("Leader");

        /// <summary>
        /// Accesses the group member's spawn. Not all members will have a spawn (if they are out of the zone).
        /// </summary>
        public SpawnType Spawn => GetMember<SpawnType>("Spawn");

        /// <summary>
        /// The member's level
        /// </summary>
        public uint? Level => GetMember<IntType>("Level");

        /// <summary>
        /// TRUE if the member is designated as the group's Main Tank, FALSE otherwise
        /// </summary>
        public bool MainTank => GetMember<BoolType>("MainTank");

        /// <summary>
        /// TRUE if the member is designated as the group's Main Assist, FALSE otherwise
        /// </summary>
        public bool MainAssist => GetMember<BoolType>("MainAssist");

        /// <summary>
        /// TRUE if the member is designated as the Mark NPC role
        /// </summary>
        public bool MarkNpc => GetMember<BoolType>("MarkNpc");

        /// <summary>
        /// TRUE if the member is designated as the Master Looter role
        /// </summary>
        public bool MasterLooter => GetMember<BoolType>("MasterLooter");

        /// <summary>
        /// TRUE if the member is designated as the Puller role
        /// </summary>
        public bool Puller => GetMember<BoolType>("Puller");

        /// <summary>
        /// TRUE if the member is a mercenary, FALSE otherwise
        /// </summary>
        public bool Mercenary => GetMember<BoolType>("Mercenary");

        /// <summary>
        /// Member's aggro percentage as shown in the group window
        /// </summary>
        public uint? PctAggro => GetMember<IntType>("PctAggro");

        /// <summary>
        /// Index (0 based) of the member in the group
        /// </summary>
        public uint? Index => GetMember<IntType>("Index");

        /// <summary>
        /// TRUE if the member is offline and FALSE if online
        /// </summary>
        public bool Offline => GetMember<BoolType>("Offline");

        /// <summary>
        /// TRUE if the member is online and in same zone and FALSE if online and not in same zone as you
        /// </summary>
        public bool Present => GetMember<BoolType>("Present");

        /// <summary>
        /// TRUE if the member is online but in another zone and FALSE if online and in same zone as you
        /// </summary>
        public bool OtherZone => GetMember<BoolType>("OtherZone");
    }
}