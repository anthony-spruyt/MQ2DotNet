using JetBrains.Annotations;
using MQ2DotNet.EQ;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    public struct SpawnLocation
    {
        public float X;
        public float Y;
        public float Z;
    }

    /// <summary>
    /// TODO: Update members and methods according to doco and implement indexed member wrapper methods and properties.
    /// Represents an in-game spawn.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-spawn/
    /// </summary>
    [PublicAPI]
    [MQ2Type("spawn")]
    public class SpawnType : MQ2DataType
    {
        internal SpawnType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _invis = new IndexedMember<BoolType, int, BoolType, string>(this, "Invis");
            _nearestSpawn = new IndexedMember<SpawnType, int, SpawnType, string>(this, "NearestSpawn");
            _headingToLoc = new IndexedMember<HeadingType>(this, "HeadingToLoc");
            _equipment = new IndexedMember<IntType, int, IntType, string>(this, "Equipment");
            _combatSkillTicks = new IndexedMember<IntType, int>(this, "CombatSkillTicks");
            _seeInvis = new IndexedMember<IntType, int>(this, "SeeInvis");
            _spawnStatus = new IndexedMember<IntType, int>(this, "SpawnStatus");
            _cachedBuff = new IndexedMember<CachedBuffType, int, CachedBuffType, string>(this, "CachedBuff");
            _buff = new IndexedMember<CachedBuffType, int, CachedBuffType, string>(this, "Buff");
            _findBuff = new IndexedMember<CachedBuffType>(this, "FindBuff");
            _myBuff = new IndexedMember<CachedBuffType, int, CachedBuffType, string>(this, "MyBuff");
            _buffDuration = new IndexedMember<TimeStampType, int, TimeStampType, string>(this, "BuffDuration");
            _myBuffDuration = new IndexedMember<TimeStampType, int, TimeStampType, string>(this, "MyBuffDuration");
        }

        /// <summary>
        /// Create a SpawnType from a pointer to a SPAWNINFO struct
        /// </summary>
        /// <param name="mq2TypeFactory"></param>
        /// <param name="pSpawn"></param>
        public SpawnType(MQ2TypeFactory mq2TypeFactory, IntPtr pSpawn) 
            : base("spawn", mq2TypeFactory, new MQ2VarPtr(pSpawn))
        {
            _invis = new IndexedMember<BoolType, int, BoolType, string>(this, "Invis");
            _nearestSpawn = new IndexedMember<SpawnType, int, SpawnType, string>(this, "NearestSpawn");
            _headingToLoc = new IndexedMember<HeadingType>(this, "HeadingToLoc");
            _equipment = new IndexedMember<IntType, int, IntType, string>(this, "Equipment");
            _combatSkillTicks = new IndexedMember<IntType, int>(this, "CombatSkillTicks");
            _seeInvis = new IndexedMember<IntType, int>(this, "SeeInvis");
            _spawnStatus = new IndexedMember<IntType, int>(this, "SpawnStatus");
            _cachedBuff = new IndexedMember<CachedBuffType, int, CachedBuffType, string>(this, "CachedBuff");
            _buff = new IndexedMember<CachedBuffType, int, CachedBuffType, string>(this, "Buff");
            _findBuff = new IndexedMember<CachedBuffType>(this, "FindBuff");
            _myBuff = new IndexedMember<CachedBuffType, int, CachedBuffType, string>(this, "MyBuff");
            _buffDuration = new IndexedMember<TimeStampType, int, TimeStampType, string>(this, "BuffDuration");
            _myBuffDuration = new IndexedMember<TimeStampType, int, TimeStampType, string>(this, "MyBuffDuration");
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
        private readonly IndexedMember<BoolType, int, BoolType, string> _invis;

        /// <summary>
        /// Invisibile? (Any type)
        /// </summary>
        public bool Invis => _invis[""];

        /// <summary>
        /// Invisibile based on type?
        /// </summary>
        /// <param name="invisType"></param>
        /// <returns></returns>
        public bool IsInvis(InvisMode invisType) => _invis[(int)invisType];

        /// <summary>
        /// Height
        /// </summary>
        public float? Height => GetMember<FloatType>("Height");

        /// <summary>
        /// The max distance from this spawn for it to hit you
        /// </summary>
        public float? MaxRange => GetMember<FloatType>("MaxRange");

        /// <summary>
        /// The Max distance from this spawn for you to hit it
        /// </summary>
        public float? MaxRangeTo => GetMember<FloatType>("MaxRangeTo");

        /// <summary>
        /// Name of the spawn's guild
        /// </summary>
        public string Guild => GetMember<StringType>("Guild");

        /// <summary>
        /// PC, NPC, Untargetable, Mount, Pet, Corpse, Chest, Trigger, Trap, Timer, Item, Mercenary, Aura, Object, Banner, Campfire, Flyer
        /// TODO: map to an enum
        /// </summary>
        public string Type => GetMember<StringType>("Type");

        /// <summary>
        /// Name of the light class this spawn has
        /// </summary>
        public string Light => GetMember<StringType>("Light");

        /// <summary>
        /// StandState
        /// </summary>
        public int? StandState => GetMember<IntType>("StandState");

        /// <summary>
        /// STAND, SIT, DUCK, BIND, FEIGN, DEAD, STUN, HOVER, MOUNT, UNKNOWN
        /// </summary>
        public SpawnState? State => GetMember<StringType>("State");

        /// <summary>
        /// Standing?
        /// </summary>
        public bool Standing => GetMember<BoolType>("Standing");

        /// <summary>
        /// Sitting?
        /// </summary>
        public bool Sitting => GetMember<BoolType>("Sitting");

        /// <summary>
        /// Time this spawn has been dead for
        /// </summary>
        public TimeSpan? TimeBeenDead => GetMember<TimeStampType>("TimeBeenDead");

        /// <summary>
        /// If it's a summoned being (pet for example). Unsure if useful for druid nukes.
        /// </summary>
        public bool IsSummoned => GetMember<BoolType>("IsSummoned");

        /// <summary>
        /// Target of this spawn's target
        /// </summary>
        [JsonIgnore]
        public SpawnType TargetOfTarget => GetMember<SpawnType>("TargetOfTarget");

        /// <summary>
        /// Ducking?
        /// </summary>
        public bool Ducking => GetMember<BoolType>("Ducking");

        /// <summary>
        /// Feigning?
        /// </summary>
        public bool Feigning => GetMember<BoolType>("Feigning");

        /// <summary>
        /// Binding wounds?
        /// </summary>
        public bool Binding => GetMember<BoolType>("Binding");

        /// <summary>
        /// Dead?
        /// </summary>
        public bool Dead => GetMember<BoolType>("Dead");

        /// <summary>
        /// Stunned?
        /// </summary>
        public bool Stunned => GetMember<BoolType>("Stunned");

        /// <summary>
        /// returns TRUE or FALSE if a mob is aggressive or not
        /// </summary>
        public bool Aggressive => GetMember<BoolType>("Aggressive");

        /// <summary>
        /// Hovering?
        /// </summary>
        public bool Hovering => GetMember<BoolType>("Hovering");

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
        /// See <see cref="DistanceX"/>
        /// </summary>
        public float? DistanceW => DistanceX;

        /// <summary>
        /// 1D distance to the spawn in the Y plane
        /// </summary>
        public float? DistanceY => GetMember<FloatType>("DistanceY");

        /// <summary>
        /// See <see cref="DistanceY"/>
        /// </summary>
        public float? DistanceN => DistanceY;

        /// <summary>
        /// 1D distance to the spawn in the Z plane
        /// </summary>
        public float? DistanceZ => GetMember<FloatType>("DistanceZ");

        /// <summary>
        /// See <see cref="DistanceZ"/>
        /// </summary>
        public float? DistanceU => DistanceZ;

        /// <summary>
        /// Heading player must travel in to reach this spawn
        /// </summary>
        public HeadingType HeadingTo => GetMember<HeadingType>("HeadingTo");

        /// <summary>
        /// Spell, if currently casting (only accurate on yourself, not NPCs or other group members)
        /// </summary>
        public SpellType Casting => GetMember<SpellType>("Casting");

        /// <summary>
        /// This spawn's mount 
        /// </summary>
        [JsonIgnore]
        public SpawnType Mount => GetMember<SpawnType>("Mount");

        /// <summary>
        /// Underwater?
        /// </summary>
        public bool Underwater => GetMember<BoolType>("Underwater");

        /// <summary>
        /// Feet wet/swimming?
        /// </summary>
        public bool FeetWet => GetMember<BoolType>("FeetWet");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool BodyWet => GetMember<BoolType>("BodyWet");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool HeadWet => GetMember<BoolType>("HeadWet");

        /// <summary>
        /// returns a mask as an inttype which has the following meaning:
        /// 0=Idle 1=Open 2=WeaponSheathed 4=Aggressive 8=ForcedAggressive 0x10=InstrumentEquipped 0x20=Stunned 0x40=PrimaryWeaponEquipped 0x80=SecondaryWeaponEquipped
        /// TODO: flags enum
        /// </summary>
        public uint? PlayerState => GetMember<IntType>("PlayerState");

        /// <summary>
        /// Stuck?
        /// </summary>
        public bool Stuck => GetMember<BoolType>("Stuck");

        /// <summary>
        /// Current animation ID, see https://www.macroquest2.com/wiki/index.php/Animations
        /// </summary>
        public uint? Animation => GetMember<IntType>("Animation");

        /// <summary>
        /// Represents if the pc/npc is holding anything?
        /// if (pSpawn->LeftHolding || pSpawn->RightHolding)
        /// </summary>
        public bool Holding => GetMember<BoolType>("Holding");

        /// <summary>
        /// Looking this angle
        /// </summary>
        public float? Look => GetMember<FloatType>("Look");

        /// <summary>
        /// GREY, GREEN, LIGHT BLUE, BLUE, WHITE, YELLOW, RED
        /// </summary>
        public ConColor? ConColor => GetMember<StringType>("ConColor");

        /// <summary>
        /// Nth closest spawn to this spawn, or the nth closest matching a search string e.g. "2,npc" for the second closest NPC
        /// </summary>
        private readonly IndexedMember<SpawnType, int, SpawnType, string> _nearestSpawn;

        /// <summary>
        /// Get the nth closest matching a search string e.g. "2,npc" for the second closest NPC
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SpawnType GetClosestSpawn(string query) => _nearestSpawn[query];

        /// <summary>
        /// Get the nth closest NPC.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SpawnType GetClosestNPC(int nth = 1) => _nearestSpawn[$"{nth},npc"];

        /// <summary>
        /// Get the nth closest spawn to this spawn.
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public SpawnType GetClosestSpawn(int nth = 1) => _nearestSpawn[nth];

        /// <summary>
        /// Trader (in bazaar)?
        /// </summary>
        public bool Trader => GetMember<BoolType>("Trader");

        /// <summary>
        /// AFK flag set?
        /// </summary>
        public bool AFK => GetMember<BoolType>("AFK");

        /// <summary>
        /// LFG flag set?
        /// </summary>
        public bool LFG => GetMember<BoolType>("LFG");

        /// <summary>
        /// Linkdead?
        /// </summary>
        public bool Linkdead => GetMember<BoolType>("Linkdead");

        /// <summary>
        /// Prefix/Title before name
        /// </summary>
        public string Title => GetMember<StringType>("Title");

        /// <summary>
        /// Leaving this in for older macros/etc..<see cref="Title"/> should be used instead.
        /// </summary>
        [Obsolete]
        public string AATitle => Title;

        /// <summary>
        /// Suffix attached to name, eg. of servername
        /// </summary>
        public string Suffix => GetMember<StringType>("Suffix");

        /// <summary>
        /// Group leader?
        /// </summary>
        public bool GroupLeader => GetMember<BoolType>("GroupLeader");

        /// <summary>
        /// Current Raid or Group assist target?
        /// </summary>
        public bool Assist => GetMember<BoolType>("Assist");

        /// <summary>
        /// Current Raid or Group marked npc mark number (raid first)
        /// </summary>
        public int? Mark => GetMember<IntType>("Mark");

        /// <summary>
        /// Anon flag set
        /// </summary>
        public bool Anonymous => GetMember<BoolType>("Anonymous");

        /// <summary>
        /// Roleplaying flag set?
        /// </summary>
        public bool Roleplaying => GetMember<BoolType>("Roleplaying");

        /// <summary>
        /// Returns TRUE if spawn is in LoS
        /// </summary>
        public bool LineOfSight => GetMember<BoolType>("LineOfSight");

        /// <summary>
        /// Heading to the coordinates y,x from the spawn
        /// </summary>
        private readonly IndexedMember<HeadingType> _headingToLoc;

        /// <summary>
        /// Heading to the coordinates y,x from the spawn
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public HeadingType GetHeading(int x, int y) => _headingToLoc[$"{y},{x}"];

        /// <summary>
        /// Is your target moving away from you?
        /// </summary>
        public bool Fleeing => GetMember<BoolType>("Fleeing");

        /// <summary>
        /// Is this a "named" spawn (ie. does it's name not start with an "a" or an "an", plus a bunch of other checks. See IsNamed() in MQ2Utilities.cpp)
        /// </summary>
        public bool Named => GetMember<BoolType>("Named");

        /// <summary>
        /// Is a buyer? (ie. Buyer in the bazaar)
        /// </summary>
        public bool Buyer => GetMember<BoolType>("Buyer");

        /// <summary>
        /// Moving?
        /// </summary>
        public bool Moving => GetMember<BoolType>("Moving");

        /// <summary>
        /// Current Mana points (only updates when target/group)
        /// </summary>
        public uint? CurrentMana => GetMember<IntType>("CurrentMana");

        /// <summary>
        /// Maximum Mana points (only updates when target/group)
        /// </summary>
        public uint? MaxMana => GetMember<IntType>("MaxMana");

        /// <summary>
        /// Mana as a percentage
        /// </summary>
        public int? PctMana => GetMember<IntType>("PctMana");

        /// <summary>
        /// Current Endurance points (only updates when target/group)
        /// </summary>
        public uint? CurrentEndurance => GetMember<IntType>("CurrentEndurance");

        /// <summary>
        /// Endurance as a percentage
        /// </summary>
        public int? PctEndurance => GetMember<IntType>("PctEndurance");

        /// <summary>
        /// Maximum Endurance points (only updates when target/group)
        /// </summary>
        public uint? MaxEndurance => GetMember<IntType>("MaxEndurance");

        /// <summary>
        /// Loc of the spawn (Y, X)
        /// sprintf_s(DataTypeTemp, "%.2f, %.2f", pSpawn->Y, pSpawn->X);
        /// </summary>
        public string Loc => GetMember<StringType>("Loc");

        /// <summary>
        /// Loc of the spawn (Y, X)
        /// sprintf_s(DataTypeTemp, "%.0f, %.0f", pSpawn->Y, pSpawn->X);
        /// </summary>
        public string LocYX => GetMember<StringType>("LocYX");

        /// <summary>
        /// Loc of the spawn (Y, X, Z)
        /// sprintf_s(DataTypeTemp, "%.2f, %.2f, %.2f", pSpawn->Y, pSpawn->X, pSpawn->Z);
        /// </summary>
        public string LocYXZ => GetMember<StringType>("LocYXZ");

        /// <summary>
        /// The spawn location.
        /// </summary>
        public SpawnLocation? Location
        {
            get
            {
                try
                {
                    var parts = LocYXZ.Split(new string[] { ", " }, 3, StringSplitOptions.RemoveEmptyEntries);

                    SpawnLocation location = new SpawnLocation()
                    {
                        X = float.Parse(parts[1]),
                        Y = float.Parse(parts[0]),
                        Z = float.Parse(parts[2])
                    };

                    return location;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Location using EQ format
        /// sprintf_s(DataTypeTemp, "%.2f, %.2f, %.2f", pSpawn->X, pSpawn->Y, pSpawn->Z);
        /// </summary>
        public string EQLoc => GetMember<StringType>("EQLoc");

        /// <summary>
        /// Location using MQ format (Y, X, Z)
        /// sprintf_s(DataTypeTemp, "%.2f, %.2f, %.2f", pSpawn->Y, pSpawn->X, pSpawn->Z);
        /// </summary>
        public string MQLoc => GetMember<StringType>("MQLoc");

        /// <summary>
        /// Owner, if mercenary
        /// </summary>
        [JsonIgnore]
        public SpawnType Owner => GetMember<SpawnType>("Owner");

        /// <summary>
        /// The spawn a player is following using /follow on - also returns your pet's target via ${Me.Pet.Following}
        /// </summary>
        [JsonIgnore]
        public SpawnType Following => GetMember<SpawnType>("Following");

        /// <summary>
        /// Spawn ID of this spawn's contractor
        /// MQ2 Client note: FIXME: ROF2 emu does not have MercID
        /// </summary>
        public uint? MercID => GetMember<IntType>("MercID");

        /// <summary>
        /// Spawn ID of this spawn's contractor
        /// MQ2 Client note: FIXME: ROF2 emu does not have ContractorID
        /// </summary>
        public uint? ContractorID => GetMember<IntType>("ContractorID");

        /// <summary>
        /// Item ID of anything that may be in the Primary slot
        /// </summary>
        public uint? Primary => GetMember<IntType>("Primary");

        /// <summary>
        /// Item ID of anything that may be in the Secondary slot
        /// </summary>
        public uint? Secondary => GetMember<IntType>("Secondary");

        /// <summary>
        /// ID of the equipment used by the spawn
        /// returns a inttype, it takes numbers 0-8 or names: head chest arms wrists hands legs feet primary offhand
        /// </summary>
        private readonly IndexedMember<IntType, int, IntType, string> _equipment;

        /// <summary>
        /// ID of the equipment used by the spawn for a slot.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public uint? GetEquipmentID(EquipmentSlot slot) => (uint?)_equipment[Enum.GetName(typeof(EquipmentSlot), slot).ToLower()];

        /// <summary>
        /// ID of the equipment used by the spawn for a slot.
        /// </summary>
        /// <param name="slotName"></param>
        /// <returns></returns>
        public uint? GetEquipmentID(string slotName) => (uint?)_equipment[slotName];

        /// <summary>
        /// ID of the equipment used by the spawn for a slot index [0,8].
        /// </summary>
        /// <param name="slotIndex"></param>
        /// <returns></returns>
        public uint? GetEquipmentID(int slotIndex) => (uint?)_equipment[slotIndex];

        /// <summary>
        /// Spawn equipment IDs for all slots.
        /// </summary>
        public IDictionary<EquipmentSlot, uint?> Equipment
        {
            get
            {
                Dictionary<EquipmentSlot, uint?> items = new Dictionary<EquipmentSlot, uint?>();

                for (int i = 0; i < 8; i++)
                {
                    var key = (EquipmentSlot)i;

                    items[key] = GetEquipmentID(key);
                }

                return items;
            }
        }

        /// <summary>
        /// Spawn can be targetted?
        /// </summary>
        public bool Targetable => GetMember<BoolType>("Targetable");

        /// <summary>
        /// TRUE/FALSE on if a splash spell can land...NOTE! This check is ONLY for line of sight to the targetindicator (red/green circle)
        /// </summary>
        public bool CanSplashLand => GetMember<BoolType>("CanSplashLand");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool IsTouchingSwitch => GetMember<BoolType>("IsTouchingSwitch");

        /// <summary>
        /// Seems broken and useless even if it wasn't
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        public bool bShowHelm => GetMember<BoolType>("bShowHelm");
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Returns weird numbers
        /// </summary>
        [Obsolete]
        public uint? CorpseDragCount => GetMember<IntType>("CorpseDragCount");

        /// <summary>
        /// Valid indexes are 0 and 1. TODO: What is SpawnType.CombatSkillTicks
        /// </summary>
        private readonly IndexedMember<IntType, int> _combatSkillTicks;

        /// <summary>
        /// Valid indexes are 0 and 1. TODO: What is SpawnType.CombatSkillTicks
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public uint? GetCombatSkillTicks(bool param) => (uint?)_combatSkillTicks[param ? 1 : 0];

        /// <summary>
        /// Valid indexes are 0 and 1. TODO: What is SpawnType.CombatSkillTicks
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint? GetCombatSkillTicks(int index) => (uint?)_combatSkillTicks[index];

        /// <summary>
        /// In a PvP area?
        /// </summary>
        public bool InPvPArea => (uint?)GetMember<IntType>("InPvPArea") > 0;

        /// <summary>
        /// GM rank
        /// </summary>
        public uint? GMRank => GetMember<IntType>("GMRank");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool TemporaryPet => GetMember<BoolType>("TemporaryPet");

        /// <summary>
        /// Holding animation
        /// </summary>
        public uint? HoldingAnimation => GetMember<IntType>("HoldingAnimation");

        /// <summary>
        /// Blind?
        /// </summary>
        public bool Blind => (uint?)GetMember<IntType>("Blind") > 0;

        /// <summary>
        /// Ceiling height at the spawn's current location
        /// </summary>
        public float? CeilingHeightAtCurrLocation => GetMember<FloatType>("CeilingHeightAtCurrLocation");

        /// <summary>
        /// TODO: SpawnType.AssistName is always blank?
        /// </summary>
        public string AssistName => GetMember<StringType>("AssistName");

        /// <summary>
        /// Spawn can see invis, takes an index of 0 - 2
        /// SeeInvisLevels_All = 0,
        /// SeeInvisLevels_Unead = 1,
	    /// SeeInvisLevels_Animal = 2,
        /// </summary>
        private readonly IndexedMember<IntType, int> _seeInvis;

        /// <summary>
        /// Spawn can see invis.
        /// </summary>
        /// <param name="invisType"></param>
        /// <returns></returns>
        public bool CanSeeInvis(SeeInvisType invisType = SeeInvisType.All) => (uint?)_seeInvis[(int)invisType] > 0;

        /// <summary>
        /// Spawn status, takes an index of 0 - 5. TODO: Confirm what they mean
        /// </summary>
        private readonly IndexedMember<IntType, int> _spawnStatus;

        /// <summary>
        /// Spawn status, takes an index of 0 - 5. TODO: Confirm what they mean
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint? GetSpawnStatus(int index) => (uint?)_spawnStatus[index];

        /// <summary>
        /// ActorDef name for this spawn
        /// </summary>
        public string ActorDef => GetMember<StringType>("ActorDef");

        /// <summary>
        /// This is fucked, not dealing with it.
        /// HAHA ^
        /// </summary>
        private readonly IndexedMember<CachedBuffType, int, CachedBuffType, string> _cachedBuff;

        /// <summary>
        /// Get a cached buff by query.
        /// Query syntax seems to:
        /// Exact spell name match
        /// # by buff slot
        /// * by buff index (looks like 1 based)
        /// ^ by keyword
        /// Keywords seem to be:
        /// slowed, rooted, mezzed, crippled, maloed, tashed, snared, beneficial
        /// 
        /// IE "^rooted"
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public CachedBuffType GetCachedBuff(string query) => _cachedBuff[query];

        /// <summary>
        /// TODO: what is this index? buff index?
        /// Looks to be 0 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CachedBuffType GetCachedBuff(int index) => _cachedBuff[index];

        /// <summary>
        /// Number of cached buffs
        /// </summary>
        public uint? CachedBuffCount => GetMember<IntType>("CachedBuffCount");

        /// <summary>
        /// All cached buffs. Based on <see cref="GetCachedBuff(int)"/>
        /// </summary>
        public IEnumerable<CachedBuffType> CachedBuffs
        {
            get
            {
                var count = CachedBuffCount ?? 0;

                for (int i = 0; i < count; i++)
                {
                    yield return GetCachedBuff(i);
                }
            }
        }

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool BuffsPopulated => GetMember<BoolType>("BuffsPopulated");

        /// <summary>
        /// This looks very similar to <see cref="_cachedBuff"/>. A more basic implementation/sub functionality of it.
        /// </summary>
        private readonly IndexedMember<CachedBuffType, int, CachedBuffType, string> _buff;

        /// <summary>
        /// Get a cached buff by name.
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns></returns>
        public CachedBuffType GetBuff(string spellName) => _buff[spellName];

        /// <summary>
        /// TODO: what is this index? buff index?
        /// Looks like it is 1 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CachedBuffType GetBuff(int index) => _buff[index];

        /// <summary>
        /// So many different cached buff implementations, all seem like variations of the same thing.
        /// </summary>
        private readonly IndexedMember<CachedBuffType> _findBuff;

        /// <summary>
        /// So many different cached buff implementations, all seem like variations of the same thing.
        /// This one supports predicates based on EvaluateCachedBuffPredicate(Index);
        /// Need to figure out the formatting of these.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public CachedBuffType FindBuff(string predicate) => _findBuff[predicate];

        /// <summary>
        /// Another one, this is only your buffs though. IE caster name is your name.
        /// </summary>
        private readonly IndexedMember<CachedBuffType, int, CachedBuffType, string> _myBuff;

        /// <summary>
        /// TODO: new indexed member
        /// Another one, this is only your buffs though. IE caster name is your name.
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns></returns>
        public CachedBuffType GetMyBuff(string spellName) => _myBuff[spellName];

        /// <summary>
        /// TODO: new indexed member
        /// Another one, this is only your buffs though. IE caster name is your name.
        /// Looks like it is 1 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CachedBuffType GetMyBuff(int index) => _myBuff[index];

        /// <summary>
        /// Number of buffs
        /// </summary>
        public uint? BuffCount => GetMember<IntType>("BuffCount");

        /// <summary>
        /// All buffs. Based on <see cref="GetBuff(int)"/>
        /// </summary>
        public IEnumerable<CachedBuffType> Buffs
        {
            get
            {
                var count = BuffCount ?? 0;

                for (int i = 0; i < count; i++)
                {
                    yield return GetBuff(i + 1);
                }
            }
        }

        /// <summary>
        /// Number of my buffs
        /// </summary>
        public uint? MyBuffCount => GetMember<IntType>("MyBuffCount");

        /// <summary>
        /// All my buffs. Based on <see cref="GetMyBuff(int)"/>
        /// </summary>
        public IEnumerable<CachedBuffType> MyBuffs
        {
            get
            {
                var count = MyBuffCount ?? 0;

                for (int i = 0; i < count; i++)
                {
                    yield return GetMyBuff(i + 1);
                }
            }
        }

        /// <summary>
        /// Same as <see cref="_buff"/> but durations.
        /// </summary>
        private readonly IndexedMember<TimeStampType, int, TimeStampType, string> _buffDuration;

        /// <summary>
        /// Get a cached buff duration by name.
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns></returns>
        public TimeSpan? GetBuffDuration(string spellName) => (TimeSpan?)_buffDuration[spellName];

        /// <summary>
        /// TODO: what is this index? buff index?
        /// Looks like it is 1 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TimeSpan? GetBuffDuration(int index) => (TimeSpan?)_buffDuration[index];

        /// <summary>
        /// All buffs and durations. Based on <see cref="GetBuff(int)"/> and <see cref="GetBuffDuration(int)"/>
        /// </summary>
        public IDictionary<CachedBuffType, TimeSpan?> BuffDurations
        {
            get
            {
                var count = BuffCount ?? 0;
                Dictionary<CachedBuffType, TimeSpan?> items = new Dictionary<CachedBuffType, TimeSpan?>((int)count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetBuff(i + 1), GetBuffDuration(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// Same as <see cref="_myBuff"/> but durations.
        /// </summary>
        private readonly IndexedMember<TimeStampType, int, TimeStampType, string> _myBuffDuration;

        /// <summary>
        /// Get a cached buff duration by name.
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns></returns>
        public TimeSpan? GetMyBuffDuration(string spellName) => (TimeSpan?)_myBuffDuration[spellName];

        /// <summary>
        /// TODO: what is this index? buff index?
        /// Looks like it is 1 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TimeSpan? GetMyBuffDuration(int index) => (TimeSpan?)_myBuffDuration[index];

        /// <summary>
        /// All my buffs and durations. Based on <see cref="GetMyBuff(int)"/> and <see cref="GetMyBuffDuration(int)"/>
        /// </summary>
        public IDictionary<CachedBuffType, TimeSpan?> MyBuffDurations
        {
            get
            {
                var count = BuffCount ?? 0;
                Dictionary<CachedBuffType, TimeSpan?> items = new Dictionary<CachedBuffType, TimeSpan?>((int)count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetMyBuff(i + 1), GetMyBuffDuration(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// Guild status (Leader, Officer, Member)
        /// This is defined as a member but there is no implementation in the switch
        /// </summary>
        [Obsolete]
        public string GuildStatus => GetMember<StringType>("GuildStatus");
    }
}