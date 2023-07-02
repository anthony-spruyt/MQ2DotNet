using JetBrains.Annotations;
using MQ2DotNet.EQ;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains data on the current raid.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-raid/
    /// </summary>
    [PublicAPI]
    [MQ2Type("raid")]
    public class RaidType : MQ2DataType
    {
        internal RaidType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _member = new IndexedMember<RaidMemberType, string, RaidMemberType, int>(this, "Member");
            _looter = new IndexedStringMember<int>(this, "Looter");
            _markNPC = new IndexedMember<RaidMemberType, int, RaidMemberType, string>(this, "MarkNPC");
        }

        /// <summary>
        /// Returns TRUE if the raid is locked
        /// </summary>
        public bool Locked => GetMember<BoolType>("Locked");

        /// <summary>
        /// Have I been invited to the raid?
        /// </summary>
        public bool Invited => GetMember<BoolType>("Invited");

        /// <summary>
        /// Raid member by name or number (1 based)
        /// </summary>
        private readonly IndexedMember<RaidMemberType, string, RaidMemberType, int> _member;

        /// <summary>
        /// Get raid member by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RaidMemberType GetMember(string name) => _member[name];

        /// <summary>
        /// Get raid member by index.
        /// </summary>
        /// <param name="index">The 1 based index.</param>
        /// <returns></returns>
        public RaidMemberType GetMember(int index) => _member[index];

        /// <summary>
        /// All raid members.
        /// </summary>
        public IEnumerable<RaidMemberType> RaidMembers
        {
            get
            {
                var items = new List<RaidMemberType>();
                var index = 1;
                var count = Members;

                while (count.HasValue)
                {
                    var item = GetMember(index);

                    if (item != null && index <= count)
                    {
                        items.Add(item);
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// Total number of raid members
        /// </summary>
        public uint? Members => GetMember<IntType>("Members");

        /// <summary>
        /// Raid target (clicked in raid window)
        /// </summary>
        public RaidMemberType Target => GetMember<RaidMemberType>("Target");

        /// <summary>
        /// Raid leader
        /// </summary>
        public RaidMemberType Leader => GetMember<RaidMemberType>("Leader");

        /// <summary>
        /// Sum of all raid members' levels
        /// </summary>
        public uint? TotalLevels => GetMember<IntType>("TotalLevels");

        /// <summary>
        /// Average level of raid members (more accurate than in the window)
        /// </summary>
        public float? AverageLevel => GetMember<FloatType>("AverageLevel");

        /// <summary>
        /// Loot type number (1 = Leader, 2 = Leader and GroupLeader, 3 = Leader and Specified)
        /// TODO: test enum conversion.
        /// </summary>
        public RaidLootType? LootType => GetMember<IntType>("LootType");

        /// <summary>
        /// Number of specified looters
        /// </summary>
        public uint? Looters => GetMember<IntType>("Looters");

        /// <summary>
        /// Specified looter name by number (base 1) (1 - <see cref="Looters"/>).
        /// </summary>
        private readonly IndexedStringMember<int> _looter;

        /// <summary>
        /// Get a raid looter name by index.
        /// </summary>
        /// <param name="index">The 1 based index.</param>
        /// <returns></returns>
        public string GetLooter(int index) => _looter[index];

        /// <summary>
        /// All raid looters.
        /// </summary>
        public IEnumerable<string> RaidLooters
        {
            get
            {
                var items = new List<string>();
                var index = 1;
                var count = Members;

                while (count.HasValue)
                {
                    var item = GetLooter(index);

                    if (item != null && index <= count)
                    {
                        items.Add(item);
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// Raid main assist
        /// </summary>
        public RaidMemberType MainAssist => GetMember<RaidMemberType>("MainAssist");

        /// <summary>
        /// Raid master looter
        /// </summary>
        public RaidMemberType MasterLooter => GetMember<RaidMemberType>("MasterLooter");

        /// <summary>
        /// Raid mark NPC
        /// </summary>
        private readonly IndexedMember<RaidMemberType, int, RaidMemberType, string> _markNPC;

        /// <summary>
        /// Get raid mark NPC by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RaidMemberType GetMarkNPC(string name) => _markNPC[name];

        /// <summary>
        /// Get raid mark NPC by base 1 index.
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public RaidMemberType GetMarkNPC(int index) => _markNPC[index];

        /// <summary>
        /// All mark NPCs.
        /// </summary>
        public IEnumerable<RaidMemberType> RaidMarkNPCs
        {
            get
            {
                var items = new List<RaidMemberType>();
                var index = 1;
                var count = Members;

                while (count.HasValue)
                {
                    var item = GetMarkNPC(index);

                    if (item != null && index <= count)
                    {
                        items.Add(item);
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// Get the first mark NPC raid member.
        /// </summary>
        public RaidMemberType MarkNPC => GetMember<RaidMemberType>("MarkNPC");

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}