using JetBrains.Annotations;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains details about your group.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-group/
    /// </summary>
    [PublicAPI]
    [MQ2Type("group")]
    public class GroupType : MQ2DataType
    {
        internal GroupType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _member = new IndexedMember<GroupMemberType, string, GroupMemberType, int>(this, "Member");
            _injured = new IndexedMember<IntType, int>(this, "Injured");
            _lowMana = new IndexedMember<IntType, int>(this, "LowMana");
        }

        /// <summary>
        /// The Nth member of your group. 0 is always you. 1 is the first person in the group list, etc.
        /// Member[ N ]
        /// 
        /// The group member of your group identified by Name.
        /// Member[ Name ]
        /// </summary>
        private IndexedMember<GroupMemberType, string, GroupMemberType, int> _member;

        /// <summary>
        /// The group member of your group identified by Name.
        /// Member[ Name ]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GroupMemberType GetGroupMember(string name) => _member[name];

        /// <summary>
        /// The Nth member of your group. 0 is always you. 1 is the first person in the group list, etc.
        /// Member[ N ]
        /// </summary>
        /// <param name="index">The index [0,5]</param>
        /// <returns></returns>
        public GroupMemberType GetGroupMember(int index) => _member[index];

        public IEnumerable<GroupMemberType>  GroupMembers
        {
            get
            {
                var count = (int?)GroupSize ?? 0;

                for (var i = 0; i < count; i++)
                {
                    yield return GetGroupMember(i);
                }
            }
        }

        /// <summary>
        /// Total number of group members, excluding yourself
        /// </summary>
        public uint? Members => GetMember<IntType>("Members");

        /// <summary>
        /// The leader of the group.
        /// </summary>
        public GroupMemberType Leader => GetMember<GroupMemberType>("Leader");

        /// <summary>
        /// The number of members in your group, including yourself.
        /// </summary>
        public uint? GroupSize => GetMember<IntType>("GroupSize");

        /// <summary>
        /// The main tank of the group, if one is assigned.
        /// </summary>
        public GroupMemberType MainTank => GetMember<GroupMemberType>("MainTank");

        /// <summary>
        /// The main assist of the group, if one is assigned.
        /// </summary>
        public GroupMemberType MainAssist => GetMember<GroupMemberType>("MainAssist");

        /// <summary>
        /// The puller of the group, if one is assigned.
        /// </summary>
        public GroupMemberType Puller => GetMember<GroupMemberType>("Puller");

        /// <summary>
        /// The group member who can mark NPCs, if one is assigned.MasterLooter
        /// </summary>
        public GroupMemberType MarkNpc => GetMember<GroupMemberType>("MarkNpc");

        /// <summary>
        /// The master looter of the group, if one is assigned.
        /// </summary>
        public GroupMemberType MasterLooter => GetMember<GroupMemberType>("MasterLooter");

        /// <summary>
        /// True if somebody in the group is offline, in some other zone, or just simply dead.
        /// </summary>
        public bool AnyoneMissing => GetMember<BoolType>("AnyoneMissing");

        /// <summary>
        /// Number of group members (excluding yourself) that are in zone and alive
        /// </summary>
        public uint? Present => GetMember<IntType>("Present");

        /// <summary>
        /// The total number of mercenaries that are in the group.
        /// </summary>
        public uint? MercenaryCount => GetMember<IntType>("MercenaryCount");

        /// <summary>
        /// The number of tank mercenaries in your group.
        /// </summary>
        public uint? TankMercCount => GetMember<IntType>("TankMercCount");

        /// <summary>
        /// The number of healer mercenaries in your group.
        /// </summary>
        public uint? HealerMercCount => GetMember<IntType>("HealerMercCount");

        /// <summary>
        /// The number of melee mercenaries in your group.
        /// </summary>
        public uint? MeleeMercCount => GetMember<IntType>("MeleeMercCount");

        /// <summary>
        /// The number of caster dps mercenaries in your group.
        /// </summary>
        public uint? CasterMercCount => GetMember<IntType>("CasterMercCount");
        
        /// <summary>
        /// Average HP percentage of group members, including yourself
        /// </summary>
        public long? AvgHPs => GetMember<IntType>("AvgHPs");

        /// <summary>
        /// The numbers of people in the group that has an hp percent lower than #.
        /// Injured[ # ]
        /// 
        /// Resulting value is stored in <see cref="MQ2VarPtr.Dword"/> so cast to <see cref="uint"/>.
        /// </summary>
        private IndexedMember<IntType, int> _injured;

        /// <summary>
        /// The numbers of people in the group that has an hp percent lower than #.
        /// Injured[ # ]
        /// </summary>
        /// <param name="hpPercent"></param>
        /// <returns></returns>
        public uint? GetInjuredCount(int hpPercent) => _injured[hpPercent];

        /// <summary>
        /// The number of people in the group that have a mana percent lower than #.
        /// LowMana[ # ]
        /// 
        /// Resulting value is stored in <see cref="MQ2VarPtr.Dword"/> so cast to <see cref="uint"/>.
        /// </summary>
        private IndexedMember<IntType, int> _lowMana;

        /// <summary>
        /// The number of people in the group that have a mana percent lower than #.
        /// LowMana[ # ]
        /// </summary>
        /// <param name="manaPercent"></param>
        /// <returns></returns>
        public uint? GetLowManaCount(int manaPercent) => _lowMana[manaPercent];

        /// <summary>
        /// The first member of the group that is a cleric.
        /// </summary>
        public SpawnType Cleric => GetMember<SpawnType>("Cleric");

        /// <summary>
        /// The group member that the mouse is currently hovering over in the group window, if any.
        /// NOTE: You can hover over your own name in the player window where you see your hp and it will return you.
        /// </summary>
        public GroupMemberType MouseOver => GetMember<GroupMemberType>("MouseOver");

        /// <summary>
        /// The number of members in the group, as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}