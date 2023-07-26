using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data for the current dynamic zone instance.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-dynamiczone/
    /// </summary>
    [MQ2Type("dynamiczone")]
    public class DynamicZoneType : MQ2DataType
    {
        internal DynamicZoneType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _timer = new IndexedMember<DZTimerType, int, DZTimerType, string>(this, "Timer");
            _member = new IndexedMember<DZMemberType, int, DZMemberType, string>(this, "Member");
        }

        /// <summary>
        /// Returns true if the dzleader can successfully enter the dz (this also means the dz is actually Loaded.)
        /// </summary>
        public bool LeaderFlagged => GetMember<BoolType>("LeaderFlagged");

        /// <summary>
        /// The number of timers present in Timers
        /// </summary>
        public uint? MaxTimers => GetMember<IntType>("MaxTimers");

        /// <summary>
        /// Access the list of current lockout timers. This is either an index from 1 to MaxTimers, or a "Expedition|Event" combination.
        /// Event is optional, but if multiple Expeditions match, the timer with the earliest lockout expiration will be returned.
        /// Timer[ # | name ]
        /// </summary>
        private readonly IndexedMember<DZTimerType, int, DZTimerType, string> _timer;

        /// <summary>
        /// Access the list of current lockout timers. This is an index from 1 to MaxTimers
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public DZTimerType GetLockoutTimer(int index) => _timer[index];

        /// <summary>
        /// Access the list of current lockout timers. This is a "Expedition|Event" combination.
        /// Event is optional, but if multiple Expeditions match, the timer with the earliest lockout expiration will be returned.
        /// </summary>
        /// <param name="query">The "Expedition|Event" combination</param>
        /// <returns></returns>
        public DZTimerType GetLockoutTimer(string query) => _timer[query];

        public IEnumerable<DZTimerType> LockoutTimers
        {
            get
            {
                var count = (int)MaxTimers.GetValueOrDefault(0u);
                List<DZTimerType> items = new List<DZTimerType>(count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetLockoutTimer(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// The full name of the dynamic zone
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Current number of characters in the dynamic zone
        /// </summary>
        public uint? Members => GetMember<IntType>("Members");

        /// <summary>
        /// Maximum number of characters that can enter this dynamic zone
        /// </summary>
        public uint? MaxMembers => GetMember<IntType>("MaxMembers");

        /// <summary>
        /// Minimum number of members required.
        /// </summary>
        public uint? MinMembers => GetMember<IntType>("MinMembers");

        /// <summary>
        /// The dynamic zone member # or name
        /// Member[ # | name ]
        /// </summary>
        private readonly IndexedMember<DZMemberType, int, DZMemberType, string> _member;

        /// <summary>
        /// The dynamic zone member by #
        /// Member[ # ]
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public DZMemberType GetDZMember(int index) => _member[index];

        /// <summary>
        /// The dynamic zone member by name
        /// Member[ name ]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DZMemberType GetDZMember(string name) => _member[name];

        public IEnumerable<DZMemberType> DZMembers
        {
            get
            {
                var count = (int)Members.GetValueOrDefault(0u);
                List<DZMemberType> items = new List<DZMemberType>(count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetDZMember(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// The leader of the dynamic zone
        /// </summary>
        public DZMemberType Leader => GetMember<DZMemberType>("Leader");

        /// <summary>
        /// TODO: What is this? (online doco has no info on this)
        /// </summary>
        public bool InRaid => GetMember<BoolType>("InRaid");

        /// <summary>
        /// Same as <see cref="Name"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}