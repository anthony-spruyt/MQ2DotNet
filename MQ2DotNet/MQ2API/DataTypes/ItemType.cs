using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MQ2DotNet.EQ;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an item. This is used for both generic item information (ITEMINFO), and a specific item (CONTENTS).
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("item")]
    public class ItemType : MQ2DataType
    {
        internal ItemType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Item = new IndexedMember<ItemType, int>(this, "Item");
            WornSlot = new IndexedMember<BoolType, string, InvSlotType, int>(this, "WornSlot");
            Class = new IndexedMember<ClassType, int, ClassType, string>(this, "Class");
            Race = new IndexedMember<RaceType, int, RaceType, string>(this, "Race");
            Deity = new IndexedMember<DeityType, int, DeityType, string>(this, "Deity");
            AugSlot = new IndexedMember<AugType, int>(this, "AugSlot");
        }

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? RefCount => GetMember<IntType>("RefCount");

        /// <summary>
        /// ID of the item
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Is lore item?
        /// </summary>
        public bool Lore => GetMember<BoolType>("Lore");

        /// <summary>
        /// Item is lore when equipped?
        /// </summary>
        public bool LoreEquipped => GetMember<BoolType>("LoreEquipped");

        /// <summary>
        /// No drop?
        /// </summary>
        public bool NoDrop => NoTrade;

        /// <summary>
        /// No trade? Same as <see cref="NoDrop"/>
        /// </summary>
        public bool NoTrade => GetMember<BoolType>("NoTrade");

        /// <summary>
        /// No rent?
        /// </summary>
        public bool NoRent => GetMember<BoolType>("NoRent");

        /// <summary>
        /// Magic?
        /// </summary>
        public bool Magic => GetMember<BoolType>("Magic");

        /// <summary>
        /// Value in coppers
        /// </summary>
        public uint? Value => GetMember<IntType>("Value");

        /// <summary>
        /// Item size (1 = Small, 2 = Medium, 3 = Large, 4 = Giant)
        /// </summary>
        public ItemSize? Size => GetMember<IntType>("Size");

        /// <summary>
        /// If item is a container, the size of items it can hold (1 = Small, 2 = Medium, 3 = Large, 4 = Giant)
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
        /// If the item is a container, the number of slots it has
        /// </summary>
        public uint? Container => GetMember<IntType>("Container");

        /// <summary>
        /// Item is a container and is open
        /// </summary>
        public uint? Open => GetMember<IntType>("Open");

        /// <summary>
        /// If the item is a container, the number of items in it
        /// </summary>
        public uint? Items => GetMember<IntType>("Items");

        /// <summary>
        /// Clicky spell on the item
        /// </summary>
        public ItemSpellType Clicky => GetMember<ItemSpellType>("Clicky");

        /// <summary>
        /// Proc on the weapon
        /// </summary>
        public ItemSpellType Proc => GetMember<ItemSpellType>("Proc");

        /// <summary>
        /// Spell effect when item is worn
        /// </summary>
        public ItemSpellType Worn => GetMember<ItemSpellType>("Worn");

        /// <summary>
        /// Focus effect on the item
        /// </summary>
        public ItemSpellType Focus => GetMember<ItemSpellType>("Focus");

        /// <summary>
        /// Spell taught by the item if it is a scroll
        /// </summary>
        public ItemSpellType Scroll => GetMember<ItemSpellType>("Scroll");

        /// <summary>
        /// Second focus effect on the item
        /// </summary>
        public ItemSpellType Focus2 => GetMember<ItemSpellType>("Focus2");

        /// <summary>
        /// Mount spell on the item
        /// </summary>
        public ItemSpellType Mount => GetMember<ItemSpellType>("Mount");

        /// <summary>
        /// Illusion spell on the item
        /// </summary>
        public ItemSpellType Illusion => GetMember<ItemSpellType>("Illusion");

        /// <summary>
        /// Familiar spell cast by the item
        /// </summary>
        public ItemSpellType Familiar => GetMember<ItemSpellType>("Familiar");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public ItemSpellType Blessing => GetMember<ItemSpellType>("Blessing");

        /// <summary>
        /// If the item is a container, the item in the nth slot (1 based)
        /// </summary>
        [JsonIgnore]
        public IndexedMember<ItemType, int> Item { get; }

        /// <summary>
        /// Stackable?
        /// </summary>
        public bool Stackable => GetMember<BoolType>("Stackable");

        /// <summary>
        /// Inventory slot the item is in (not the slot it can be equipped in)
        /// </summary>
        public InvSlotType InvSlot => GetMember<InvSlotType>("InvSlot");

        /// <summary>
        /// Item slot number the item is currently in
        /// </summary>
        public int? ItemSlot => GetMember<IntType>("ItemSlot");

        /// <summary>
        /// If the item is in a container, the index (0 based) of the slot within the container
        /// </summary>
        public int? ItemSlot2 => GetMember<IntType>("ItemSlot2");

        /// <summary>
        /// The cost to buy this item from the active merchant
        /// </summary>
        public long? BuyPrice => GetMember<Int64Type>("BuyPrice");

        /// <summary>
        /// Price to sell this item at this merchant if one is open
        /// </summary>
        public uint? SellPrice => GetMember<IntType>("SellPrice");

        /// <summary>
        /// Can item be worn in invslot with name, or the nth invslot (1 based) that the item can be worn in
        /// </summary>
        [JsonIgnore]
        public IndexedMember<BoolType, string, InvSlotType, int> WornSlot { get; }

        /// <summary>
        /// Number of slots this item can be worn in (fingers/ears count as 2)
        /// </summary>
        public uint? WornSlots => GetMember<IntType>("WornSlots");

        /// <summary>
        /// Cast time on clicky ItemSpell
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
        /// Time remaining on recast timer
        /// </summary>
        public TimeSpan? Timer => GetMember<TicksType>("Timer");

        /// <summary>
        /// Damage value on the weapon
        /// </summary>
        public uint? Damage => GetMember<IntType>("Damage");

        /// <summary>
        /// Weapon delay
        /// </summary>
        public uint? ItemDelay => GetMember<IntType>("ItemDelay");

        /// <summary>
        /// Number of seconds remaining on recast timer
        /// </summary>
        public uint? TimerReady => GetMember<IntType>("TimerReady");

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
        /// The number of items needed to fill all the stacks of the item you have
        /// If you have 3 stacks (1, 10, 20 in those stacks), you have room for 60 total and you have 31 on you, so FreeStack would return 29
        /// </summary>
        public uint? FreeStack => GetMember<IntType>("FreeStack");

        /// <summary>
        /// Quantity of item active merchant has
        /// </summary>
        public uint? MerchQuantity => GetMember<IntType>("MerchQuantity");

        /// <summary>
        /// Number of classes that can use this item
        /// </summary>
        public uint? Classes => GetMember<IntType>("Classes");

        /// <summary>
        /// Class that can use the item, by number (1 - Classes), or by class name or 3 letter code
        /// TODO: see if we can use <see cref="MQ2DotNet.EQ.Class"/> here to improve usage.
        /// </summary>
        [JsonIgnore]
        public IndexedMember<ClassType, int, ClassType, string> Class { get; }

        /// <summary>
        /// Number of races that can use the item. 16 if usable by all races
        /// </summary>
        public uint? Races => GetMember<IntType>("Races");

        /// <summary>
        /// Race that can use the item, by number (1 - Races), or by name (full name e.g. Froglok, not FRG)
        /// </summary>
        [JsonIgnore]
        public IndexedMember<RaceType, int, RaceType, string> Race { get; }

        /// <summary>
        /// Number of deities that can use the item. Returns 0 if there are no restrictions
        /// </summary>
        public uint? Deities => GetMember<IntType>("Deities");

        /// <summary>
        /// Deity that can use the item, by number (1 - Deities), or by name. Returns null if there are no restrictions
        /// TODO: look at adding in an enum here.
        /// </summary>
        [JsonIgnore]
        public IndexedMember<DeityType, int, DeityType, string> Deity { get; }

        /// <summary>
        /// Required level to wear the item. Items with no required level will return 0
        /// </summary>
        public uint? RequiredLevel => GetMember<IntType>("RequiredLevel");

        /// <summary>
        /// Recommended level to wear the item. Items with no recommended level will return 0
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
        /// Details about the evolving item, if it is one
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
        /// 
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
        /// Type of slot the item fits in, if it is an augment
        /// </summary>
        public uint? AugType => GetMember<IntType>("AugType");

        /// <summary>
        /// Augment Restrictions
        /// </summary>
        public uint? AugRestrictions => GetMember<IntType>("AugRestrictions");

        /// <summary>
        /// Augment slots on the item (0 - 5).
        /// TODO: native MQ client has comment which might mean this is broken? // FIXME: ItemIndex
        /// </summary>
        [JsonIgnore]
        public IndexedMember<AugType, int> AugSlot { get; }

        /// <summary>
        /// Type of the 1st aug slot
        /// </summary>
        public uint? AugSlot1 => GetMember<IntType>("AugSlot1");

        /// <summary>
        /// Type of the 2nd aug slot
        /// </summary>
        public uint? AugSlot2 => GetMember<IntType>("AugSlot2");

        /// <summary>
        /// Type of the 3rd aug slot
        /// </summary>
        public uint? AugSlot3 => GetMember<IntType>("AugSlot3");

        /// <summary>
        /// Type of the 4th aug slot
        /// </summary>
        public uint? AugSlot4 => GetMember<IntType>("AugSlot4");

        /// <summary>
        /// Type of the 5th aug slot
        /// </summary>
        public uint? AugSlot5 => GetMember<IntType>("AugSlot5");

        /// <summary>
        /// Type of the 6th aug slot
        /// </summary>
        public uint? AugSlot6 => GetMember<IntType>("AugSlot6");

        /// <summary>
        /// Power remaining on a power source
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
        /// Purity value on the item
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
        /// CombatEffects value on the item
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
        /// Heal amount value on the item
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
        /// Item is collectible?
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
        /// just prints the actual hexlink for an item (not clickable)
        /// </summary>
        public string ItemLink => GetMember<StringType>("ItemLink");

        /// <summary>
        /// ID of the icon used for the item
        /// </summary>
        public uint? Icon => GetMember<IntType>("Icon");

        /// <summary>
        /// Ornamentation icon
        /// </summary>
        public uint? OrnamentationIcon => GetMember<IntType>("OrnamentationIcon");

        /// <summary>
        /// Size of items that can be placed in the container (4 = Giant)
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
        /// TODO: new member
        /// </summary>
        public void Inspect() => GetMember<MQ2DataType>("Inspect");
    }
}