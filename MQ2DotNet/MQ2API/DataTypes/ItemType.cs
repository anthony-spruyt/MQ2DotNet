using JetBrains.Annotations;
using MQ2DotNet.EQ;
using System;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains the properties that describe an item.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-item/
    /// </summary>
    [PublicAPI]
    [MQ2Type("item")]
    public class ItemType : MQ2DataType
    {
        internal ItemType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _item = new IndexedMember<ItemType, int>(this, "Item");
            _wornSlot = new IndexedMember<BoolType, string, InvSlotType, int>(this, "WornSlot");
            _class = new IndexedMember<ClassType, int, ClassType, string>(this, "Class");
            _race = new IndexedMember<RaceType, int, RaceType, string>(this, "Race");
            _deity = new IndexedMember<DeityType, int, DeityType, string>(this, "Deity");
            _augSlot = new IndexedMember<AugType, int>(this, "AugSlot");
        }

        /// <summary>
        /// TODO: not in official doco
        /// </summary>
        public uint? RefCount => GetMember<IntType>("RefCount");

        /// <summary>
        /// Item ID
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Name
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Lore?
        /// </summary>
        public bool Lore => GetMember<BoolType>("Lore");

        /// <summary>
        /// Item is lore when equipped? (no doco available)
        /// </summary>
        public bool LoreEquipped => GetMember<BoolType>("LoreEquipped");

        /// <summary>
        /// No Trade?
        /// Synonym for <see cref="NoTrade"/>
        /// </summary>
        public bool NoDrop => NoTrade;

        /// <summary>
        /// No Trade?
        /// Synonym for <see cref="NoDrop"/>
        /// </summary>
        public bool NoTrade => GetMember<BoolType>("NoTrade");

        /// <summary>
        /// Temporary?
        /// </summary>
        public bool NoRent => GetMember<BoolType>("NoRent");

        /// <summary>
        /// Magic?
        /// </summary>
        public bool Magic => GetMember<BoolType>("Magic");

        /// <summary>
        /// Item value in coppers
        /// </summary>
        public uint? Value => GetMember<IntType>("Value");

        /// <summary>
        /// Item size: 1 SMALL 2 MEDIUM 3 LARGE 4 GIANT
        /// </summary>
        public ItemSize? Size => GetMember<IntType>("Size");

        /// <summary>
        /// If item is a container, size of items it can hold: 1 SMALL 2 MEDIUM 3 LARGE 4 GIANT
        /// </summary>
        public ItemSize? SizeCapacity => GetMember<IntType>("SizeCapacity");

        /// <summary>
        /// Item weight
        /// </summary>
        public uint? Weight => GetMember<IntType>("Weight");

        /// <summary>
        /// Number of items in the stack
        /// </summary>
        public uint? Stack => GetMember<IntType>("Stack");

        /// <summary>
        /// Type of the item e.g. Armor, 2H Slashing (corresponds to type in bazaar search)
        /// </summary>
        public string Type => GetMember<StringType>("Type");

        /// <summary>
        /// Charges on the item TODO: Total or remaining charges?
        /// </summary>
        public uint? Charges => GetMember<IntType>("Charges");

        /// <summary>
        /// "All", "Deepest Guk", "Miragul's", "Mistmoore", "Rujarkian", "Takish", "Unknown"
        /// </summary>
        public LDoNTheme? LDoNTheme => GetMember<StringType>("LDoNTheme");

        /// <summary>
        /// "None", "Magic", "Fire", "Cold", "Poison", "Disease"
        /// </summary>
        public DMGBonusType? DMGBonusType => GetMember<StringType>("DMGBonusType");

        /// <summary>
        /// Number of slots, if this is a container
        /// </summary>
        public uint? Container => GetMember<IntType>("Container");

        /// <summary>
        /// Item is a container and is open
        /// </summary>
        public uint? Open => GetMember<IntType>("Open");

        /// <summary>
        /// Number of items, if this is a container.
        /// </summary>
        public uint? Items => GetMember<IntType>("Items");

        /// <summary>
        /// Activatable spell effect, if any.
        /// </summary>
        public ItemSpellType Clicky => GetMember<ItemSpellType>("Clicky");

        /// <summary>
        /// Combat proc effect.
        /// </summary>
        public ItemSpellType Proc => GetMember<ItemSpellType>("Proc");

        /// <summary>
        /// Passive worn effect, if any.
        /// </summary>
        public ItemSpellType Worn => GetMember<ItemSpellType>("Worn");

        /// <summary>
        /// First focus effect, if any.
        /// </summary>
        public ItemSpellType Focus => GetMember<ItemSpellType>("Focus");

        /// <summary>
        /// Scroll effect, if any.
        /// </summary>
        public ItemSpellType Scroll => GetMember<ItemSpellType>("Scroll");

        /// <summary>
        /// Second focus effect, if any.
        /// </summary>
        public ItemSpellType Focus2 => GetMember<ItemSpellType>("Focus2");

        /// <summary>
        /// Mount spell effect, if any.
        /// </summary>
        public ItemSpellType Mount => GetMember<ItemSpellType>("Mount");

        /// <summary>
        /// Illusion spell effect, if any.
        /// </summary>
        public ItemSpellType Illusion => GetMember<ItemSpellType>("Illusion");

        /// <summary>
        /// Familiar spell effect, if any.
        /// </summary>
        public ItemSpellType Familiar => GetMember<ItemSpellType>("Familiar");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public ItemSpellType Blessing => GetMember<ItemSpellType>("Blessing");

        /// <summary>
        /// If the item is a container, the item in the nth slot (1 based)
        /// Item[N]
        /// </summary>
        private IndexedMember<ItemType, int> _item;

        /// <summary>
        /// If the item is a container, the item in the nth slot (1 based)
        /// Item[N]
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public ItemType GetItem(int nth) => _item[nth];

        /// <summary>
        /// The items in a container if this item is a container.
        /// </summary>
        public IEnumerable<ItemType> Contents
        {
            get
            {
                var count = (int?)Items ?? 0;
                List<ItemType> items = new List<ItemType>(count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetItem(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// Stackable?
        /// </summary>
        public bool Stackable => GetMember<BoolType>("Stackable");

        /// <summary>
        /// Inventory Slot Number (Historic and now deprecated, use ItemSlot and ItemSlot2)
        /// </summary>
        [Obsolete]
        public InvSlotType InvSlot => GetMember<InvSlotType>("InvSlot");

        /// <summary>
        /// Item Slot number see https://docs.macroquest.org/reference/general/slot-names/
        /// </summary>
        public int? ItemSlot => GetMember<IntType>("ItemSlot");

        /// <summary>
        /// Item Slot subnumber see https://docs.macroquest.org/reference/general/slot-names/
        /// If the item is in a container, the index (0 based) of the slot within the container
        /// </summary>
        public int? ItemSlot2 => GetMember<IntType>("ItemSlot2");

        /// <summary>
        /// The cost to buy this item from active merchant
        /// </summary>
        public long? BuyPrice => GetMember<Int64Type>("BuyPrice");

        /// <summary>
        /// Price to sell this item at this merchant if one is open
        /// </summary>
        public uint? SellPrice => GetMember<IntType>("SellPrice");

        /// <summary>
        /// The Nth invslot this item can be worn in (fingers/ears count as 2 slots)
        /// WornSlot[N]
        /// Can item be worn in invslot with this name? (worn slots only)
        /// WornSlot[name]
        /// </summary>
        private IndexedMember<BoolType, string, InvSlotType, int> _wornSlot;

        /// <summary>
        /// Can item be worn in invslot with this name? (worn slots only)
        /// WornSlot[name]
        /// </summary>
        /// <param name="slotName"></param>
        /// <returns></returns>
        public bool CanWearInSlot(string slotName) => _wornSlot[slotName];

        /// <summary>
        /// The Nth invslot this item can be worn in (fingers/ears count as 2 slots)
        /// WornSlot[N]
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public InvSlotType GetInvSlot(int nth) => _wornSlot[nth];

        /// <summary>
        /// The inv slots this item can be worn, if worn item.
        /// </summary>
        public IEnumerable<InvSlotType> InvSlotRestrictions
        {
            get
            {
                var count = (int?)WornSlots ?? 0;
                List<InvSlotType> items = new List<InvSlotType>(count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetInvSlot(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// Number of slots this item can be worn in (fingers/ears count as 2)
        /// </summary>
        public uint? WornSlots => GetMember<IntType>("WornSlots");

        /// <summary>
        /// Cast time on clicky ItemSpell
        /// The C++ source shows Dest.UInt64 and pTimeStampType which everywhere else is milliseconds, but the online doco say it is seconds. Testing confirms it is milliseconds and the doco is incorrect. 
        /// </summary>
        public TimeSpan? CastTime => GetMember<TimeStampType>("CastTime");

        /// <summary>
        /// Spell effect
        /// </summary>
        public SpellType Spell => GetMember<SpellType>("Spell");

        /// <summary>
        /// Spell effect type (see below for spell effect types)
        /// Click Inventory - item has a right-click spell and can be cast from inventory
        /// Click Unknown - item has an unknown right-click effect restriction
        /// Click Worn - item has a right-click spell and must be equipped to click it
        /// Combat - weapon has a proc
        /// Spell Scroll - Scribeable spell scroll
        /// Worn - item has a focus effect
        /// </summary>
        public EffectType? EffectType => GetMember<StringType>("EffectType");

        /// <summary>
        /// Instrument Modifier Value
        /// </summary>
        public float? InstrumentMod => GetMember<FloatType>("InstrumentMod");

        /// <summary>
        /// Tribute value of item
        /// </summary>
        public uint? Tribute => GetMember<IntType>("Tribute");

        /// <summary>
        /// Attuneable?
        /// </summary>
        public bool Attuneable => GetMember<BoolType>("Attuneable");

        /// <summary>
        /// Returns the number of ticks remaining on an item recast timer
        /// </summary>
        public TimeSpan? Timer => GetMember<TicksType>("Timer");

        /// <summary>
        /// Damage value on the weapon (no info in doco)
        /// </summary>
        public uint? Damage => GetMember<IntType>("Damage");

        /// <summary>
        /// Weapon delay
        /// </summary>
        public uint? ItemDelay => GetMember<IntType>("ItemDelay");

        /// <summary>
        /// Weapon delay
        /// </summary>
        public TimeSpan? ItemDelay2
        {
            get
            {
                if (ItemDelay.HasValue)
                {
                    return TimeSpan.FromSeconds(ItemDelay.Value / 10f);
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the number of seconds remaining on an item recast timer
        /// </summary>
        public TimeSpan? TimerReady
        {
            get
            {
                var seconds = (uint?)GetMember<IntType>("TimerReady");

                if (seconds.HasValue)
                {
                    return TimeSpan.FromSeconds(seconds.Value);
                }

                return null;
            }
        }

        /// <summary>
        /// Maximum number if items that can be in the stack
        /// </summary>
        public uint? StackSize => GetMember<IntType>("StackSize");

        /// <summary>
        /// Number of stacks of the item in your inventory
        /// </summary>
        public uint? Stacks => GetMember<IntType>("Stacks");

        /// <summary>
        /// The total number of the stackable item in your inventory
        /// </summary>
        public uint? StackCount => GetMember<IntType>("StackCount");

        /// <summary>
        /// The number of items needed to fill all the stacks of the item you have (with a stacksize of 20).
        /// If you have 3 stacks (1, 10, 20 in those stacks), you have room for 60 total and you have 31 on you,
        /// so FreeStack would return 29.
        /// </summary>
        public uint? FreeStack => GetMember<IntType>("FreeStack");

        /// <summary>
        /// Quantity of item active merchant has
        /// </summary>
        public uint? MerchQuantity => GetMember<IntType>("MerchQuantity");

        /// <summary>
        /// The number of classes that can use the item. Items suitable for ALL classes will return 16.
        /// </summary>
        public uint? Classes => GetMember<IntType>("Classes");

        /// <summary>
        /// Returns the Nth long class name of the listed classes on an item. Items suitable for ALL classes will effectively have all 16 classes listed.
        /// Class that can use the item, by number (1 - Classes), or by class name or 3 letter code
        /// TODO: see if we can use <see cref="Class"/> here to improve usage.
        /// </summary>
        private IndexedMember<ClassType, int, ClassType, string> _class;

        /// <summary>
        /// TODO: doco description and source doesnt look like it matches.
        /// </summary>
        /// <param name="nth">The base 1 index.</param>
        /// <returns></returns>
        public ClassType GetClass(int nth) => _class[nth];

        /// <summary>
        /// TODO: doco description and source doesnt look like it matches.
        /// </summary>
        /// <param name="name">The class name or 3 letter code.</param>
        /// <returns></returns>
        public ClassType GetClass(string name) => _class[name];

        /// <summary>
        /// This convenience method does n [1,<see cref="Classes"/>]
        /// </summary>
        public IEnumerable<ClassType> ClassRestrictions
        {
            get
            {
                int count = (int?)Classes ?? 0;
                List<ClassType> items = new List<ClassType>(count);

                for (int i = 0; i < count; i++)
                {
                    var item = GetClass(i + 1);

                    if (item != null)
                    {
                        items.Add(item);
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// The number of races that can use the item. Items suitable for ALL races will return 15.
        /// </summary>
        public uint? Races => GetMember<IntType>("Races");

        /// <summary>
        /// Race that can use the item, by number (1 - Races), or by name (full name e.g. Froglok, not FRG)
        /// Returns the Nth long race name of the listed races on an item. Items suitable for ALL races will effectively have all 15 races listed.
        /// Race[N]
        /// </summary>
        private IndexedMember<RaceType, int, RaceType, string> _race;

        /// <summary>
        /// Returns the Nth long race name of the listed races on an item. Items suitable for ALL races will effectively have all 15 races listed.
        /// Race[N]
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public RaceType GetRace(int nth) => _race[nth];

        public RaceType GetRace(string name) => _race[name];

        /// <summary>
        /// This convenience method does n [1,15]
        /// </summary>
        public IEnumerable<RaceType> RaceRestrictions
        {
            get
            {
                List<RaceType> items = new List<RaceType>(RaceType.NUM_OF_RACES);

                for (int i = 0; i < RaceType.NUM_OF_RACES; i++)
                {
                    var item = GetRace(i + 1);

                    if (item != null)
                    {
                        items.Add(item);
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// The number of deities that can use the item. Items with no deity restrictions will return 0.
        /// </summary>
        public uint? Deities => GetMember<IntType>("Deities");

        /// <summary>
        /// Returns the Nth deity of the listed deities on an item. Items with no deity restrictions will return NULL for all values of N.
        /// Deity[N]
        /// Deity that can use the item, by number (1 - Deities), or by name. Returns null if there are no restrictions
        /// TODO: look at adding in an enum here.
        /// </summary>
        private IndexedMember<DeityType, int, DeityType, string> _deity;

        /// <summary>
        /// Returns the Nth deity of the listed deities on an item. Items with no deity restrictions will return NULL for all values of N.
        /// Deity[N]
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public DeityType GetDeity(int nth) => _deity[nth];

        /// <summary>
        /// Returns the deity of the listed deities on an item. Items with no deity restrictions will return NULL for all name values.
        /// Deity[name]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DeityType GetDeity(string name) => _deity[name];

        /// <summary>
        /// If no deity is returned then there are no restrictions.
        /// This convenience method does n [1,16]
        /// TODO: Test if <see cref="DeityRestrictions1"/> or <see cref="DeityRestrictions2"/> is the correct implementation.
        /// </summary>
        public IEnumerable<DeityType> DeityRestrictions1
        {
            get
            {
                List<DeityType> items = new List<DeityType>(DeityType.NUM_OF_DEITIES);

                for (int i = 0; i < DeityType.NUM_OF_DEITIES; i++)
                {
                    var item = GetDeity(i + 1);

                    if (item != null)
                    {
                        items.Add(item);
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// If no deity is returned then there are no restrictions.
        /// This convenience method does n [1,<see cref="Deities"/>]
        /// TODO: Test if <see cref="DeityRestrictions1"/> or <see cref="DeityRestrictions2"/> is the correct implementation.
        /// </summary>
        public IEnumerable<DeityType> DeityRestrictions2
        {
            get
            {
                int count = (int?)Deities ?? 0;
                List<DeityType> items = new List<DeityType>(count);

                for (int i = 0; i < count; i++)
                {
                    var item = GetDeity(i + 1);

                    if (item != null)
                    {
                        items.Add(item);
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// Returns the Required Level of an item. Items with no required level will return 0.
        /// </summary>
        public uint? RequiredLevel => GetMember<IntType>("RequiredLevel");

        /// <summary>
        /// Returns the Recommended Level of an item. Items with no recommended level will return 0.
        /// </summary>
        public uint? RecommendedLevel => GetMember<IntType>("RecommendedLevel");

        /// <summary>
        /// Skill modifier value as a percentage e.g. 12 for a Master Tailor Trophy
        /// </summary>
        public uint? SkillModValue => GetMember<IntType>("SkillModValue");

        /// <summary>
        /// Maximum absolute value of the skill mod e.g. 36 for a Master Tailor Trophy
        /// </summary>
        public uint? SkillModMax => GetMember<IntType>("SkillModMax");

        /// <summary>
        /// Does this item have Evolving experience on?
        /// </summary>
        public EvolvingItemType Evolving => GetMember<EvolvingItemType>("Evolving");
        
        /// <summary>
        /// AC value on item
        /// </summary>
        public uint? AC => GetMember<IntType>("AC");

        /// <summary>
        /// HP value on item
        /// </summary>
        public uint? HP => GetMember<IntType>("HP");

        /// <summary>
        /// STR value on item
        /// </summary>
        public uint? STR => GetMember<IntType>("STR");

        /// <summary>
        /// STA value on the item
        /// </summary>
        public uint? STA => GetMember<IntType>("STA");

        /// <summary>
        /// AGI value on item
        /// </summary>
        public uint? AGI => GetMember<IntType>("AGI");

        /// <summary>
        /// DEX value on item
        /// </summary>
        public uint? DEX => GetMember<IntType>("DEX");

        /// <summary>
        /// CHA value on item
        /// </summary>
        public uint? CHA => GetMember<IntType>("CHA");

        /// <summary>
        /// INT value on item
        /// </summary>
        public uint? INT => GetMember<IntType>("INT");

        /// <summary>
        /// WIS value on item
        /// </summary>
        public uint? WIS => GetMember<IntType>("WIS");

        /// <summary>
        /// Mana value on item
        /// </summary>
        public uint? Mana => GetMember<IntType>("Mana");

        /// <summary>
        /// ManaRegen value on item
        /// </summary>
        public uint? ManaRegen => GetMember<IntType>("ManaRegen");

        /// <summary>
        /// HP Regen value on item
        /// </summary>
        public uint? HPRegen => GetMember<IntType>("HPRegen");

        /// <summary>
        /// Endurance value on the item
        /// </summary>
        public uint? Endurance => GetMember<IntType>("Endurance");

        /// <summary>
        /// Attack value on item
        /// </summary>
        public uint? Attack => GetMember<IntType>("Attack");

        /// <summary>
        /// Cold resistance value on item
        /// </summary>
        public uint? svCold => GetMember<IntType>("svCold");

        /// <summary>
        /// Fire resistance value on item
        /// </summary>
        public uint? svFire => GetMember<IntType>("svFire");

        /// <summary>
        /// Magic resistance value on item
        /// </summary>
        public uint? svMagic => GetMember<IntType>("svMagic");

        /// <summary>
        /// Disease resistance value on item
        /// </summary>
        public uint? svDisease => GetMember<IntType>("svDisease");

        /// <summary>
        /// Poison resistance value on item
        /// </summary>
        public uint? svPoison => GetMember<IntType>("svPoison");

        /// <summary>
        /// Corruption resistance value on item
        /// </summary>
        public uint? svCorruption => GetMember<IntType>("svCorruption");

        /// <summary>
        /// Haste value on item
        /// </summary>
        public uint? Haste => GetMember<IntType>("Haste");

        /// <summary>
        /// Damage shield value on the item
        /// </summary>
        public uint? DamShield => GetMember<IntType>("DamShield");

        /// <summary>
        /// Augmentation slot type mask.
        /// </summary>
        public uint? AugType => GetMember<IntType>("AugType");

        /// <summary>
        /// Augment Restrictions
        /// </summary>
        public uint? AugRestrictions => GetMember<IntType>("AugRestrictions");

        /// <summary>
        /// Retrieve the augment in the specified slot number.
        /// Augment slots on the item (0 - 5).
        /// TODO: native MQ client has comment which might mean this is broken? // FIXME: ItemIndex
        /// </summary>
        private IndexedMember<AugType, int> _augSlot;

        /// <summary>
        /// Retrieve the augment in the specified slot number.
        /// Augment slots on the item (0 - 5).
        /// TODO: native MQ client has comment which might mean this is broken? // FIXME: ItemIndex
        /// </summary>
        /// <param name="slotNumber"></param>
        /// <returns></returns>
        public AugType GetAugSlot(int slotNumber) => _augSlot[slotNumber];

        /// <summary>
        /// Returns the type of agument in slot 1
        /// </summary>
        public uint? AugSlot1 => GetMember<IntType>("AugSlot1");

        /// <summary>
        /// Returns the type of agument in slot 2
        /// </summary>
        public uint? AugSlot2 => GetMember<IntType>("AugSlot2");

        /// <summary>
        /// Returns the type of agument in slot 3
        /// </summary>
        public uint? AugSlot3 => GetMember<IntType>("AugSlot3");

        /// <summary>
        /// Returns the type of agument in slot 4
        /// </summary>
        public uint? AugSlot4 => GetMember<IntType>("AugSlot4");

        /// <summary>
        /// Returns the type of agument in slot 5
        /// </summary>
        public uint? AugSlot5 => GetMember<IntType>("AugSlot5");

        /// <summary>
        /// Returns the type of agument in slot 6
        /// </summary>
        public uint? AugSlot6 => GetMember<IntType>("AugSlot6");

        /// <summary>
        /// Power left on power source
        /// </summary>
        public uint? Power => GetMember<IntType>("Power");

        /// <summary>
        /// Percentage power remaining on a power source
        /// </summary>
        public float? PctPower => GetMember<FloatType>("PctPower");

        /// <summary>
        /// Max power on a power source
        /// </summary>
        public uint? MaxPower => GetMember<IntType>("MaxPower");

        /// <summary>
        /// Purity of item
        /// </summary>
        public uint? Purity => GetMember<IntType>("Purity");

        /// <summary>
        /// Range of a ranged weapon
        /// </summary>
        public uint? Range => GetMember<IntType>("Range");

        /// <summary>
        /// Avoidance value on the item
        /// </summary>
        public uint? Avoidance => GetMember<IntType>("Avoidance");

        /// <summary>
        /// Spell shield value on the item
        /// </summary>
        public uint? SpellShield => GetMember<IntType>("SpellShield");

        /// <summary>
        /// Strikethrough value on item
        /// </summary>
        public uint? StrikeThrough => GetMember<IntType>("StrikeThrough");

        /// <summary>
        /// Stun resist value on item
        /// </summary>
        public uint? StunResist => GetMember<IntType>("StunResist");

        /// <summary>
        /// Shielding value on the item
        /// </summary>
        public uint? Shielding => GetMember<IntType>("Shielding");

        /// <summary>
        /// Accuracy value on item
        /// </summary>
        public uint? Accuracy => GetMember<IntType>("Accuracy");

        /// <summary>
        /// CombatEffects value on the item. (no info in doco)
        /// </summary>
        public uint? CombatEffects => GetMember<IntType>("CombatEffects");

        /// <summary>
        /// DoT Shielding value on the item
        /// </summary>
        public uint? DoTShielding => GetMember<IntType>("DoTShielding");

        /// <summary>
        /// Heroic STR value on the item
        /// Increases endurance pool, endurance regen, and the maximum amount of endurance regen a character can have
        /// Also increases damage done by melee attacks and improves the bonus granted to armor class while using a shield
        /// (10 Heroic STR increases each Melee Hit by 1 point)
        /// </summary>
        public uint? HeroicSTR => GetMember<IntType>("HeroicSTR");

        /// <summary>
        /// Heroic INT value on the item
        /// Increases mana pool, mana regen, and the maximum amount of mana regen an int-based caster can have
        /// It requires +25 heroic intel to gain a single point of +mana regeneration
        /// </summary>
        public uint? HeroicINT => GetMember<IntType>("HeroicINT");

        /// <summary>
        /// Heroic WIS value on the item
        /// Increases mana pool, mana regen, and the maximum amount of mana regen a wis-based caster can have
        /// </summary>
        public uint? HeroicWIS => GetMember<IntType>("HeroicWIS");

        /// <summary>
        /// Heroic AGI value on the item
        /// Increases endurance pool, endurance regen, and the maximum amount of endurance regen a character can have
        /// Also increases the chance to dodge an attack, grants a bonus to defense skill, and reduces falling damage
        /// </summary>
        public uint? HeroicAGI => GetMember<IntType>("HeroicAGI");

        /// <summary>
        /// Heroic DEX value on the item
        /// Increases endurance pool, endurance regen, and the maximum amount of endurance regen a character can have
        /// Also increases damage done by ranged attacks, improves chance to successfully assassinate or headshot, and improves the chance to riposte, block, and parry incoming attacks
        /// </summary>
        public uint? HeroicDEX => GetMember<IntType>("HeroicDEX");

        /// <summary>
        /// Heroic STA value on the item
        /// Increases hit point pool, hit point regen, and the maximum amount of hit point regen a character can have
        /// Also increases endurance pool, endurance regen, and the maximum amount of endurance regen a character can have.
        /// </summary>
        public uint? HeroicSTA => GetMember<IntType>("HeroicSTA");

        /// <summary>
        /// Heroic CHA value on the item
        /// Improves reaction rolls with some NPCs and increases the amount of faction you gain or lose when faction is adjusted
        /// </summary>
        public uint? HeroicCHA => GetMember<IntType>("HeroicCHA");

        /// <summary>
        /// Don't use this
        /// </summary>
        [Obsolete]
        public uint? HeroicSvMagic => GetMember<IntType>("HeroicSvMagic");

        /// <summary>
        /// Don't use this
        /// </summary>
        [Obsolete]
        public uint? HeroicSvFire => GetMember<IntType>("HeroicSvFire");

        /// <summary>
        /// Don't use this
        /// </summary>
        [Obsolete]
        public uint? HeroicSvCold => GetMember<IntType>("HeroicSvCold");

        /// <summary>
        /// Don't use this
        /// </summary>
        [Obsolete]
        public uint? HeroicSvDisease => GetMember<IntType>("HeroicSvDisease");

        /// <summary>
        /// Don't use this
        /// </summary>
        [Obsolete]
        public uint? HeroicSvPoison => GetMember<IntType>("HeroicSvPoison");

        /// <summary>
        /// Don't use this
        /// </summary>
        [Obsolete]
        public uint? HeroicSvCorruption => GetMember<IntType>("HeroicSvCorruption");

        /// <summary>
        /// Endurance regen value on the item
        /// </summary>
        public uint? EnduranceRegen => GetMember<IntType>("EnduranceRegen");

        /// <summary>
        /// HealAmount (regen?)
        /// </summary>
        public uint? HealAmount => GetMember<IntType>("HealAmount");

        /// <summary>
        /// Clairvoyance value on item
        /// </summary>
        public uint? Clairvoyance => GetMember<IntType>("Clairvoyance");

        /// <summary>
        /// Damage shield mitigation value on the item
        /// </summary>
        public uint? DamageShieldMitigation => GetMember<IntType>("DamageShieldMitigation");

        /// <summary>
        /// Spell damage value on the item
        /// </summary>
        public uint? SpellDamage => GetMember<IntType>("SpellDamage");

        /// <summary>
        /// Number of augs on this item
        /// </summary>
        public uint? Augs => GetMember<IntType>("Augs");

        /// <summary>
        /// Used in tradeskills?
        /// </summary>
        public bool Tradeskills => GetMember<BoolType>("Tradeskills");

        /// <summary>
        /// Prestige? (Usable only by gold accounts)
        /// </summary>
        public bool Prestige => GetMember<BoolType>("Prestige");

        /// <summary>
        /// If the item is a container, the 1 based index of the first free slot in it
        /// TODO: looks broken from comments in native source. - // FIXME: Check with world container
        /// </summary>
        public uint? FirstFreeSlot => GetMember<IntType>("FirstFreeSlot");

        /// <summary>
        /// If the item is a container, the number of slots in it taken up by a given item name
        /// </summary>
        public uint? SlotsUsedByItem => GetMember<IntType>("SlotsUsedByItem");

        /// <summary>
        /// Heirloom item?
        /// </summary>
        public bool Heirloom => GetMember<BoolType>("Heirloom");

        /// <summary>
        /// Item is collectible? (no info in doco)
        /// </summary>
        public bool Collectible => GetMember<BoolType>("Collectible");

        /// <summary>
        /// No destroy?
        /// </summary>
        public bool NoDestroy => GetMember<BoolType>("NoDestroy");

        /// <summary>
        /// Quest item?
        /// </summary>
        public bool Quest => GetMember<BoolType>("Quest");

        /// <summary>
        /// Is the item expendable?
        /// </summary>
        public bool Expendable => GetMember<BoolType>("Expendable");

        /// <summary>
        /// Just prints the actual hexlink for an item (not clickable)
        /// </summary>
        public string ItemLink => GetMember<StringType>("ItemLink");

        /// <summary>
        /// Just prints the actual hexlink for an item (clickable)
        /// ItemLink[CLICKABLE]
        /// </summary>
        public string ItemLinkClickable => GetMember<StringType>("ItemLink", "CLICKABLE");

        /// <summary>
        /// ID of the icon used for the item
        /// </summary>
        public uint? Icon => GetMember<IntType>("Icon");

        /// <summary>
        /// Ornamentation icon
        /// </summary>
        public uint? OrnamentationIcon => GetMember<IntType>("OrnamentationIcon");

        /// <summary>
        /// Size of items that can be placed in the container (4 = Giant) - (no info in doco)
        /// TODO: test enum conversion
        /// </summary>
        public ItemSize? ContentSize => GetMember<IntType>("ContentSize");

        /// <summary>
        /// Usable by character
        /// </summary>
        public bool CanUse => GetMember<BoolType>("CanUse");

        /// <summary>
        /// Luck value on item
        /// </summary>
        public uint? Luck => GetMember<IntType>("Luck");

        /// <summary>
        /// Minimum luck value on item
        /// </summary>
        public uint? MinLuck => GetMember<IntType>("MinLuck");

        /// <summary>
        /// Maximum luck value on item
        /// </summary>
        public uint? MaxLuck => GetMember<IntType>("MaxLuck");

        /// <summary>
        /// TODO: new member, used to be a pointer pre 2020-04-13
        /// </summary>
        public uint? IDFile => GetMember<IntType>("IDFile");

        /// <summary>
        /// TODO: new member, used to be a pointer pre 2020-04-13
        /// </summary>
        public uint? IDFile2 => GetMember<IntType>("IDFile2");

        /// <summary>
        /// Opens the item display window for this item
        /// </summary>
        public void Inspect() => GetMember<MQ2DataType>("Inspect");

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