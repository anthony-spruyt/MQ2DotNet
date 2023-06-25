using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an alert list.
    /// Last Verified: 2023-06-25
    /// </summary>
    /// <remarks>VarPtr identifies a SPAWNSEARCH struct on an alert list</remarks>
    [PublicAPI]
    [MQ2Type("alertlist")]
    public class AlertListType : MQ2DataType
    {
        internal AlertListType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Minimum level, inclusive
        /// </summary>
        public uint? MinLevel => GetMember<IntType>("MinLevel");

        /// <summary>
        /// Maximum level, inclusive
        /// </summary>
        public uint? MaxLevel => GetMember<IntType>("MaxLevel");

        /// <summary>
        /// Type, see eSpawnType in MQ2Internal.h
        /// </summary>
        public EQ.SpawnType? SpawnType => GetMember<IntType>("SpawnType");

        /// <summary>
        /// Spawn ID to match
        /// </summary>
        public uint? SpawnID => GetMember<IntType>("SpawnID");

        /// <summary>
        /// Last spawn ID returned, used when iterating through a search spawn
        /// </summary>
        public uint? FromSpawnID => GetMember<IntType>("FromSpawnID");

        /// <summary>
        /// Radius in which to search (around xLoc/yLoc if set, otherwise around character)
        /// </summary>
        public float? Radius => GetMember<FloatType>("Radius");

        /// <summary>
        /// Include spawns matching this name
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Include spawns with this body type description e.g. Humanoid
        /// TODO: can we maybe make an enum?
        /// </summary>
        public string BodyType => GetMember<StringType>("BodyType");

        /// <summary>
        /// Include spawns with this race e.g. Vah Shir
        /// TODO: can we maybe make an enum? <see cref="EQ.Race"/>
        /// </summary>
        public string Race => GetMember<StringType>("Race");

        /// <summary>
        /// Include spawns with this class e.g. Cleric
        /// TODO: can we maybe make an enum? <see cref="EQ.Class"/>
        /// </summary>
        public string Class => GetMember<StringType>("Class");

        /// <summary>
        /// Include spawns with this light
        /// </summary>
        public string Light => GetMember<StringType>("Light");

        /// <summary>
        /// Include spawns in this guild ID
        /// </summary>
        public long? GuildID => GetMember<Int64Type>("GuildID");
        
        /// <summary>
        /// SpawnID filter enabled?
        /// </summary>
        public bool bSpawnID => GetMember<BoolType>("bSpawnID");

        /// <summary>
        /// Not near alert filter enabled?
        /// </summary>
        public bool bNotNearAlert => GetMember<BoolType>("bNotNearAlert");

        /// <summary>
        /// Near alert filter enabled?
        /// </summary>
        public bool bNearAlert => GetMember<BoolType>("bNearAlert");

        /// <summary>
        /// No alert filter enabled?
        /// </summary>
        public bool bNoAlert => GetMember<BoolType>("bNoAlert");

        /// <summary>
        /// Alert filter enabled?
        /// </summary>
        public bool bAlert => GetMember<BoolType>("bAlert");

        /// <summary>
        /// Only include LFG spawns?
        /// </summary>
        public bool bLFG => GetMember<BoolType>("bLFG");

        /// <summary>
        /// Only include trader spawns?
        /// </summary>
        public bool bTrader => GetMember<BoolType>("bTrader");

        /// <summary>
        /// Light filter enabled?
        /// </summary>
        public bool bLight => GetMember<BoolType>("bLight");

        /// <summary>
        /// Return next spawn in list after <see cref="FromSpawnID"/>?
        /// </summary>
        public bool bTargNext => GetMember<BoolType>("bTargNext");

        /// <summary>
        /// Return prev spawn in list before <see cref="FromSpawnID"/>?
        /// </summary>
        public bool bTargPrev => GetMember<BoolType>("bTargPrev");

        /// <summary>
        /// Include group members only?
        /// </summary>
        public bool bGroup => GetMember<BoolType>("bGroup");

        /// <summary>
        /// Include fellowship members only?
        /// </summary>
        public bool bFellowship => GetMember<BoolType>("bFellowship");

        /// <summary>
        /// Exclude group members?
        /// </summary>
        public bool bNoGroup => GetMember<BoolType>("bNoGroup");

        /// <summary>
        /// Exclude raid members?
        /// </summary>
        public bool bRaid => GetMember<BoolType>("bRaid");

        /// <summary>
        /// Include GMs only?
        /// </summary>
        public bool bGM => GetMember<BoolType>("bGM");

        /// <summary>
        /// Include named NPCs only?
        /// </summary>
        public bool bNamed => GetMember<BoolType>("bNamed");

        /// <summary>
        /// Include merchants only?
        /// </summary>
        public bool bMerchant => GetMember<BoolType>("bMerchant");

        /// <summary>
        /// Include only bankers
        /// </summary>
        public bool bBanker => GetMember<BoolType>("bBanker");

        /// <summary>
        /// Include tribute masters only?
        /// </summary>
        public bool bTributeMaster => GetMember<BoolType>("bTributeMaster");

        /// <summary>
        /// Include knights (PAL/SHD) only?
        /// </summary>
        public bool bKnight => GetMember<BoolType>("bKnight");

        /// <summary>
        /// Include tanks (WAR/PAL/SHD) only?
        /// </summary>
        public bool bTank => GetMember<BoolType>("bTank");

        /// <summary>
        /// Include healers (CLR/SHM/DRU) only?
        /// </summary>
        public bool bHealer => GetMember<BoolType>("bHealer");

        /// <summary>
        /// Include DPS classes only?
        /// </summary>
        public bool bDps => GetMember<BoolType>("bDps");

        /// <summary>
        /// Include classes (ENC/SHM/BRD) that can slow only?
        /// </summary>
        public bool bSlower => GetMember<BoolType>("bSlower");

        /// <summary>
        /// Not used
        /// </summary>
        public bool bAura => GetMember<BoolType>("bAura");

        /// <summary>
        /// Not used
        /// </summary>
        public bool bBanner => GetMember<BoolType>("bBanner");

        /// <summary>
        /// Not used
        /// </summary>
        public bool bCampfire => GetMember<BoolType>("bCampfire");

        /// <summary>
        /// Exclude spawn with an id of <see cref="SpawnID"/>
        /// </summary>
        public uint? NotID => GetMember<IntType>("NotID");

        /// <summary>
        /// Exclude spawns near a spawn on this alert list
        /// </summary>
        public uint? NotNearAlertList => GetMember<IntType>("NotNearAlertList");

        /// <summary>
        /// Include spawns near a spawn on this alert list
        /// </summary>
        public uint? NearAlertList => GetMember<IntType>("NearAlertList");

        /// <summary>
        /// Exclude spawns on this alert list
        /// </summary>
        public uint? NoAlertList => GetMember<IntType>("NoAlertList");

        /// <summary>
        /// Include spawns on this alert list
        /// </summary>
        public uint? AlertList => GetMember<IntType>("AlertList");

        /// <summary>
        /// Include spawns within this distance of zLoc if set, otherwise character's z location
        /// </summary>
        public double? ZRadius => GetMember<DoubleType>("ZRadius");

        /// <summary>
        /// Include spawns within this 3D distance of the xLoc/yLoc/zLoc if specified, otherwise character's location
        /// </summary>
        public double? FRadius => GetMember<DoubleType>("FRadius");

        /// <summary>
        /// X location to base search around instead of character's
        /// </summary>
        public float? xLoc => GetMember<FloatType>("xLoc");

        /// <summary>
        /// Y location to base search around instead of character's
        /// </summary>
        public float? yLoc => GetMember<FloatType>("yLoc");

        /// <summary>
        /// If true, use xLoc/yLoc/zLoc insted of character's position
        /// </summary>
        public bool bKnownLocation => GetMember<BoolType>("bKnownLocation");

        /// <summary>
        /// Exclude pets?
        /// </summary>
        public bool bNoPet => GetMember<BoolType>("bNoPet");

        /// <summary>
        /// What to sort the list by
        /// 0 = level, 1 = display name (default), 2 = race, 3 = class, 4 = distance (2D, XY), 5 = guild, 6 = id
        /// </summary>
        public uint? SortBy => GetMember<IntType>("SortBy");

        /// <summary>
        /// Exclude spawns in a guild
        /// </summary>
        public bool bNoGuild => GetMember<BoolType>("bNoGuild");

        /// <summary>
        /// Only include spawns you have line of sight to
        /// </summary>
        public bool bLoS => GetMember<BoolType>("bLoS");

        /// <summary>
        /// Match exact name rather than partial
        /// </summary>
        public bool bExactName => GetMember<BoolType>("bExactName");

        /// <summary>
        /// Include only targetable spawns
        /// </summary>
        public bool bTargetable => GetMember<BoolType>("bTargetable");

        /// <summary>
        /// Bitmask of player states to include
        /// </summary>
        public uint? PlayerState => GetMember<IntType>("PlayerState");

        /// <summary>
        /// Return the first spawn matching the ID or Name filters (ignores all other filters)
        /// </summary>
        public SpawnType Spawn => GetMember<SpawnType>("Spawn");

        public override string ToString()
        {
            return nameof(AlertListType);
        }
    }
}