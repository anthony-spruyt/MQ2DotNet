using JetBrains.Annotations;
using System;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a spawn.
    /// Last Verified: 2023-06-29
    /// </summary>
    [PublicAPI]
    [MQ2Type("spawn")]
    public class SpawnType : MQ2DataType
    {
        internal SpawnType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _spawnStatus = new IndexedMember<IntType, int>(this, "SpawnStatus");
            _seeInvis = new IndexedMember<IntType, int>(this, "SeeInvis");
            _nearestSpawn = new IndexedMember<SpawnType, int, SpawnType, string>(this, "NearestSpawn");
            _headingToLoc = new IndexedMember<HeadingType>(this, "HeadingToLoc");
            _equipment = new IndexedMember<IntType, int, IntType, string>(this, "Equipment");
            _dragNames = new IndexedStringMember<int>(this, "DragNames");
            _combatSkillTicks = new IndexedMember<IntType, int>(this, "CombatSkillTicks");
            _cachedBuff = new IndexedMember<CachedBuffType>(this, "CachedBuff");
        }

        /// <summary>
        /// Create a SpawnType from a pointer to a SPAWNINFO struct
        /// </summary>
        /// <param name="mq2TypeFactory"></param>
        /// <param name="pSpawn"></param>
        public SpawnType(MQ2TypeFactory mq2TypeFactory, IntPtr pSpawn) 
            : base("spawn", mq2TypeFactory, new MQ2VarPtr(pSpawn))
        {
            _spawnStatus = new IndexedMember<IntType, int>(this, "SpawnStatus");
            _seeInvis = new IndexedMember<IntType, int>(this, "SeeInvis");
            _nearestSpawn = new IndexedMember<SpawnType, int, SpawnType, string>(this, "NearestSpawn");
            _headingToLoc = new IndexedMember<HeadingType>(this, "HeadingToLoc");
            _equipment = new IndexedMember<IntType, int, IntType, string>(this, "Equipment");
            _dragNames = new IndexedStringMember<int>(this, "DragNames");
            _combatSkillTicks = new IndexedMember<IntType, int>(this, "CombatSkillTicks");
            _cachedBuff = new IndexedMember<CachedBuffType>(this, "CachedBuff");
        }

        /// <summary>
        /// Targets the spawn (equivalent of /target)
        /// </summary>
        public void DoTarget() => GetMember<MQ2DataType>("DoTarget");

        /// <summary>
        /// Faces the spawn (equivalent of /face)
        /// </summary>
        public void DoFace() => GetMember<MQ2DataType>("DoFace");

        /// <summary>
        /// Left click on the spawn
        /// </summary>
        public void LeftClick() => GetMember<MQ2DataType>("LeftClick");

        /// <summary>
        /// Right click on the spawn
        /// </summary>
        public void RightClick() => GetMember<MQ2DataType>("RightClick");

        /// <summary>
        /// Assists the spawn (equivalent of /assist)
        /// </summary>
        public void DoAssist() => GetMember<MQ2DataType>("DoAssist");

        /// <summary>
        /// Level of the spawn
        /// </summary>
        public uint? Level => GetMember<IntType>("Level");

        /// <summary>
        /// Spawn's ID
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Internal name of the spawn e.g. a_rat01
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Last name
        /// </summary>
        public string Surname => GetMember<StringType>("Surname");

        /// <summary>
        /// The "cleaned up" name
        /// </summary>
        public string CleanName => GetMember<StringType>("CleanName");

        /// <summary>
        /// Name displayed in game (same as EQ's %T)
        /// </summary>
        public string DisplayName => GetMember<StringType>("DisplayName");

        /// <summary>
        /// Shortcut for -X (makes Eastward positive)
        /// </summary>
        public float? E => GetMember<FloatType>("E");

        /// <summary>
        /// X, the Northward-positive coordinate
        /// </summary>
        public float? X => GetMember<FloatType>("X");

        /// <summary>
        /// Same as <see cref="X"/>
        /// </summary>
        public float? W => X;

        /// <summary>
        /// Shortcut for -Y (makes Southward positive)
        /// </summary>
        public float? S => GetMember<FloatType>("S");

        /// <summary>
        /// Y, the Westward-positive coordinate
        /// </summary>
        public float? Y => GetMember<FloatType>("Y");

        /// <summary>
        /// Same as <see cref="Y"/>
        /// </summary>
        public float? N => Y;

        /// <summary>
        /// Shortcut for -Z (makes Downward positive)
        /// </summary>
        public float? D => GetMember<FloatType>("D");

        /// <summary>
        /// Z, the Upward-positive coordinate
        /// </summary>
        public float? Z => GetMember<FloatType>("Z");

        /// <summary>
        /// Same as <see cref="Z"/>
        /// </summary>
        public float? U => Z;

        /// <summary>
        /// Floor z value at the spawn's location
        /// </summary>
        public float? FloorZ => GetMember<FloatType>("FloorZ");

        /// <summary>
        /// Next spawn in the linked list
        /// </summary>
        [JsonIgnore]
        public SpawnType Next => GetMember<SpawnType>("Next");

        /// <summary>
        /// Next spawn in EQ's favourite data structure
        /// </summary>
        [JsonIgnore]
        public SpawnType Prev => GetMember<SpawnType>("Prev");

        /// <summary>
        /// Current hit points
        /// </summary>
        public long? CurrentHPs => GetMember<Int64Type>("CurrentHPs");

        /// <summary>
        /// Maximum hit points
        /// </summary>
        public long? MaxHPs => GetMember<Int64Type>("MaxHPs");

        /// <summary>
        /// HP as a percentage
        /// </summary>
        public long? PctHPs => GetMember<Int64Type>("PctHPs");

        /// <summary>
        /// Dunno wtf this is or why I would care about it
        /// </summary>
        public int? AARank => GetMember<IntType>("AARank");

        /// <summary>
        /// Speed as a percentage of regular run speed
        /// </summary>
        public float? Speed => GetMember<FloatType>("Speed");

        /// <summary>
        /// Direction the spawn is facing
        /// </summary>
        public HeadingType Heading => GetMember<HeadingType>("Heading");

        /// <summary>
        /// Spawn's pet
        /// </summary>

        [JsonIgnore]
        public PetType Pet => GetMember<PetType>("Pet");

        /// <summary>
        /// Master, if it is charmed or a pet
        /// </summary>
        [JsonIgnore]
        public SpawnType Master => GetMember<SpawnType>("Master");

        /// <summary>
        /// Gender
        /// TODO: map to enum
        /// </summary>
        public string Gender => GetMember<StringType>("Gender");

        /// <summary>
        /// Spawn's race
        /// </summary>
        public RaceType Race => GetMember<RaceType>("Race");

        /// <summary>
        /// Class
        /// </summary>
        public ClassType Class => GetMember<ClassType>("Class");

        /// <summary>
        /// Body type
        /// TODO: map to enum?
        /// </summary>
        public BodyType Body => GetMember<BodyType>("Body");

        /// <summary>
        /// GM or Guide?
        /// </summary>
        public bool GM => GetMember<BoolType>("GM");

        /// <summary>
        /// Spawn is levitating?
        /// </summary>
        public bool Levitating => GetMember<BoolType>("Levitating");

        /// <summary>
        /// Sneaking?
        /// </summary>
        public bool Sneaking => GetMember<BoolType>("Sneaking");

        /// <summary>
        /// Invis?
        /// </summary>
        public bool Invis => GetMember<BoolType>("Invis");


        // TODO continue fixing from here ^


        //f000000000000000000000000000000;
        //f000000000000000000000000000000;
        //f000000000000000000000000000000;
        //f000000000000000000000000000000;
        //f000000000000000000000000000000;
        //f000000000000000000000000000000;
        //f000000000000000000000000000000;
        //f000000000000000000000000000000;








        
        
        /// <summary>
        /// ActorDef name for this spawn
        /// </summary>
        public string ActorDef => GetMember<StringType>("ActorDef");
        
        /// <summary>
        /// AFK flag set?
        /// </summary>
        public bool AFK => GetMember<BoolType>("AFK");
        
        /// <summary>
        /// returns TRUE or FALSE if a mob is aggressive or not
        /// </summary>
        public bool Aggressive => GetMember<BoolType>("Aggressive");
        
        /// <summary>
        /// Current animation ID, see https://www.macroquest2.com/wiki/index.php/Animations
        /// </summary>
        public int? Animation => GetMember<IntType>("Animation");
        
        /// <summary>
        /// Anon flag set
        /// </summary>
        public bool Anonymous => GetMember<BoolType>("Anonymous");
        
        /// <summary>
        /// Current Raid or Group assist target?
        /// </summary>
        public bool Assist => GetMember<BoolType>("Assist");
        
        /// <summary>
        /// TODO: SpawnType.AssistName is always blank?
        /// </summary>
        public string AssistName => GetMember<StringType>("AssistName");
        
        /// <summary>
        /// TODO: What is SpawnType.bAlwaysShowAura
        /// </summary>
        public bool bAlwaysShowAura => GetMember<BoolType>("bAlwaysShowAura");
        
        /// <summary>
        /// TODO: What is SpawnType.bBetaBuffed
        /// </summary>
        public bool bBetaBuffed => GetMember<BoolType>("bBetaBuffed");
        
        /// <summary>
        /// Returns stupid numbers
        /// </summary>
        [Obsolete]
        public float? BearingToTarget => GetMember<FloatType>("BearingToTarget");
        
        /// <summary>
        /// Binding wounds?
        /// </summary>
        public bool Binding => GetMember<BoolType>("Binding");
        
        /// <summary>
        /// Blind?
        /// </summary>
        public int? Blind => GetMember<IntType>("Blind");
        
        /// <summary>
        /// Seems broken and useless even if it wasn't
        /// </summary>
        public bool bShowHelm => GetMember<BoolType>("bShowHelm");
        
        /// <summary>
        /// True for stationary spawns maybe? Returns FALSE for me when I'm standing still
        /// </summary>
        public bool bStationary => GetMember<BoolType>("bStationary");
        
        /// <summary>
        /// Is the spawn a temp pet?
        /// </summary>
        public bool bTempPet => GetMember<BoolType>("bTempPet");
        
        /// <summary>
        /// Is a buyer? (ie. Buyer in the bazaar)
        /// </summary>
        public bool Buyer => GetMember<BoolType>("Buyer");
        
        /// <summary>
        /// No idea
        /// </summary>
        public bool bWaitingForPort => GetMember<BoolType>("bWaitingForPort");
        
        /// <summary>
        /// TRUE/FALSE on if a splash spell can land...NOTE! This check is ONLY for line of sight to the targetindicator (red/green circle)
        /// </summary>
        public bool CanSplashLand => GetMember<BoolType>("CanSplashLand");
        
        /// <summary>
        /// Spell, if currently casting (only accurate on yourself, not NPCs or other group members)
        /// </summary>
        public SpellType Casting => GetMember<SpellType>("Casting");
        
        /// <summary>
        /// Ceiling height at the spawn's current location
        /// </summary>
        public float? CeilingHeightAtCurrLocation => GetMember<FloatType>("CeilingHeightAtCurrLocation");
        
        /// <summary>
        /// Collision counter
        /// </summary>
        public int? CollisionCounter => GetMember<IntType>("CollisionCounter");
        
        /// <summary>
        /// Valid indexes are 0 and 1. TODO: What is SpawnType.CombatSkillTicks
        /// </summary>
        private IndexedMember<IntType, int> _combatSkillTicks;
        
        /// <summary>
        /// Spawn ID of this spawn's contractor
        /// </summary>
        public int? ContractorID => GetMember<IntType>("ContractorID");
        
        /// <summary>
        /// Returns weird numbers
        /// </summary>
        [Obsolete]
        public int? CorpseDragCount => GetMember<IntType>("CorpseDragCount");
        
        /// <summary>
        /// Current Endurance points (only updates when target/group)
        /// </summary>
        public int? CurrentEndurance => GetMember<IntType>("CurrentEndurance");
        
        /// <summary>
        /// Current Mana points (only updates when target/group)
        /// </summary>
        public int? CurrentMana => GetMember<IntType>("CurrentMana");
        
        /// <summary>
        /// Dead?
        /// </summary>
        public bool Dead => GetMember<BoolType>("Dead");
        
        /// <summary>
        /// Deity
        /// </summary>
        public DeityType Deity => GetMember<DeityType>("Deity");
        
        /// <summary>
        /// 2D distance to the spawn in the XY plane
        /// </summary>
        public float? Distance => GetMember<FloatType>("Distance");
        
        /// <summary>
        /// 3D distance to the spawn in the XYZ plane
        /// </summary>
        public float? Distance3D => GetMember<FloatType>("Distance3D");
        
        /// <summary>
        /// 2D distance to the spawn in the XY plane, taking into account the spawn's movement but not the player's
        /// </summary>
        public float? DistancePredict => GetMember<FloatType>("DistancePredict");
        
        /// <summary>
        /// 1D distance to the spawn in the X plane
        /// </summary>
        public float? DistanceX => GetMember<FloatType>("DistanceX");
        
        /// <summary>
        /// 1D distance to the spawn in the Y plane
        /// </summary>
        public float? DistanceY => GetMember<FloatType>("DistanceY");
        
        /// <summary>
        /// 1D distance to the spawn in the Z plane
        /// </summary>
        public float? DistanceZ => GetMember<FloatType>("DistanceZ");
        
        /// <summary>
        /// Player this corpse is being dragged by
        /// </summary>
        public string DraggingPlayer => GetMember<StringType>("DraggingPlayer");
        
        /// <summary>
        /// Players whose corpse this spawn is dragging. Valid indexes are 0 and 1
        /// </summary>
        private IndexedStringMember<int> _dragNames;
        
        /// <summary>
        /// Ducking?
        /// </summary>
        public bool Ducking => GetMember<BoolType>("Ducking");
        
        /// <summary>
        /// Location using EQ format
        /// </summary>
        public string EQLoc => GetMember<StringType>("EQLoc");
        /// <summary>
        /// ID of the equipment used by the spawn
        /// returns a inttype, it takes numbers 0-8 or names: head chest arms wrists hands legs feet primary offhand
        /// </summary>
        private IndexedMember<IntType, int, IntType, string> _equipment;
        
        /// <summary>
        /// TODO: What is SpawnType.FD?
        /// </summary>
        public int? FD => GetMember<IntType>("FD");
        
        /// <summary>
        /// Feet wet/swimming?
        /// </summary>
        public bool FeetWet => GetMember<BoolType>("FeetWet");
        
        /// <summary>
        /// Feigning?
        /// </summary>
        public bool Feigning => GetMember<BoolType>("Feigning");
        
        /// <summary>
        /// Is your target moving away from you?
        /// </summary>
        public bool Fleeing => GetMember<BoolType>("Fleeing");
        
        /// <summary>
        /// The spawn a player is following using /follow on - also returns your pet's target via ${Me.Pet.Following}
        /// </summary>
        [JsonIgnore]
        public SpawnType Following => GetMember<SpawnType>("Following");
        
        /// <summary>
        /// GM rank
        /// </summary>
        public int? GMRank => GetMember<IntType>("GMRank");
        
        /// <summary>
        /// Name of the spawn's guild
        /// </summary>
        public string Guild => GetMember<StringType>("Guild");
        
        /// <summary>
        /// Guild status (Leader, Officer, Member)
        /// </summary>
        public string GuildStatus => GetMember<StringType>("GuildStatus");
        
        /// <summary>
        /// Heading player must travel in to reach this spawn
        /// </summary>
        public HeadingType HeadingTo => GetMember<HeadingType>("HeadingTo");
        
        /// <summary>
        /// Heading to the coordinates y,x from the spawn
        /// </summary>
        private IndexedMember<HeadingType> _headingToLoc;
        
        /// <summary>
        /// Height
        /// </summary>
        public float? Height => GetMember<FloatType>("Height");
        
        /// <summary>
        /// Represents what the pc/npc is holding
        /// </summary>
        public bool Holding => GetMember<BoolType>("Holding");
        
        /// <summary>
        /// Holding animation
        /// </summary>
        public int? HoldingAnimation => GetMember<IntType>("HoldingAnimation");
        
        /// <summary>
        /// Hovering?
        /// </summary>
        public bool Hovering => GetMember<BoolType>("Hovering");
        
        /// <summary>
        /// In a PvP area?
        /// </summary>
        public int? InPvPArea => GetMember<IntType>("InPvPArea");
        
        /// <summary>
        /// Spawn has been invited to a group
        /// </summary>
        public bool Invited => GetMember<BoolType>("Invited");
        
        /// <summary>
        /// Who invited the spawn to a group?
        /// </summary>
        public string Inviter => GetMember<StringType>("Inviter");
        
        /// <summary>
        /// Spawn is berserk?
        /// </summary>
        public int? IsBerserk => GetMember<IntType>("IsBerserk");
        
        /// <summary>
        /// Spawn is a passenger?
        /// </summary>
        public int? IsPassenger => GetMember<IntType>("IsPassenger");
        
        /// <summary>
        /// If it's a summoned being (pet for example). Unsure if useful for druid nukes.
        /// </summary>
        public bool IsSummoned => GetMember<BoolType>("IsSummoned");
        
        /// <summary>
        /// TODO: What is SpawnType.LastCastNum
        /// </summary>
        public int? LastCastNum => GetMember<IntType>("LastCastNum");
        
        /// <summary>
        /// TODO: What is SpawnType.LastCastTime
        /// </summary>
        public int? LastCastTime => GetMember<IntType>("LastCastTime");
        
        /// <summary>
        /// LFG flag set?
        /// </summary>
        public bool LFG => GetMember<BoolType>("LFG");
        
        /// <summary>
        /// Name of the light class this spawn has
        /// </summary>
        public string Light => GetMember<StringType>("Light");
        
        /// <summary>
        /// Linkdead?
        /// </summary>
        public bool Linkdead => GetMember<BoolType>("Linkdead");
        
        /// <summary>
        /// Loc of the spawn (Y, X)
        /// </summary>
        public string Loc => GetMember<StringType>("Loc");
        
        /// <summary>
        /// Loc of the spawn (Y, X)
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string LocYX => GetMember<StringType>("LocYX");
        
        /// <summary>
        /// Loc of the spawn (Y, X, Z)
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string LocYXZ => GetMember<StringType>("LocYXZ");
        
        /// <summary>
        /// Looking this angle
        /// </summary>
        public float? Look => GetMember<FloatType>("Look");
        
        /// <summary>
        /// Current Raid or Group marked npc mark number (raid first)
        /// </summary>
        public int? Mark => GetMember<IntType>("Mark");
        
        /// <summary>
        /// Maximum Endurance points (only updates when target/group)
        /// </summary>
        public int? MaxEndurance => GetMember<IntType>("MaxEndurance");
        
        /// <summary>
        /// Maximum Mana points (only updates when target/group)
        /// </summary>
        public int? MaxMana => GetMember<IntType>("MaxMana");
        
        /// <summary>
        /// The max distance from this spawn for it to hit you
        /// </summary>
        public float? MaxRange => GetMember<FloatType>("MaxRange");
        
        /// <summary>
        /// The Max distance from this spawn for you to hit it
        /// </summary>
        public float? MaxRangeTo => GetMember<FloatType>("MaxRangeTo");
        
        /// <summary>
        /// Spawn ID of this spawn's contractor
        /// </summary>
        public int? MercID => GetMember<IntType>("MercID");
        
        /// <summary>
        /// This spawn's mount 
        /// </summary>
        [JsonIgnore]
        public SpawnType Mount => GetMember<SpawnType>("Mount");
        
        /// <summary>
        /// Moving?
        /// </summary>
        public bool Moving => GetMember<BoolType>("Moving");
        
        /// <summary>
        /// Location using MQ format (Y, X, Z)
        /// </summary>
        public string MQLoc => GetMember<StringType>("MQLoc");
        
        /// <summary>
        /// Is this a "named" spawn (ie. does it's name not start with an "a" or an "an", plus a bunch of other checks. See IsNamed() in MQ2Utilities.cpp)
        /// </summary>
        public bool Named => GetMember<BoolType>("Named");
        
        /// <summary>
        /// Nth closest spawn to this spawn, or the nth closest matching a search string e.g. "2,npc" for the second closest NPC
        /// </summary>
        private IndexedMember<SpawnType, int, SpawnType, string> _nearestSpawn;
        
        /// <summary>
        /// Owner, if mercenary
        /// </summary>
        [JsonIgnore]
        public SpawnType Owner => GetMember<SpawnType>("Owner");
        
        /// <summary>
        /// Endurance as a percentage
        /// </summary>
        public int? PctEndurance => GetMember<IntType>("PctEndurance");
        
        /// <summary>
        /// Mana as a percentage
        /// </summary>
        public int? PctMana => GetMember<IntType>("PctMana");
        
        /// <summary>
        /// returns a mask as an inttype which has the following meaning:
        /// 0=Idle 1=Open 2=WeaponSheathed 4=Aggressive 8=ForcedAggressive 0x10=InstrumentEquipped 0x20=Stunned 0x40=PrimaryWeaponEquipped 0x80=SecondaryWeaponEquipped
        /// </summary>
        public int? PlayerState => GetMember<IntType>("PlayerState");
        
        /// <summary>
        /// Item ID of anything that may be in the Primary slot
        /// </summary>
        public int? Primary => GetMember<IntType>("Primary");
        
        /// <summary>
        /// TODO: What is SpawnType.pTouchingSwitch
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int? pTouchingSwitch => GetMember<IntType>("pTouchingSwitch");
        
        /// <summary>
        /// Roleplaying flag set?
        /// </summary>
        public bool Roleplaying => GetMember<BoolType>("Roleplaying");
        
        /// <summary>
        /// Item ID of anything that may be in the Secondary slot
        /// </summary>
        public int? Secondary => GetMember<IntType>("Secondary");
        
        /// <summary>
        /// Spawn can see invis, takes an index of 0 - 2, guessing for invis/invis vs undead/improved invis?
        /// TODO: Confirm function of SpawnType.SeeInvis
        /// </summary>
        private IndexedMember<IntType, int> _seeInvis;
        
        /// <summary>
        /// Sitting?
        /// </summary>
        public bool Sitting => GetMember<BoolType>("Sitting");
        
        /// <summary>
        /// Spawn status, takes an index of 0 - 5. TODO: Confirm what they mean
        /// </summary>
        private IndexedMember<IntType, int> _spawnStatus;
        
        /// <summary>
        /// Standing?
        /// </summary>
        public bool Standing => GetMember<BoolType>("Standing");
        
        /// <summary>
        /// StandState
        /// </summary>
        public int? StandState => GetMember<IntType>("StandState");
        
        /// <summary>
        /// STAND, SIT, DUCK, BIND, FEIGN, DEAD, STUN, HOVER, MOUNT, UNKNOWN
        /// </summary>
        public string State => GetMember<StringType>("State");
        
        /// <summary>
        /// Stuck?
        /// </summary>
        public bool Stuck => GetMember<BoolType>("Stuck");
        
        /// <summary>
        /// Stunned?
        /// </summary>
        public bool Stunned => GetMember<BoolType>("Stunned");
        
        /// <summary>
        /// Suffix attached to name, eg. of servername
        /// </summary>
        public string Suffix => GetMember<StringType>("Suffix");
        
        /// <summary>
        /// Target of this spawn's target
        /// </summary>
        [JsonIgnore]
        public SpawnType TargetOfTarget => GetMember<SpawnType>("TargetOfTarget");
        
        /// <summary>
        /// Time this spawn has been dead for
        /// </summary>
        public TimeStampType TimeBeenDead => GetMember<TimeStampType>("TimeBeenDead");
        
        /// <summary>
        /// Prefix/Title before name
        /// </summary>
        public string Title => GetMember<StringType>("Title");
        
        /// <summary>
        /// Trader (in bazaar)?
        /// </summary>
        public bool Trader => GetMember<BoolType>("Trader");
        
        /// <summary>
        /// PC, NPC, Untargetable, Mount, Pet, Corpse, Chest, Trigger, Trap, Timer, Item, Mercenary, Aura, Object, Banner, Campfire, Flyer
        /// </summary>
        public string Type => GetMember<StringType>("Type");
        
        /// <summary>
        /// Underwater?
        /// </summary>
        public bool Underwater => GetMember<BoolType>("Underwater");
        
        /// <summary>
        /// TODO: What is SpawnType.WarCry?
        /// </summary>
        public int? WarCry => GetMember<IntType>("WarCry");
        
        /// <summary>
        /// GREY, GREEN, LIGHT BLUE, BLUE, WHITE, YELLOW, RED
        /// </summary>
        public string ConColor => GetMember<StringType>("ConColor");
        
        /// <summary>
        /// Group leader?
        /// </summary>
        public bool GroupLeader => GetMember<BoolType>("GroupLeader");
        
        /// <summary>
        /// Returns TRUE if spawn is in LoS
        /// </summary>
        public bool LineOfSight => GetMember<BoolType>("LineOfSight");
        
        /// <summary>
        /// Spawn can be targetted?
        /// </summary>
        public bool Targetable => GetMember<BoolType>("Targetable");
        
        /// <summary>
        /// This is fucked, not dealing with it
        /// </summary>
        private IndexedMember<CachedBuffType> _cachedBuff;
        
        /// <summary>
        /// Number of cached buffs
        /// </summary>
        public int? CachedBuffCount => GetMember<IntType>("CachedBuffCount");
    }
}