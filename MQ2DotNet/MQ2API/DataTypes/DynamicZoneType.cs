using JetBrains.Annotations;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a dynamic zone.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("dynamiczone")]
    public class DynamicZoneType : MQ2DataType
    {
        internal DynamicZoneType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Timer = new IndexedMember<DZTimerType, int, DZTimerType, string>(this, "Timer");
            Member = new IndexedMember<DZMemberType, int, DZMemberType, string>(this, "Member");
        }

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool LeaderFlagged => GetMember<BoolType>("LeaderFlagged");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? MaxTimers => GetMember<IntType>("MaxTimers");

        /// <summary>
        /// TODO: new member
        /// </summary>
        [JsonIgnore]
        public IndexedMember<DZTimerType, int, DZTimerType, string> Timer { get; }

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
        /// TODO: new member
        /// </summary>
        public uint? MinMembers => GetMember<IntType>("MinMembers");

        /// <summary>
        /// Member of the dynamic zone by name or number
        /// </summary>
        [JsonIgnore]
        public IndexedMember<DZMemberType, int, DZMemberType, string> Member { get; }

        /// <summary>
        /// The leader of the dynamic zone
        /// </summary>
        public DZMemberType Leader => GetMember<DZMemberType>("Leader");
        
        /// <summary>
        /// TODO: Document DynamicZoneType.InRaid
        /// </summary>
        public bool InRaid => GetMember<BoolType>("InRaid");
    }
}