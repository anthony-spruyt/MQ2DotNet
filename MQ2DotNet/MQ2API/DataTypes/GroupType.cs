﻿using JetBrains.Annotations;
using System;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for the character's group.
    /// Last Verified: 2023-06-26
    /// </summary>
    [PublicAPI]
    [MQ2Type("group")]
    public class GroupType : MQ2DataType
    {
        internal GroupType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Member = new IndexedMember<GroupMemberType, string, GroupMemberType, int>(this, "Member");
            Injured = new IndexedMember<IntType, int>(this, "Injured");
            LowMana = new IndexedMember<IntType, int>(this, "LowMana");
        }

        /// <summary>
        /// A group member by name or number (0 - 5)
        /// </summary>
        [JsonIgnore]
        public IndexedMember<GroupMemberType, string, GroupMemberType, int> Member { get; }

        /// <summary>
        /// Total number of group members, excluding yourself
        /// </summary>
        public uint? Members => GetMember<IntType>("Members");

        /// <summary>
        /// Data on the leader of the group
        /// </summary>
        public GroupMemberType Leader => GetMember<GroupMemberType>("Leader");

        /// <summary>
        /// Number of members in your group, including yourself
        /// </summary>
        public uint? GroupSize => GetMember<IntType>("GroupSize");

        /// <summary>
        /// Data on the main tank of the group
        /// </summary>
        public GroupMemberType MainTank => GetMember<GroupMemberType>("MainTank");

        /// <summary>
        /// Data on the main assist of the group
        /// </summary>
        public GroupMemberType MainAssist => GetMember<GroupMemberType>("MainAssist");

        /// <summary>
        /// Data on the puller of the group
        /// </summary>
        public GroupMemberType Puller => GetMember<GroupMemberType>("Puller");

        /// <summary>
        /// Data on the mark NPCs role of the group
        /// </summary>
        public GroupMemberType MarkNpc => GetMember<GroupMemberType>("MarkNpc");

        /// <summary>
        /// Data on the master looter of the group
        /// </summary>
        public GroupMemberType MasterLooter => GetMember<GroupMemberType>("MasterLooter");

        /// <summary>
        /// TRUE if someone is missing in group, offline, in other zone or simply just dead
        /// </summary>
        public bool AnyoneMissing => GetMember<BoolType>("AnyoneMissing");

        /// <summary>
        /// Number of group members (excluding yourself) that are in zone and alive
        /// </summary>
        public uint? Present => GetMember<IntType>("Present");

        /// <summary>
        /// Count of how many mercenaries are in your group
        /// </summary>
        public uint? MercenaryCount => GetMember<IntType>("MercenaryCount");

        /// <summary>
        /// Count of how many Tank mercenaries are in your group
        /// </summary>
        public uint? TankMercCount => GetMember<IntType>("TankMercCount");

        /// <summary>
        /// Count of how many Healer mercenaries are in your group
        /// </summary>
        public uint? HealerMercCount => GetMember<IntType>("HealerMercCount");

        /// <summary>
        /// Count of how many Melee DPS mercenaries are in your group
        /// </summary>
        public uint? MeleeMercCount => GetMember<IntType>("MeleeMercCount");

        /// <summary>
        /// Count of how many Caster DPS mercenaries are in your group
        /// </summary>
        public uint? CasterMercCount => GetMember<IntType>("CasterMercCount");
        
        /// <summary>
        /// Average HP percentage of group members, including yourself
        /// </summary>
        public long? AvgHPs => GetMember<IntType>("AvgHPs");

        /// <summary>
        /// Will return the numbers of people in the group that has less than a certain percentage HP.
        /// Resulting value is stored in <see cref="MQ2VarPtr.Dword"/> so cast to <see cref="uint"/>.
        /// </summary>
        [JsonIgnore]
        public IndexedMember<IntType, int> Injured { get; }

        /// <summary>
        /// Will return the numbers of people in the group that has less than a certain percentage mana.
        /// Resulting value is stored in <see cref="MQ2VarPtr.Dword"/> so cast to <see cref="uint"/>.
        /// </summary>
        [JsonIgnore]
        public IndexedMember<IntType, int> LowMana { get; }

        /// <summary>
        /// The first non-mercenary cleric in the group
        /// </summary>
        public SpawnType Cleric => GetMember<SpawnType>("Cleric");

        /// <summary>
        /// Group member the mouse is currently over
        /// </summary>
        public GroupMemberType MouseOver => GetMember<GroupMemberType>("MouseOver");
    }
}