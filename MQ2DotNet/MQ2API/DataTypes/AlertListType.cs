namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Provides access to the properties of a spawn search associated with an alert. For a spawn to be entered into an alert it must match all the criteria specified by the alert list.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/top-level-objects/tlo-alert/#alertlist-type
    /// </summary>
    /// <remarks>VarPtr identifies a SPAWNSEARCH struct on an alert list</remarks>
    [MQ2Type("alertlist")]
    public class AlertListType : MQ2DataType
    {
        internal AlertListType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Any spawn that is at this level or greater
        /// </summary>
        public uint? MinLevel => GetMember<IntType>("MinLevel");

        /// <summary>
        /// Any spawn that is at this level or lower
        /// </summary>
        public uint? MaxLevel => GetMember<IntType>("MaxLevel");

        /// <summary>
        /// Any spawn with the given type
        /// </summary>
        public EQ.SpawnType? SpawnType => GetMember<IntType>("SpawnType");

        /// <summary>
        /// Any spawn with the given Spawn ID
        /// </summary>
        public uint? SpawnID => GetMember<IntType>("SpawnID");

        /// <summary>
        /// Search starts at given spawn id
        /// </summary>
        public uint? FromSpawnID => GetMember<IntType>("FromSpawnID");

        /// <summary>
        /// Excludes the spawn if any player is within this distance (nopcnear filter)
        /// </summary>
        public float? Radius => GetMember<FloatType>("Radius");

        /// <summary>
        /// Any spawn with the given name
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Any spawn with given body type
        /// TODO: can we maybe make an enum?
        /// </summary>
        public string BodyType => GetMember<StringType>("BodyType");

        /// <summary>
        /// Any spawn with the given race
        /// TODO: can we maybe make an enum? <see cref="EQ.Race"/>
        /// </summary>
        public string Race => GetMember<StringType>("Race");

        /// <summary>
        /// Any spawn that is the given class
        /// TODO: can we maybe make an enum? <see cref="EQ.Class"/>
        /// </summary>
        public string Class => GetMember<StringType>("Class");

        /// <summary>
        /// Any spawn that is equipped with the given light source
        /// </summary>
        public string Light => GetMember<StringType>("Light");

        /// <summary>
        /// Any member of the guild with the given id
        /// </summary>
        public long? GuildID => GetMember<Int64Type>("GuildID");

        /// <summary>
        /// Indicates usage of the id filter
        /// </summary>
        public bool bSpawnID => GetMember<BoolType>("bSpawnID");

        /// <summary>
        /// Indicates usage of notnearalert filter
        /// </summary>
        public bool bNotNearAlert => GetMember<BoolType>("bNotNearAlert");

        /// <summary>
        /// Indicates usage of nearalert filter
        /// </summary>
        public bool bNearAlert => GetMember<BoolType>("bNearAlert");

        /// <summary>
        /// Indicates usage of noalert filter
        /// </summary>
        public bool bNoAlert => GetMember<BoolType>("bNoAlert");

        /// <summary>
        /// Indicates usage of alert filter
        /// </summary>
        public bool bAlert => GetMember<BoolType>("bAlert");

        /// <summary>
        /// Any player that is flagged as LFG
        /// </summary>
        public bool bLFG => GetMember<BoolType>("bLFG");

        /// <summary>
        /// Any player that is a trader
        /// </summary>
        public bool bTrader => GetMember<BoolType>("bTrader");

        /// <summary>
        /// Indicates usage of a light filter
        /// </summary>
        public bool bLight => GetMember<BoolType>("bLight");

        /// <summary>
        /// Indicates usage of the next filter
        /// </summary>
        public bool bTargNext => GetMember<BoolType>("bTargNext");

        /// <summary>
        /// Indicates usage of the prev filter
        /// </summary>
        public bool bTargPrev => GetMember<BoolType>("bTargPrev");

        /// <summary>
        /// Any member of the group
        /// </summary>
        public bool bGroup => GetMember<BoolType>("bGroup");

        /// <summary>
        /// Any member of the fellowship
        /// </summary>
        public bool bFellowship => GetMember<BoolType>("bFellowship");

        /// <summary>
        /// Exclude any player that is in the group
        /// </summary>
        public bool bNoGroup => GetMember<BoolType>("bNoGroup");

        /// <summary>
        /// Any member of the raid
        /// </summary>
        public bool bRaid => GetMember<BoolType>("bRaid");

        /// <summary>
        /// Any player flagged as a GM
        /// </summary>
        public bool bGM => GetMember<BoolType>("bGM");

        /// <summary>
        /// Any "named" NPC
        /// </summary>
        public bool bNamed => GetMember<BoolType>("bNamed");

        /// <summary>
        /// Any merchant
        /// </summary>
        public bool bMerchant => GetMember<BoolType>("bMerchant");

        /// <summary>
        /// Any banker
        /// </summary>
        public bool bBanker => GetMember<BoolType>("bBanker");

        /// <summary>
        /// Any NPC that is a tribute master
        /// </summary>
        public bool bTributeMaster => GetMember<BoolType>("bTributeMaster");

        /// <summary>
        /// Any player that is a knight
        /// </summary>
        public bool bKnight => GetMember<BoolType>("bKnight");

        /// <summary>
        /// Any player that is a tank class
        /// </summary>
        public bool bTank => GetMember<BoolType>("bTank");

        /// <summary>
        /// Any player that is a healer class
        /// </summary>
        public bool bHealer => GetMember<BoolType>("bHealer");

        /// <summary>
        /// Any player that is a DPS class
        /// </summary>
        public bool bDps => GetMember<BoolType>("bDps");

        /// <summary>
        /// Any player that is a slower
        /// </summary>
        public bool bSlower => GetMember<BoolType>("bSlower");

        /// <summary>
        /// Any aur.
        /// </summary>
        public bool bAura => GetMember<BoolType>("bAura");

        /// <summary>
        /// Any banner
        /// </summary>
        public bool bBanner => GetMember<BoolType>("bBanner");

        /// <summary>
        ///	Any campfire
        /// </summary>
        public bool bCampfire => GetMember<BoolType>("bCampfire");

        /// <summary>
        /// Excludes any spawn with the given id
        /// </summary>
        public uint? NotID => GetMember<IntType>("NotID");

        /// <summary>
        /// Excludes any spawn near the given alert list
        /// </summary>
        public uint? NotNearAlertList => GetMember<IntType>("NotNearAlertList");

        /// <summary>
        /// Any spawn near the given alert list
        /// </summary>
        public uint? NearAlertList => GetMember<IntType>("NearAlertList");

        /// <summary>
        /// Excludes any spawn in the given alert list
        /// </summary>
        public uint? NoAlertList => GetMember<IntType>("NoAlertList");

        /// <summary>
        /// Any spawn on the associated alert list
        /// </summary>
        public uint? AlertList => GetMember<IntType>("AlertList");

        /// <summary>
        /// z distance component of the loc filter
        /// </summary>
        public double? ZRadius => GetMember<DoubleType>("ZRadius");

        /// <summary>
        /// Any spawn that is given distance from the given loc filter
        /// </summary>
        public double? FRadius => GetMember<DoubleType>("FRadius");

        /// <summary>
        /// x component of the loc filter
        /// </summary>
        public float? xLoc => GetMember<FloatType>("xLoc");

        /// <summary>
        /// y component of the loc filter
        /// </summary>
        public float? yLoc => GetMember<FloatType>("yLoc");

        /// <summary>
        /// Indicates usage of a loc filter
        /// </summary>
        public bool bKnownLocation => GetMember<BoolType>("bKnownLocation");

        /// <summary>
        /// Exclude any spawn that is a pet
        /// </summary>
        public bool bNoPet => GetMember<BoolType>("bNoPet");

        /// <summary>
        /// Indicates the sort order of the filter
        /// 0 = level, 1 = display name (default), 2 = race, 3 = class, 4 = distance (2D, XY), 5 = guild, 6 = id
        /// </summary>
        public uint? SortBy => GetMember<IntType>("SortBy");

        /// <summary>
        /// Exclude any player that is in the guild
        /// </summary>
        public bool bNoGuild => GetMember<BoolType>("bNoGuild");

        /// <summary>
        /// Any spawn in line of sight
        /// </summary>
        public bool bLoS => GetMember<BoolType>("bLoS");

        /// <summary>
        /// Name match requiries an exact match
        /// </summary>
        public bool bExactName => GetMember<BoolType>("bExactName");

        /// <summary>
        /// Any spawn that is targetable
        /// </summary>
        public bool bTargetable => GetMember<BoolType>("bTargetable");

        /// <summary>
        /// Any spawn with the given state (bitmask?)
        /// </summary>
        public uint? PlayerState => GetMember<IntType>("PlayerState");

        /// <summary>
        /// If an ID or Name is part of the filter, attempts to return a spawn with the matching ID or Name
        /// </summary>
        public SpawnType Spawn => GetMember<SpawnType>("Spawn");

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}