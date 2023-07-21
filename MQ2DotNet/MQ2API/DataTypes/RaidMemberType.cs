namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data related to the specified raid member.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-raidmember/
    /// </summary>
    [MQ2Type("raidmember")]
    public class RaidMemberType : MQ2DataType
    {
        internal RaidMemberType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Raid member's name
        /// </summary>
        public string Name => GetMember<StringType>("Name");
        
        /// <summary>
        /// Current group number (or 0)
        /// </summary>
        public uint? Group => GetMember<IntType>("Group");
        
        /// <summary>
        /// Returns TRUE if the member is a group leader
        /// </summary>
        public bool GroupLeader => GetMember<BoolType>("GroupLeader");
        
        /// <summary>
        /// Returns TRUE if the member is the raid leader
        /// </summary>
        public bool RaidLeader => GetMember<BoolType>("RaidLeader");
        
        /// <summary>
        /// Allowed to loot with current loot rules and looters?
        /// </summary>
        public bool Looter => GetMember<BoolType>("Looter");
        
        /// <summary>
        /// Spawn object for this player if available (must be in zone)
        /// </summary>
        public SpawnType Spawn => GetMember<SpawnType>("Spawn");
        
        /// <summary>
        /// Raid member's level (works without being in zone)
        /// </summary>
        public uint? Level => GetMember<IntType>("Level");
        
        /// <summary>
        /// Raid member's class (works without being in zone)
        /// </summary>
        public ClassType Class => GetMember<ClassType>("Class");

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