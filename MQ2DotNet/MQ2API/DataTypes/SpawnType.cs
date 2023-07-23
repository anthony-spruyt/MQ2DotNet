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
        public virtual void DoTarget() => GetMember<MQ2DataType>("DoTarget");

        /// <summary>
        /// Faces the spawn (equivalent of /face)
        /// </summary>
        public virtual void DoFace() => GetMember<MQ2DataType>("DoFace");

        /// <summary>
        /// Left click on the spawn
        /// </summary>
        public virtual void LeftClick() => GetMember<MQ2DataType>("LeftClick");

        /// <summary>
        /// Right click on the spawn
        /// </summary>
        public virtual void RightClick() => GetMember<MQ2DataType>("RightClick");

        /// <summary>
        /// Assists the spawn (equivalent of /assist)
        /// </summary>
        public virtual void DoAssist() => GetMember<MQ2DataType>("DoAssist");

        /// <summary>
        /// Level of the spawn
        /// </summary>
        public virtual uint? Level => GetMember<IntType>("Level");

        /// <summary>
        /// Spawn's ID
        /// </summary>
        public virtual uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Internal name of the spawn e.g. a_rat01
        /// </summary>
        public virtual string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Last name
        /// </summary>
        public virtual string Surname => GetMember<StringType>("Surname");

        /// <summary>
        /// The "cleaned up" name
        /// </summary>
        public virtual string CleanName => GetMember<StringType>("CleanName");

        /// <summary>
        /// Name displayed in game (same as EQ's %T)
        /// </summary>
        public virtual string DisplayName => GetMember<StringType>("DisplayName");

        /// <summary>
        /// Shortcut for -X (makes Eastward positive)
        /// </summary>
        public virtual float? E => GetMember<FloatType>("E");

        /// <summary>
        /// X, the Northward-positive coordinate
        /// </summary>
        public virtual float? X => GetMember<FloatType>("X");

        /// <summary>
        /// Same as <see cref="X"/>
        /// </summary>
        public virtual float? W => X;

        /// <summary>
        /// Shortcut for -Y (makes Southward positive)
        /// </summary>
        public virtual float? S => GetMember<FloatType>("S");

        /// <summary>
        /// Y, the Westward-positive coordinate
        /// </summary>
        public virtual float? Y => GetMember<FloatType>("Y");

        /// <summary>
        /// Same as <see cref="Y"/>
        /// </summary>
        public virtual float? N => Y;

        /// <summary>
        /// Shortcut for -Z (makes Downward positive)
        /// </summary>
        public virtual float? D => GetMember<FloatType>("D");

        /// <summary>
        /// Z, the Upward-positive coordinate
        /// </summary>
        public virtual float? Z => GetMember<FloatType>("Z");

        /// <summary>
        /// Same as <see cref="Z"/>
        /// </summary>
        public virtual float? U => Z;

        /// <summary>
        /// Floor z value at the spawn's location
        /// </summary>
        public virtual float? FloorZ => GetMember<FloatType>("FloorZ");

        /// <summary>
        /// Next spawn in the linked list
        /// </summary>
        [JsonIgnore]
        public virtual SpawnType Next => GetMember<SpawnType>("Next");

        /// <summary>
        /// Next spawn in EQ's favourite data structure
        /// </summary>
        [JsonIgnore]
        public virtual SpawnType Prev => GetMember<SpawnType>("Prev");

        /// <summary>
        /// Current hit points
        /// </summary>
        public virtual long? CurrentHPs => GetMember<Int64Type>("CurrentHPs");

        /// <summary>
        /// Maximum hit points
        /// </summary>
        public virtual long? MaxHPs => GetMember<Int64Type>("MaxHPs");

        /// <summary>
        /// HP as a percentage
        /// </summary>
        public virtual long? PctHPs => GetMember<Int64Type>("PctHPs");

        /// <summary>
        /// Dunno wtf this is or why I would care about it
        /// </summary>
        public virtual int? AARank => GetMember<IntType>("AARank");

        /// <summary>
        /// Speed as a percentage of regular run speed
        /// </summary>
        public virtual float? Speed => GetMember<FloatType>("Speed");

        /// <summary>
        /// Direction the spawn is facing
        /// </summary>
        public virtual HeadingType Heading => GetMember<HeadingType>("Heading");

        /// <summary>
        /// Spawn's pet
        /// </summary>

        [JsonIgnore]
        public virtual PetType Pet => GetMember<PetType>("Pet");

        /// <summary>
        /// Master, if it is charmed or a pet
        /// </summary>
        [JsonIgnore]
        public virtual SpawnType Master => GetMember<SpawnType>("Master");

        /// <summary>
        /// Gender
        /// TODO: map to enum
        /// </summary>
        public virtual string Gender => GetMember<StringType>("Gender");

        /// <summary>
        /// Spawn's race
        /// </summary>
        public virtual RaceType Race => GetMember<RaceType>("Race");

        /// <summary>
        /// Class
        /// </summary>
        public virtual ClassType Class => GetMember<ClassType>("Class");

        /// <summary>
        /// Body type
        /// TODO: map to enum?
        /// </summary>
        public virtual BodyType Body => GetMember<BodyType>("Body");

        /// <summary>
        /// GM or Guide?
        /// </summary>
        public virtual bool GM => GetMember<BoolType>("GM");

        /// <summary>
        /// Spawn is levitating?
        /// </summary>
        public virtual bool Levitating => GetMember<BoolType>("Levitating");

        /// <summary>
        /// Sneaking?
        /// </summary>
        public virtual bool Sneaking => GetMember<BoolType>("Sneaking");

        /// <summary>
        /// Invis?
        /// </summary>
        private readonly IndexedMember<BoolType, int, BoolType, string> _invis;

        /// <summary>
        /// Invisibile? (Any type)
        /// </summary>
        public virtual bool Invis => _invis[""];

        /// <summary>
        /// Invisibile based on type?
        /// </summary>
        /// <param name="invisType"></param>
        /// <returns></returns>
        public virtual bool IsInvis(InvisMode invisType) => _invis[(int)invisType];

        /// <summary>
        /// Height
        /// </summary>
        public virtual float? Height => GetMember<FloatType>("Height");

        /// <summary>
        /// The max distance from this spawn for it to hit you
        /// </summary>
        public virtual float? MaxRange => GetMember<FloatType>("MaxRange");

        /// <summary>
        /// The Max distance from this spawn for you to hit it
        /// </summary>
        public virtual float? MaxRangeTo => GetMember<FloatType>("MaxRangeTo");

        /// <summary>
        /// Name of the spawn's guild
        /// </summary>
        public virtual string Guild => GetMember<StringType>("Guild");

        /// <summary>
        /// PC, NPC, Untargetable, Mount, Pet, Corpse, Chest, Trigger, Trap, Timer, Item, Mercenary, Aura, Object, Banner, Campfire, Flyer
        /// TODO: map to an enum
        /// </summary>
        public virtual string Type => GetMember<StringType>("Type");

        /// <summary>
        /// Name of the light class this spawn has
        /// </summary>
        public virtual string Light => GetMember<StringType>("Light");

        /// <summary>
        /// StandState
        /// </summary>
        public virtual int? StandState => GetMember<IntType>("StandState");

        /// <summary>
        /// STAND, SIT, DUCK, BIND, FEIGN, DEAD, STUN, HOVER, MOUNT, UNKNOWN
        /// </summary>
        public virtual SpawnState? State => GetMember<StringType>("State");

        /// <summary>
        /// Standing?
        /// </summary>
        public virtual bool Standing => GetMember<BoolType>("Standing");

        /// <summary>
        /// Sitting?
        /// </summary>
        public virtual bool Sitting => GetMember<BoolType>("Sitting");

        /// <summary>
        /// Time this spawn has been dead for
        /// </summary>
        public virtual TimeSpan? TimeBeenDead => GetMember<TimeStampType>("TimeBeenDead");

        /// <summary>
        /// If it's a summoned being (pet for example). Unsure if useful for druid nukes.
        /// </summary>
        public virtual bool IsSummoned => GetMember<BoolType>("IsSummoned");

        /// <summary>
        /// Target of this spawn's target
        /// </summary>
        [JsonIgnore]
        public virtual SpawnType TargetOfTarget => GetMember<SpawnType>("TargetOfTarget");

        /// <summary>
        /// Ducking?
        /// </summary>
        public virtual bool Ducking => GetMember<BoolType>("Ducking");

        /// <summary>
        /// Feigning?
        /// </summary>
        public virtual bool Feigning => GetMember<BoolType>("Feigning");

        /// <summary>
        /// Binding wounds?
        /// </summary>
        public virtual bool Binding => GetMember<BoolType>("Binding");

        /// <summary>
        /// Dead?
        /// </summary>
        public virtual bool Dead => GetMember<BoolType>("Dead");

        /// <summary>
        /// Stunned?
        /// </summary>
        public virtual bool Stunned => GetMember<BoolType>("Stunned");

        /// <summary>
        /// returns TRUE or FALSE if a mob is aggressive or not
        /// </summary>
        public virtual bool Aggressive => GetMember<BoolType>("Aggressive");

        /// <summary>
        /// Hovering?
        /// </summary>
        public virtual bool Hovering => GetMember<BoolType>("Hovering");

        /// <summary>
        /// Deity
        /// </summary>
        public virtual DeityType Deity => GetMember<DeityType>("Deity");

        /// <summary>
        /// 2D distance to the spawn in the XY plane
        /// </summary>
        public virtual float? Distance => GetMember<FloatType>("Distance");

        /// <summary>
        /// 3D distance to the spawn in the XYZ plane
        /// </summary>
        public virtual float? Distance3D => GetMember<FloatType>("Distance3D");

        /// <summary>
        /// 2D distance to the spawn in the XY plane, taking into account the spawn's movement but not the player's
        /// </summary>
        public virtual float? DistancePredict => GetMember<FloatType>("DistancePredict");

        /// <summary>
        /// 1D distance to the spawn in the X plane
        /// </summary>
        public virtual float? DistanceX => GetMember<FloatType>("DistanceX");

        /// <summary>
        /// See <see cref="DistanceX"/>
        /// </summary>
        public virtual float? DistanceW => DistanceX;

        /// <summary>
        /// 1D distance to the spawn in the Y plane
        /// </summary>
        public virtual float? DistanceY => GetMember<FloatType>("DistanceY");

        /// <summary>
        /// See <see cref="DistanceY"/>
        /// </summary>
        public virtual float? DistanceN => DistanceY;

        /// <summary>
        /// 1D distance to the spawn in the Z plane
        /// </summary>
        public virtual float? DistanceZ => GetMember<FloatType>("DistanceZ");

        /// <summary>
        /// See <see cref="DistanceZ"/>
        /// </summary>
        public virtual float? DistanceU => DistanceZ;

        /// <summary>
        /// Heading player must travel in to reach this spawn
        /// </summary>
        public virtual HeadingType HeadingTo => GetMember<HeadingType>("HeadingTo");

        /// <summary>
        /// Spell, if currently casting (only accurate on yourself, not NPCs or other group members)
        /// </summary>
        public virtual SpellType Casting => GetMember<SpellType>("Casting");

        /// <summary>
        /// This spawn's mount 
        /// </summary>
        [JsonIgnore]
        public virtual SpawnType Mount => GetMember<SpawnType>("Mount");

        /// <summary>
        /// Underwater?
        /// </summary>
        public virtual bool Underwater => GetMember<BoolType>("Underwater");

        /// <summary>
        /// Feet wet/swimming?
        /// </summary>
        public virtual bool FeetWet => GetMember<BoolType>("FeetWet");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public virtual bool BodyWet => GetMember<BoolType>("BodyWet");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public virtual bool HeadWet => GetMember<BoolType>("HeadWet");

        /// <summary>
        /// returns a mask as an inttype which has the following meaning:
        /// 0=Idle 1=Open 2=WeaponSheathed 4=Aggressive 8=ForcedAggressive 0x10=InstrumentEquipped 0x20=Stunned 0x40=PrimaryWeaponEquipped 0x80=SecondaryWeaponEquipped
        /// TODO: flags enum
        /// </summary>
        public virtual uint? PlayerState => GetMember<IntType>("PlayerState");

        /// <summary>
        /// Stuck?
        /// </summary>
        public virtual bool Stuck => GetMember<BoolType>("Stuck");

        /// <summary>
        /// Current animation ID, see https://www.macroquest2.com/wiki/index.php/Animations
        /// </summary>
        public virtual uint? Animation => GetMember<IntType>("Animation");

        /// <summary>
        /// Represents if the pc/npc is holding anything?
        /// if (pSpawn->LeftHolding || pSpawn->RightHolding)
        /// </summary>
        public virtual bool Holding => GetMember<BoolType>("Holding");

        /// <summary>
        /// Looking this angle
        /// </summary>
        public virtual float? Look => GetMember<FloatType>("Look");

        /// <summary>
        /// GREY, GREEN, LIGHT BLUE, BLUE, WHITE, YELLOW, RED
        /// </summary>
        public virtual ConColor? ConColor => GetMember<StringType>("ConColor");

        /// <summary>
        /// Nth closest spawn to this spawn, or the nth closest matching a search string e.g. "2,npc" for the second closest NPC
        /// </summary>
        private readonly IndexedMember<SpawnType, int, SpawnType, string> _nearestSpawn;

        /// <summary>
        /// Get the nth closest matching a search string e.g. "2,npc" for the second closest NPC
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual SpawnType GetClosestSpawn(string query) => _nearestSpawn[query];

        /// <summary>
        /// Get the nth closest NPC.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual SpawnType GetClosestNPC(int nth = 1) => _nearestSpawn[$"{nth},npc"];

        /// <summary>
        /// Get the nth closest spawn to this spawn.
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public virtual SpawnType GetClosestSpawn(int nth = 1) => _nearestSpawn[nth];

        /// <summary>
        /// Trader (in bazaar)?
        /// </summary>
        public virtual bool Trader => GetMember<BoolType>("Trader");

        /// <summary>
        /// AFK flag set?
        /// </summary>
        public virtual bool AFK => GetMember<BoolType>("AFK");

        /// <summary>
        /// LFG flag set?
        /// </summary>
        public virtual bool LFG => GetMember<BoolType>("LFG");

        /// <summary>
        /// Linkdead?
        /// </summary>
        public virtual bool Linkdead => GetMember<BoolType>("Linkdead");

        /// <summary>
        /// Prefix/Title before name
        /// </summary>
        public virtual string Title => GetMember<StringType>("Title");

        /// <summary>
        /// Leaving this in for older macros/etc..<see cref="Title"/> should be used instead.
        /// </summary>
        [Obsolete]
        public virtual string AATitle => Title;

        /// <summary>
        /// Suffix attached to name, eg. of servername
        /// </summary>
        public virtual string Suffix => GetMember<StringType>("Suffix");

        /// <summary>
        /// Group leader?
        /// </summary>
        public virtual bool GroupLeader => GetMember<BoolType>("GroupLeader");

        /// <summary>
        /// Current Raid or Group assist target?
        /// </summary>
        public virtual bool Assist => GetMember<BoolType>("Assist");

        /// <summary>
        /// Current Raid or Group marked npc mark number (raid first)
        /// </summary>
        public virtual int? Mark => GetMember<IntType>("Mark");

        /// <summary>
        /// Anon flag set
        /// </summary>
        public virtual bool Anonymous => GetMember<BoolType>("Anonymous");

        /// <summary>
        /// Roleplaying flag set?
        /// </summary>
        public virtual bool Roleplaying => GetMember<BoolType>("Roleplaying");

        /// <summary>
        /// Returns TRUE if spawn is in LoS
        /// </summary>
        public virtual bool LineOfSight => GetMember<BoolType>("LineOfSight");

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
        public virtual HeadingType GetHeading(int x, int y) => _headingToLoc[$"{y},{x}"];

        /// <summary>
        /// Is your target moving away from you?
        /// </summary>
        public virtual bool Fleeing => GetMember<BoolType>("Fleeing");

        /// <summary>
        /// Is this a "named" spawn (ie. does it's name not start with an "a" or an "an", plus a bunch of other checks. See IsNamed() in MQ2Utilities.cpp)
        /// </summary>
        public virtual bool Named => GetMember<BoolType>("Named");

        /// <summary>
        /// Is a buyer? (ie. Buyer in the bazaar)
        /// </summary>
        public virtual bool Buyer => GetMember<BoolType>("Buyer");

        /// <summary>
        /// Moving?
        /// </summary>
        public virtual bool Moving => GetMember<BoolType>("Moving");

        /// <summary>
        /// Current Mana points (only updates when target/group)
        /// </summary>
        public virtual uint? CurrentMana => GetMember<IntType>("CurrentMana");

        /// <summary>
        /// Maximum Mana points (only updates when target/group)
        /// </summary>
        public virtual uint? MaxMana => GetMember<IntType>("MaxMana");

        /// <summary>
        /// Mana as a percentage
        /// </summary>
        public virtual int? PctMana => GetMember<IntType>("PctMana");

        /// <summary>
        /// Current Endurance points (only updates when target/group)
        /// </summary>
        public virtual uint? CurrentEndurance => GetMember<IntType>("CurrentEndurance");

        /// <summary>
        /// Endurance as a percentage
        /// </summary>
        public virtual int? PctEndurance => GetMember<IntType>("PctEndurance");

        /// <summary>
        /// Maximum Endurance points (only updates when target/group)
        /// </summary>
        public virtual uint? MaxEndurance => GetMember<IntType>("MaxEndurance");

        /// <summary>
        /// Loc of the spawn (Y, X)
        /// sprintf_s(DataTypeTemp, "%.2f, %.2f", pSpawn->Y, pSpawn->X);
        /// </summary>
        public virtual string Loc => GetMember<StringType>("Loc");

        /// <summary>
        /// Loc of the spawn (Y, X)
        /// sprintf_s(DataTypeTemp, "%.0f, %.0f", pSpawn->Y, pSpawn->X);
        /// </summary>
        public virtual string LocYX => GetMember<StringType>("LocYX");

        /// <summary>
        /// Loc of the spawn (Y, X, Z)
        /// sprintf_s(DataTypeTemp, "%.2f, %.2f, %.2f", pSpawn->Y, pSpawn->X, pSpawn->Z);
        /// </summary>
        public virtual string LocYXZ => GetMember<StringType>("LocYXZ");

        /// <summary>
        /// The spawn location.
        /// </summary>
        public virtual SpawnLocation? Location
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
        public virtual string EQLoc => GetMember<StringType>("EQLoc");

        /// <summary>
        /// Location using MQ format (Y, X, Z)
        /// sprintf_s(DataTypeTemp, "%.2f, %.2f, %.2f", pSpawn->Y, pSpawn->X, pSpawn->Z);
        /// </summary>
        public virtual string MQLoc => GetMember<StringType>("MQLoc");

        /// <summary>
        /// Owner, if mercenary
        /// </summary>
        [JsonIgnore]
        public virtual SpawnType Owner => GetMember<SpawnType>("Owner");

        /// <summary>
        /// The spawn a player is following using /follow on - also returns your pet's target via ${Me.Pet.Following}
        /// </summary>
        [JsonIgnore]
        public virtual SpawnType Following => GetMember<SpawnType>("Following");

        /// <summary>
        /// Spawn ID of this spawn's contractor
        /// MQ2 Client note: FIXME: ROF2 emu does not have MercID
        /// </summary>
        public virtual uint? MercID => GetMember<IntType>("MercID");

        /// <summary>
        /// Spawn ID of this spawn's contractor
        /// MQ2 Client note: FIXME: ROF2 emu does not have ContractorID
        /// </summary>
        public virtual uint? ContractorID => GetMember<IntType>("ContractorID");

        /// <summary>
        /// Item ID of anything that may be in the Primary slot
        /// </summary>
        public virtual uint? Primary => GetMember<IntType>("Primary");

        /// <summary>
        /// Item ID of anything that may be in the Secondary slot
        /// </summary>
        public virtual uint? Secondary => GetMember<IntType>("Secondary");

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
        public virtual uint? GetEquipmentID(EquipmentSlot slot) => (uint?)_equipment[Enum.GetName(typeof(EquipmentSlot), slot).ToLower()];

        /// <summary>
        /// ID of the equipment used by the spawn for a slot.
        /// </summary>
        /// <param name="slotName"></param>
        /// <returns></returns>
        public virtual uint? GetEquipmentID(string slotName) => (uint?)_equipment[slotName];

        /// <summary>
        /// ID of the equipment used by the spawn for a slot index [0,8].
        /// </summary>
        /// <param name="slotIndex"></param>
        /// <returns></returns>
        public virtual uint? GetEquipmentID(int slotIndex) => (uint?)_equipment[slotIndex];

        /// <summary>
        /// Spawn equipment IDs for all slots.
        /// </summary>
        public virtual IDictionary<EquipmentSlot, uint?> Equipment
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
        public virtual bool Targetable => GetMember<BoolType>("Targetable");

        /// <summary>
        /// TRUE/FALSE on if a splash spell can land...NOTE! This check is ONLY for line of sight to the targetindicator (red/green circle)
        /// </summary>
        public virtual bool CanSplashLand => GetMember<BoolType>("CanSplashLand");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public virtual bool IsTouchingSwitch => GetMember<BoolType>("IsTouchingSwitch");

        /// <summary>
        /// Seems broken and useless even if it wasn't
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        public virtual bool bShowHelm => GetMember<BoolType>("bShowHelm");
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Returns weird numbers
        /// </summary>
        [Obsolete]
        public virtual uint? CorpseDragCount => GetMember<IntType>("CorpseDragCount");

        /// <summary>
        /// Valid indexes are 0 and 1. TODO: What is SpawnType.CombatSkillTicks
        /// </summary>
        private readonly IndexedMember<IntType, int> _combatSkillTicks;

        /// <summary>
        /// Valid indexes are 0 and 1. TODO: What is SpawnType.CombatSkillTicks
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual uint? GetCombatSkillTicks(bool param) => (uint?)_combatSkillTicks[param ? 1 : 0];

        /// <summary>
        /// Valid indexes are 0 and 1. TODO: What is SpawnType.CombatSkillTicks
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual uint? GetCombatSkillTicks(int index) => (uint?)_combatSkillTicks[index];

        /// <summary>
        /// In a PvP area?
        /// </summary>
        public virtual bool InPvPArea => (uint?)GetMember<IntType>("InPvPArea") > 0;

        /// <summary>
        /// GM rank
        /// </summary>
        public virtual uint? GMRank => GetMember<IntType>("GMRank");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public virtual bool TemporaryPet => GetMember<BoolType>("TemporaryPet");

        /// <summary>
        /// Holding animation
        /// </summary>
        public virtual uint? HoldingAnimation => GetMember<IntType>("HoldingAnimation");

        /// <summary>
        /// Blind?
        /// </summary>
        public virtual bool Blind => (uint?)GetMember<IntType>("Blind") > 0;

        /// <summary>
        /// Ceiling height at the spawn's current location
        /// </summary>
        public virtual float? CeilingHeightAtCurrLocation => GetMember<FloatType>("CeilingHeightAtCurrLocation");

        /// <summary>
        /// TODO: SpawnType.AssistName is always blank?
        /// </summary>
        public virtual string AssistName => GetMember<StringType>("AssistName");

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
        public virtual bool CanSeeInvis(SeeInvisType invisType = SeeInvisType.All) => (uint?)_seeInvis[(int)invisType] > 0;

        /// <summary>
        /// Spawn status, takes an index of 0 - 5. TODO: Confirm what they mean
        /// </summary>
        private readonly IndexedMember<IntType, int> _spawnStatus;

        /// <summary>
        /// Spawn status, takes an index of 0 - 5. TODO: Confirm what they mean
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual uint? GetSpawnStatus(int index) => (uint?)_spawnStatus[index];

        /// <summary>
        /// ActorDef name for this spawn
        /// </summary>
        public virtual string ActorDef => GetMember<StringType>("ActorDef");

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
        public virtual CachedBuffType GetCachedBuff(string query) => _cachedBuff[query];

        /// <summary>
        /// TODO: what is this index? buff index?
        /// Looks to be 0 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual CachedBuffType GetCachedBuff(int index) => _cachedBuff[index];

        /// <summary>
        /// Number of cached buffs
        /// </summary>
        public virtual uint? CachedBuffCount => GetMember<IntType>("CachedBuffCount");

        /// <summary>
        /// All cached buffs. Based on <see cref="GetCachedBuff(int)"/>
        /// </summary>
        public virtual IEnumerable<CachedBuffType> CachedBuffs
        {
            get
            {
                var count = CachedBuffCount ?? 0;

                for (int i = 0; i < count; i++)
                {
                    var buff = GetCachedBuff(i + 1);

                    if (buff == null)
                    {
                        continue;
                    }

                    yield return buff;
                }
            }
        }

        /// <summary>
        /// TODO: new member
        /// </summary>
        public virtual bool BuffsPopulated => GetMember<BoolType>("BuffsPopulated");

        /// <summary>
        /// This looks very similar to <see cref="_cachedBuff"/>. A more basic implementation/sub functionality of it.
        /// </summary>
        private readonly IndexedMember<CachedBuffType, int, CachedBuffType, string> _buff;

        /// <summary>
        /// Get a cached buff by name.
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns></returns>
        public virtual CachedBuffType GetBuff(string spellName) => _buff[spellName];

        /// <summary>
        /// TODO: what is this index? buff index?
        /// Looks like it is 1 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual CachedBuffType GetBuff(int index) => _buff[index];

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
        public virtual CachedBuffType FindBuff(string predicate) => _findBuff[predicate];

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
        public virtual CachedBuffType GetMyBuff(string spellName) => _myBuff[spellName];

        /// <summary>
        /// TODO: new indexed member
        /// Another one, this is only your buffs though. IE caster name is your name.
        /// Looks like it is 1 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual CachedBuffType GetMyBuff(int index) => _myBuff[index];

        /// <summary>
        /// Number of buffs
        /// </summary>
        public virtual uint? BuffCount => GetMember<IntType>("BuffCount");

        /// <summary>
        /// All buffs. Based on <see cref="GetBuff(int)"/>
        /// </summary>
        public virtual IEnumerable<CachedBuffType> Buffs
        {
            get
            {
                var count = BuffCount ?? 0;

                for (int i = 0; i < count; i++)
                {
                    var buff = GetBuff(i + 1);

                    if (buff == null)
                    {
                        continue;
                    }

                    yield return buff;
                }
            }
        }

        /// <summary>
        /// Number of my buffs
        /// </summary>
        public virtual uint? MyBuffCount => GetMember<IntType>("MyBuffCount");

        /// <summary>
        /// All my buffs. Based on <see cref="GetMyBuff(int)"/>
        /// </summary>
        public virtual IEnumerable<CachedBuffType> MyBuffs
        {
            get
            {
                var count = MyBuffCount ?? 0;

                for (int i = 0; i < count; i++)
                {
                    var buff = GetMyBuff(i + 1);

                    if (buff == null)
                    {
                        continue;
                    }

                    yield return buff;
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
        public virtual TimeSpan? GetBuffDuration(string spellName) => (TimeSpan?)_buffDuration[spellName];

        /// <summary>
        /// TODO: what is this index? buff index?
        /// Looks like it is 1 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual TimeSpan? GetBuffDuration(int index) => (TimeSpan?)_buffDuration[index];

        /// <summary>
        /// All buffs and durations. Based on <see cref="GetBuff(int)"/> and <see cref="GetBuffDuration(int)"/>
        /// </summary>
        public virtual IDictionary<CachedBuffType, TimeSpan?> BuffDurations
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
        public virtual TimeSpan? GetMyBuffDuration(string spellName) => (TimeSpan?)_myBuffDuration[spellName];

        /// <summary>
        /// TODO: what is this index? buff index?
        /// Looks like it is 1 based index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual TimeSpan? GetMyBuffDuration(int index) => (TimeSpan?)_myBuffDuration[index];

        /// <summary>
        /// All my buffs and durations. Based on <see cref="GetMyBuff(int)"/> and <see cref="GetMyBuffDuration(int)"/>
        /// </summary>
        public virtual IDictionary<CachedBuffType, TimeSpan?> MyBuffDurations
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
        public virtual string GuildStatus => GetMember<StringType>("GuildStatus");
    }
}