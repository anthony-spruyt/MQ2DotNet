using JetBrains.Annotations;
using MQ2DotNet.EQ;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// TODO: Update members and methods according to doco and implement indexed member wrapper methods and properties.
    /// MQ2 type for the local player character.
    /// Last Verified: 2023-06-30
    /// https://docs.macroquest.org/reference/data-types/datatype-character/
    /// </summary>
    [PublicAPI]
    [MQ2Type("character")]
    public class CharacterType : MQ2DataType
    {
        internal CharacterType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _blockedPetBuff = new IndexedMember<SpellType, int>(this, "BlockedPetBuff");
            _blockedBuff = new IndexedMember<SpellType, int>(this, "BlockedBuff");
            _buff = new IndexedMember<BuffType, string, BuffType, int>(this, "Buff");
            _song = new IndexedMember<BuffType, string, BuffType, int>(this, "Song");
            _findBuff = new IndexedMember<BuffType, string>(this, "FindBuff");
            _inventory = new IndexedMember<ItemType, string, ItemType, int>(this, "Inventory");
            _bank = new IndexedMember<ItemType, int>(this, "Bank");
            _sharedBank = new IndexedMember<ItemType, int>(this, "SharedBank");
            _gem = new IndexedMember<SpellType, int, IntType, string>(this, "Gem");
            _languageSkill = new IndexedMember<IntType, int, IntType, string>(this, "LanguageSkill");
            _combatAbility = new IndexedMember<SpellType, int, IntType, string>(this, "CombatAbility");
            _combatAbilityTimer = new IndexedMember<TicksType, int, TicksType, string>(this, "CombatAbilityTimer");
            _combatAbilityReady = new IndexedMember<BoolType, int, BoolType, string>(this, "CombatAbilityReady");
            _altAbilityTimer = new IndexedMember<TimeStampType, int, TimeStampType, string>(this, "AltAbilityTimer");
            _altAbilityReady = new IndexedMember<BoolType, int, BoolType, string>(this, "AltAbilityReady");
            _altAbility = new IndexedMember<AltAbilityType, int, AltAbilityType, string>(this, "AltAbility");
            _skill = new IndexedMember<IntType, string, IntType, int>(this, "Skill");
            _skillBase = new IndexedMember<IntType, string, IntType, int>(this, "SkillBase");
            _skillCap = new IndexedMember<IntType, string, IntType, int>(this, "SkillCap");
            _ability = new IndexedStringMember<int, IntType, string>(this, "Ability");
            _abilityReady = new IndexedMember<BoolType, int, BoolType, string>(this, "AbilityReady");
            _book = new IndexedMember<SpellType, int, IntType, string>(this, "Book");
            _spell = new IndexedMember<SpellType, int, SpellType, string>(this, "Spell");
            _itemReady = new IndexedMember<BoolType, int, BoolType, string>(this, "ItemReady");
            _spellReady = new IndexedMember<BoolType, int, BoolType, string>(this, "SpellReady");
            _petBuff = new IndexedMember<SpellType, int, IntType, string>(this, "PetBuff");
            _raidAssistTarget = new IndexedMember<SpawnType, int>(this, "RaidAssistTarget");
            _raidMarkNPC = new IndexedMember<SpawnType, int>(this, "RaidMarkNPC");
            _groupMarkNPC = new IndexedMember<SpawnType, int>(this, "GroupMarkNPC");
            _language = new IndexedStringMember<int, IntType, string>(this, "Language");
            _aura = new IndexedMember<AuraType, string, AuraType, int>(this, "Aura");
            _xTAggroCount = new IndexedMember<IntType, int>(this, "XTAggroCount");
            _xTarget = new IndexedMember<XTargetType, int, XTargetType, string>(this, "XTarget");
            _sPA = new IndexedMember<IntType, int>(this, "SPA");
            _gemTimer = new IndexedMember<TimeStampType, int, TimeStampType, string>(this, "GemTimer");
            _haveExpansion = new IndexedMember<BoolType, int, BoolType, string>(this, "HaveExpansion");
            _altCurrency = new IndexedMember<IntType, int, IntType, string>(this, "AltCurrency");
            _mercListInfo = new IndexedStringMember<int, IntType, string>(this, "MercListInfo");
            _boundLocation = new IndexedMember<WorldLocationType, int>(this, "BoundLocation");
            _autoSkill = new IndexedMember<SkillType, int>(this, "AutoSkill");
            _bandolier = new IndexedMember<BandolierType, string, BandolierType, int>(this, "Bandolier");
            _abilityTimer = new IndexedMember<TimeStampType, int, TimeStampType, string>(this, "AbilityTimer");
        }

        /// <summary>
        /// The player name.
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// The player origin zone.
        /// </summary>
        public ZoneType Origin => GetMember<ZoneType>("Origin");

        /// <summary>
        /// TODO: new field
        /// </summary>
        public int? SubscriptionDays => GetMember<IntType>("SubscriptionDays");

        /// <summary>
        /// Experience (out of 330)
        /// </summary>
        public long? Exp => GetMember<Int64Type>("Exp");

        /// <summary>
        /// Current experience as a percentage
        /// </summary>
        public float? PctExp => GetMember<FloatType>("PctExp");

        /// <summary>
        /// Percentage of your experience going to AA
        /// </summary>
        public int? PctExpToAA => GetMember<IntType>("PctExpToAA");

        /// <summary>
        /// Current AA experience as a percentage
        /// </summary>
        public float? PctAAExp => GetMember<FloatType>("PctAAExp");

        /// <summary>
        /// Current vitality
        /// </summary>
        public long? Vitality => GetMember<Int64Type>("Vitality");

        /// <summary>
        /// TODO: new field
        /// </summary>
        public long? VitalityCap => GetMember<Int64Type>("VitalityCap");

        /// <summary>
        /// Current vitality as a percentage
        /// </summary>
        public float? PctVitality => GetMember<FloatType>("PctVitality");

        /// <summary>
        /// The total number of AA Vitality you have
        /// </summary>
        public long? AAVitality => GetMember<Int64Type>("AAVitality");

        /// <summary>
        /// ?
        /// </summary>
        public long? AAVitalityCap => GetMember<Int64Type>("AAVitalityCap");

        /// <summary>
        /// Current AA vitality as a percentage
        /// </summary>
        public float? PctAAVitality => GetMember<FloatType>("PctAAVitality");

        /// <summary>
        /// The character's spawn
        /// </summary>
        public SpawnType Spawn => GetMember<SpawnType>("Spawn");

        /// <summary>
        /// Current hit points
        /// Override this as the spawn has a different data type for the member.
        /// </summary>
        public uint? CurrentHPs => GetMember<IntType>("CurrentHPs");

        /// <summary>
        /// Maximum hit points
        /// </summary>
        public int? MaxHPs => GetMember<IntType>("MaxHPs");

        /// <summary>
        /// HP as a percentage
        /// </summary>
        public int? PctHPs => GetMember<IntType>("PctHPs");

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
        public uint? PctMana => GetMember<IntType>("PctMana");

        /// <summary>
        /// Number of buffs you have, not including short duration buffs (songs)
        /// </summary>
        public uint? CountBuffs => GetMember<IntType>("CountBuffs");

        /// <summary>
        /// Number of short duration buffs (songs) you have
        /// </summary>
        public uint? CountSongs => GetMember<IntType>("CountSongs");

        /// <summary>
        /// Blocked pet buff by index, valid index are 1 - 40
        /// In the MQ client this looks to be the same as <see cref="_blockedBuff"/>
        /// </summary>
        private readonly IndexedMember<SpellType, int> _blockedPetBuff;

        /// <summary>
        /// Blocked buff by index, valid index are 1 - 40
        /// In the MQ client this looks to be the same as <see cref="_blockedPetBuff"/>
        /// </summary>
        private readonly IndexedMember<SpellType, int> _blockedBuff;

        /// <summary>
        /// Buff by name or slot number
        /// </summary>
        private readonly IndexedMember<BuffType, string, BuffType, int> _buff;

        /// <summary>
        /// Song (short buff) by name or slot number
        /// </summary>
        private readonly IndexedMember<BuffType, string, BuffType, int> _song;

        /// <summary>
        /// TODO: new member
        /// </summary>
        private readonly IndexedMember<BuffType, string> _findBuff;

        /// <summary>
        /// Hit point bonus from gear and spells
        /// </summary>
        public uint? HPBonus => GetMember<IntType>("HPBonus");

        /// <summary>
        /// Mana bonus from gear and spells
        /// </summary>
        public uint? ManaBonus => GetMember<IntType>("ManaBonus");

        /// <summary>
        /// Endurance bonus from gear and spells
        /// </summary>
        public uint? EnduranceBonus => GetMember<IntType>("EnduranceBonus");

        /// <summary>
        /// Combat Effects bonus from gear and spells
        /// </summary>
        public uint? CombatEffectsBonus => GetMember<IntType>("CombatEffectsBonus");

        /// <summary>
        /// Shielding bonus from gear and spells
        /// </summary>
        public uint? ShieldingBonus => GetMember<IntType>("ShieldingBonus");

        /// <summary>
        /// Spell Shield bonus from gear and spells
        /// </summary>
        public uint? SpellShieldBonus => GetMember<IntType>("SpellShieldBonus");

        /// <summary>
        /// Avoidance bonus from gear/spells
        /// </summary>
        public uint? AvoidanceBonus => GetMember<IntType>("AvoidanceBonus");

        /// <summary>
        /// Accuracy bonus from gear and spells
        /// </summary>
        public uint? AccuracyBonus => GetMember<IntType>("AccuracyBonus");

        /// <summary>
        /// Stun Resist bonus from gear and spells
        /// </summary>
        public uint? StunResistBonus => GetMember<IntType>("StunResistBonus");

        /// <summary>
        /// Strikethrough bonus from gear and spells
        /// </summary>
        public uint? StrikeThroughBonus => GetMember<IntType>("StrikeThroughBonus");

        /// <summary>
        /// DoT Shield bonus from gear and spells
        /// </summary>
        public uint? DoTShieldBonus => GetMember<IntType>("DoTShieldBonus");

        /// <summary>
        /// Attack bonus from gear and spells
        /// </summary>
        public uint? AttackBonus => GetMember<IntType>("AttackBonus");

        /// <summary>
        /// HP regen bonus from gear and spells
        /// </summary>
        public uint? HPRegenBonus => GetMember<IntType>("HPRegenBonus");

        /// <summary>
        /// Mana regen bonus from gear and spells
        /// </summary>
        public uint? ManaRegenBonus => GetMember<IntType>("ManaRegenBonus");

        /// <summary>
        /// Damage Shield bonus from gear and spells
        /// </summary>
        public uint? DamageShieldBonus => GetMember<IntType>("DamageShieldBonus");

        /// <summary>
        /// Damage Shield Mitigation bonus from gear and spells
        /// </summary>
        public uint? DamageShieldMitigationBonus => GetMember<IntType>("DamageShieldMitigationBonus");

        /// <summary>
        /// Total Heroic Strength bonus from gear
        /// Increases endurance pool, endurance regen, and the maximum amount of endurance regen a character can have
        /// Also increases damage done by melee attacks and improves the bonus granted to armor class while using a shield
        /// (10 Heroic STR increases each Melee Hit by 1 point)
        /// </summary>
        public uint? HeroicSTRBonus => GetMember<IntType>("HeroicSTRBonus");

        /// <summary>
        /// Total Heroic Intelligence bonus from gear
        /// Increases mana pool, mana regen, and the maximum amount of mana regen an int-based caster can have
        /// It requires +25 heroic intel to gain a single point of +mana regeneration
        /// </summary>
        public uint? HeroicINTBonus => GetMember<IntType>("HeroicINTBonus");

        /// <summary>
        /// Total Heroic Wisdom bonus from gear
        /// Increases mana pool, mana regen, and the maximum amount of mana regen a wis-based caster can have
        /// </summary>
        public uint? HeroicWISBonus => GetMember<IntType>("HeroicWISBonus");

        /// <summary>
        /// Total Heroic Agility bonus from gear
        /// Increases endurance pool, endurance regen, and the maximum amount of endurance regen a character can have
        /// Also increases the chance to dodge an attack, grants a bonus to defense skill, and reduces falling damage
        /// </summary>
        public uint? HeroicAGIBonus => GetMember<IntType>("HeroicAGIBonus");

        /// <summary>
        /// Total Heroic Dexterity bonus from gear
        /// Increases endurance pool, endurance regen, and the maximum amount of endurance regen a character can have
        /// Also increases damage done by ranged attacks, improves chance to successfully assassinate or headshot, and improves the chance to riposte, block, and parry incoming attacks
        /// </summary>
        public uint? HeroicDEXBonus => GetMember<IntType>("HeroicDEXBonus");

        /// <summary>
        /// Total Heroic Stamina bonus from gear
        /// Increases hit point pool, hit point regen, and the maximum amount of hit point regen a character can have
        /// Also increases endurance pool, endurance regen, and the maximum amount of endurance regen a character can have.
        /// </summary>
        public uint? HeroicSTABonus => GetMember<IntType>("HeroicSTABonus");

        /// <summary>
        /// Total Heroic Charisma bonus from gear
        /// Improves reaction rolls with some NPCs and increases the amount of faction you gain or lose when faction is adjusted
        /// </summary>
        public uint? HeroicCHABonus => GetMember<IntType>("HeroicCHABonus");

        /// <summary>
        /// Total Heal Amount bonus from gear
        /// </summary>
        public uint? HealAmountBonus => GetMember<IntType>("HealAmountBonus");

        /// <summary>
        /// Spell Damage bonus
        /// </summary>
        public uint? SpellDamageBonus => GetMember<IntType>("SpellDamageBonus");

        /// <summary>
        /// Clairvoyance Bonus
        /// </summary>
        public uint? ClairvoyanceBonus => GetMember<IntType>("ClairvoyanceBonus");

        /// <summary>
        /// Endurance regen bonus
        /// </summary>
        public uint? EnduranceRegenBonus => GetMember<IntType>("EnduranceRegenBonus");

        /// <summary>
        /// Your Attack Speed. No haste spells/items = AttackSpeed of 100. A 41% haste item will result in an AttackSpeed of 141. This variable does not take into account spell or song haste
        /// </summary>
        public uint? AttackSpeed => GetMember<IntType>("AttackSpeed");

        /// <summary>
        /// Current Endurance points. Use <see cref="CurrentEndurance"/>
        /// </summary>
        [Obsolete]
        public uint? Endurance => CurrentEndurance;

        /// <summary>
        /// Current Endurance points
        /// </summary>
        public uint? CurrentEndurance => GetMember<IntType>("CurrentEndurance");

        /// <summary>
        /// Maximum Endurance points
        /// </summary>
        public uint? MaxEndurance => GetMember<IntType>("MaxEndurance");

        /// <summary>
        /// Endurance as a percentage (0-100)
        /// </summary>
        public uint? PctEndurance => GetMember<IntType>("PctEndurance");

        /// <summary>
        /// Total points earned in Deepest Guk LDoN missions
        /// </summary>
        public uint? GukEarned => GetMember<IntType>("GukEarned");

        /// <summary>
        /// Total points earned in Mistmoore LDoN missions
        /// </summary>
        public uint? MMEarned => GetMember<IntType>("MMEarned");

        /// <summary>
        /// Total points earned in Rujurkian LDoN missions
        /// </summary>
        public uint? RujEarned => GetMember<IntType>("RujEarned");

        /// <summary>
        /// Total points earned in Takish LDoN missions
        /// </summary>
        public uint? TakEarned => GetMember<IntType>("TakEarned");

        /// <summary>
        /// Total points earned in Miragul's LDoN missions
        /// </summary>
        public uint? MirEarned => GetMember<IntType>("MirEarned");

        /// <summary>
        /// Total points earned across all LDoN missions
        /// </summary>
        public uint? LDoNPoints => GetMember<IntType>("LDoNPoints");

        /// <summary>
        /// Current favor/tribute
        /// </summary>
        public long? CurrentFavor => GetMember<Int64Type>("CurrentFavor");

        /// <summary>
        /// Career favor/tribute
        /// </summary>
        public long? CareerFavor => GetMember<Int64Type>("CareerFavor");

        /// <summary>
        /// An item from your inventory by slot name or number
        /// </summary>
        private readonly IndexedMember<ItemType, string, ItemType, int> _inventory;

        /// <summary>
        /// Item in this bankslot #
        /// </summary>
        private readonly IndexedMember<ItemType, int> _bank;

        /// <summary>
        /// TODO: new member
        /// </summary>
        private readonly IndexedMember<ItemType, int> _sharedBank;

        /// <summary>
        /// Platinum in your shared bank
        /// </summary>
        public uint? PlatinumShared => GetMember<IntType>("PlatinumShared");

        /// <summary>
        /// Total cash on your character, expressed in coppers (eg. if you are carrying 100pp, Cash will return 100000)
        /// </summary>
        public long? Cash => GetMember<Int64Type>("Cash");

        /// <summary>
        /// Platinum on your character
        /// </summary>
        public uint? Platinum => GetMember<IntType>("Platinum");

        /// <summary>
        /// Platinum on your cursor
        /// </summary>
        public uint? CursorPlatinum => GetMember<IntType>("CursorPlatinum");

        /// <summary>
        /// Gold on your character
        /// </summary>
        public uint? Gold => GetMember<IntType>("Gold");

        /// <summary>
        /// Gold on your cursor
        /// </summary>
        public uint? CursorGold => GetMember<IntType>("CursorGold");

        /// <summary>
        /// Silver on your character
        /// </summary>
        public uint? Silver => GetMember<IntType>("Silver");

        /// <summary>
        /// Silver on your cursor
        /// </summary>
        public uint? CursorSilver => GetMember<IntType>("CursorSilver");

        /// <summary>
        /// Copper on your character
        /// </summary>
        public uint? Copper => GetMember<IntType>("Copper");

        /// <summary>
        /// Copper on your cursor
        /// </summary>
        public uint? CursorCopper => GetMember<IntType>("CursorCopper");

        /// <summary>
        /// Total cash in your bank, expressed in coppers
        /// </summary>
        public long? CashBank => GetMember<Int64Type>("CashBank");

        /// <summary>
        /// Platinum in your bank
        /// </summary>
        public uint? PlatinumBank => GetMember<IntType>("PlatinumBank");

        /// <summary>
        /// Gold in your bank
        /// </summary>
        public uint? GoldBank => GetMember<IntType>("GoldBank");

        /// <summary>
        /// Silver in your bank
        /// </summary>
        public uint? SilverBank => GetMember<IntType>("SilverBank");

        /// <summary>
        /// Copper in your bank
        /// </summary>
        public uint? CopperBank => GetMember<IntType>("CopperBank");

        /// <summary>
        /// AA exp as a raw number out of 330 (330=100%)
        /// </summary>
        public uint? AAExp => GetMember<IntType>("AAExp");

        /// <summary>
        /// Unused AA points
        /// </summary>
        public uint? AAPoints => GetMember<IntType>("AAPoints");

        /// <summary>
        /// Is auto attack turned on?
        /// </summary>
        public bool Combat => GetMember<BoolType>("Combat");

        /// <summary>
        /// Hit point regeneration from last tick
        /// </summary>
        public uint? HPRegen => GetMember<IntType>("HPRegen");

        /// <summary>
        /// Mana regeneration from last tick
        /// </summary>
        public uint? ManaRegen => GetMember<IntType>("ManaRegen");

        /// <summary>
        /// Endurance regen from the last tick
        /// </summary>
        public uint? EnduranceRegen => GetMember<IntType>("EnduranceRegen");

        /// <summary>
        /// True if in a group with a player or a mercenary
        /// </summary>
        public bool Grouped => GetMember<BoolType>("Grouped");

        /// <summary>
        /// This isn't really working as intended just yet as per MQ client comments
        /// </summary>
        [Obsolete]
        public string GroupList => GetMember<StringType>("GroupList");

        /// <summary>
        /// Am I the group leader?
        /// </summary>
        public bool AmIGroupLeader => GetMember<BoolType>("AmIGroupLeader");

        /// <summary>
        /// Maximum number of buffs you can have
        /// </summary>
        public uint? MaxBuffSlots => GetMember<IntType>("MaxBuffSlots");

        /// <summary>
        /// Number of free buff slots remaining
        /// </summary>
        public uint? FreeBuffSlots => GetMember<IntType>("FreeBuffSlots");

        /// <summary>
        /// The gem number that a spell name is memorized in, or the spell in a gem number.
        /// Cast to <see cref="uint"/> to get the value if using a <see cref="string"/> index / by name.
        /// </summary>
        private readonly IndexedMember<SpellType, int, IntType, string> _gem;

        /// <summary>
        /// Language skill by name or number.
        /// Cast to <see cref="uint"/> to get the value.
        /// </summary>
        private readonly IndexedMember<IntType, int, IntType, string> _languageSkill;

        /// <summary>
        /// Combat ability spell by number, or number by name.
        /// Cast to <see cref="uint"/> to get the value if using a <see cref="string"/> index / by name.
        /// </summary>
        private readonly IndexedMember<SpellType, int, IntType, string> _combatAbility;

        /// <summary>
        /// Combat ability reuse time remaining by name or number.
        /// Cast to <see cref="TimeSpan"/> to get the value. Value field is <see cref="MQ2VarPtr.Int"/>.
        /// </summary>
        private readonly IndexedMember<TicksType, int, TicksType, string> _combatAbilityTimer;

        /// <summary>
        /// Combat ability ready by name or number.
        /// </summary>
        private readonly IndexedMember<BoolType, int, BoolType, string> _combatAbilityReady;

        /// <summary>
        /// Returns a spell if melee discipline is active.
        /// </summary>
        public SpellType ActiveDisc => GetMember<SpellType>("ActiveDisc");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool Moving => GetMember<BoolType>("Moving");

        /// <summary>
        /// Hunger level
        /// </summary>
        public uint? Hunger => GetMember<IntType>("Hunger");

        /// <summary>
        /// Thirst level
        /// </summary>
        public uint? Thirst => GetMember<IntType>("Thirst");

        /// <summary>
        /// Alt ability reuse time remaining by name or number.
        /// Cast to <see cref="TimeSpan"/> to get the value. Value field is <see cref="MQ2VarPtr.Int64"/>.
        /// </summary>
        private readonly IndexedMember<TimeStampType, int, TimeStampType, string> _altAbilityTimer;

        /// <summary>
        /// Alt ability ready by name or number
        /// </summary>
        private readonly IndexedMember<BoolType, int, BoolType, string> _altAbilityReady;

        /// <summary>
        /// Returns an alt ability by name or number
        /// </summary>
        private readonly IndexedMember<AltAbilityType, int, AltAbilityType, string> _altAbility;

        /// <summary>
        /// Skill level by name or number.
        /// Cast to <see cref="uint"/> to get the value. Value field is <see cref="MQ2VarPtr.Dword"/>.
        /// </summary>
        private readonly IndexedMember<IntType, string, IntType, int> _skill;

        /// <summary>
        /// Skill base level by name or number.
        /// Cast to <see cref="uint"/> to get the value. Value field is <see cref="MQ2VarPtr.Dword"/>.
        /// </summary>
        private readonly IndexedMember<IntType, string, IntType, int> _skillBase;

        /// <summary>
        /// Skill cap by name or number.
        /// Cast to <see cref="uint"/> to get the value. Value field is <see cref="MQ2VarPtr.Dword"/>.
        /// </summary>
        private readonly IndexedMember<IntType, string, IntType, int> _skillCap;

        /// <summary>
        /// The doability button number that the skill name is on, or the skill name assigned to a doability button.
        /// If a string index was provided then cast to <see cref="uint"/> to get the value since the value field is <see cref="MQ2VarPtr.Dword"/>.
        /// If an int index was provided then the result is a <see cref="string"/>.
        /// </summary>
        private readonly IndexedStringMember<int, IntType, string> _ability;

        /// <summary>
        /// Ability with this name or on this button # ready?.
        /// </summary>
        private readonly IndexedMember<BoolType, int, BoolType, string> _abilityReady;

        /// <summary>
        /// Ranged attack ready?
        /// </summary>
        public bool RangedReady => GetMember<BoolType>("RangedReady");

        /// <summary>
        /// Don't use this. This is broken and should be fixed or removed.
        /// </summary>
        [Obsolete("This is broken and should be fixed or removed.")]
        public bool AltTimerReady => GetMember<BoolType>("AltTimerReady");

        /// <summary>
        /// Spell in your spellbook by slot number, or slot in your spellbook by spell name.
        /// If a string index was provided then cast to <see cref="uint"/> to get the value since the value field is <see cref="MQ2VarPtr.Dword"/>.
        /// </summary>
        private readonly IndexedMember<SpellType, int, IntType, string> _book;

        /// <summary>
        /// TODO: new member
        /// </summary>
        private readonly IndexedMember<SpellType, int, SpellType, string> _spell;

        /// <summary>
        /// Is an item ready to cast?
        /// </summary>
        private readonly IndexedMember<BoolType, int, BoolType, string> _itemReady;

        /// <summary>
        /// True if you're currently playing a bard song
        /// </summary>
        public bool BardSongPlaying => GetMember<BoolType>("BardSongPlaying");

        /// <summary>
        /// Indiciates if a spell is ready, by spell name or gem number
        /// </summary>
        private readonly IndexedMember<BoolType, int, BoolType, string> _spellReady;

        /// <summary>
        /// A buff on your pet by slot number, or a slot number by buff name.
        /// If a string index was provided then cast to <see cref="uint"/> to get the value since the value field is <see cref="MQ2VarPtr.Dword"/>.
        /// </summary>
        private readonly IndexedMember<SpellType, int, IntType, string> _petBuff;

        /// <summary>
        /// Stunned?
        /// </summary>
        public bool Stunned => GetMember<BoolType>("Stunned");

        /// <summary>
        /// Size of your largest free inventory slot (4 = Giant)
        /// </summary>
        public uint? LargestFreeInventory => GetMember<IntType>("LargestFreeInventory");

        /// <summary>
        /// Number of free inventory slots remaining
        /// </summary>
        public uint? FreeInventory => GetMember<IntType>("FreeInventory");

        /// <summary>
        /// Drunkenness level (0 - 200)
        /// </summary>
        public uint? Drunk => GetMember<IntType>("Drunk");

        /// <summary>
        /// Target of this spawn's target
        /// </summary>
        public SpawnType TargetOfTarget => GetMember<SpawnType>("TargetOfTarget");

        /// <summary>
        /// Current raid assist target (1-3)
        /// </summary>
        private readonly IndexedMember<SpawnType, int> _raidAssistTarget;

        /// <summary>
        /// Target of the group's main assist
        /// </summary>
        public SpawnType GroupAssistTarget => GetMember<SpawnType>("GroupAssistTarget");

        /// <summary>
        /// Current raid marked NPC (1-3)
        /// </summary>
        private readonly IndexedMember<SpawnType, int> _raidMarkNPC;

        /// <summary>
        /// Current group marked NPC (1 - 3)
        /// </summary>
        private readonly IndexedMember<SpawnType, int> _groupMarkNPC;

        /// <summary>
        /// Strength
        /// </summary>
        public uint? STR => GetMember<IntType>("STR");

        /// <summary>
        /// Stamina
        /// </summary>
        public uint? STA => GetMember<IntType>("STA");

        /// <summary>
        /// Agility
        /// </summary>
        public uint? AGI => GetMember<IntType>("AGI");

        /// <summary>
        /// Dexterity
        /// </summary>
        public uint? DEX => GetMember<IntType>("DEX");

        /// <summary>
        /// Wisdom
        /// </summary>
        public uint? WIS => GetMember<IntType>("WIS");

        /// <summary>
        /// Intelligence
        /// </summary>
        public uint? INT => GetMember<IntType>("INT");

        /// <summary>
        /// Charisma
        /// </summary>
        public uint? CHA => GetMember<IntType>("CHA");

        /// <summary>
        /// Luck
        /// </summary>
        public uint? LCK => GetMember<IntType>("LCK");

        /// <summary>
        /// Magic resist
        /// </summary>
        public uint? svMagic => GetMember<IntType>("svMagic");

        /// <summary>
        /// Fire resist
        /// </summary>
        public uint? svFire => GetMember<IntType>("svFire");

        /// <summary>
        /// Cold resist
        /// </summary>
        public uint? svCold => GetMember<IntType>("svCold");

        /// <summary>
        /// Poison resist
        /// </summary>
        public uint? svPoison => GetMember<IntType>("svPoison");

        /// <summary>
        /// Disease resist
        /// </summary>
        public uint? svDisease => GetMember<IntType>("svDisease");

        /// <summary>
        /// Current weight
        /// </summary>
        public uint? CurrentWeight => GetMember<IntType>("CurrentWeight");

        /// <summary>
        /// The number of points you have spent on AA abilities
        /// </summary>
        public uint? AAPointsSpent => GetMember<IntType>("AAPointsSpent");

        /// <summary>
        /// The total number of AA points you have
        /// </summary>
        public uint? AAPointsTotal => GetMember<IntType>("AAPointsTotal");

        /// <summary>
        /// Number of points that have been assigned to an ability
        /// </summary>
        public uint? AAPointsAssigned => GetMember<IntType>("AAPointsAssigned");

        /// <summary>
        /// Personal tribute currently active?
        /// </summary>
        public bool TributeActive => GetMember<BoolType>("TributeActive");

        /// <summary>
        /// Do I have auto-run turned on?
        /// </summary>
        public bool Running => GetMember<BoolType>("Running");

        /// <summary>
        /// Number of characters in group, including yourself. Returns null if not in a group
        /// </summary>
        public uint? GroupSize => GetMember<IntType>("GroupSize");

        /// <summary>
        /// Personal tribute timer.
        /// This is another version of ticks type. The inconsistencies makes it very hard to deal with casting it to TimeSpan.
        /// This one stores the value in <see cref="MQ2VarPtr.Dword"/> in milliseconds.
        /// </summary>
        public TicksType TributeTimer => GetMember<TicksType>("TributeTimer");

        /// <summary>
        /// Radiant Crystals (alt currency)
        /// </summary>
        public uint? RadiantCrystals => GetMember<IntType>("RadiantCrystals");

        /// <summary>
        /// Ebon Crystals (alt currency)
        /// </summary>
        public uint? EbonCrystals => GetMember<IntType>("EbonCrystals");

        /// <summary>
        /// Am I Shrouded?
        /// </summary>
        public bool Shrouded => GetMember<BoolType>("Shrouded");

        /// <summary>
        /// Is Autofire on?
        /// </summary>
        public bool AutoFire => GetMember<BoolType>("AutoFire");

        /// <summary>
        /// Language name by number, or number by name
        /// </summary>
        private readonly IndexedStringMember<int, IntType, string> _language;

        /// <summary>
        /// Aura by name or slot #
        /// </summary>
        private readonly IndexedMember<AuraType, string, AuraType, int> _aura;

        /// <summary>
        /// Level of Mark NPC of the current group leader (not your own ability level)
        /// </summary>
        public uint? LAMarkNPC => GetMember<IntType>("LAMarkNPC");

        /// <summary>
        /// Level of NPC Health of the current group leader (not your own ability level)
        /// </summary>
        public uint? LANPCHealth => GetMember<IntType>("LANPCHealth");

        /// <summary>
        /// Level of Delegate MA of the current group leader (not your own ability level)
        /// </summary>
        public uint? LADelegateMA => GetMember<IntType>("LADelegateMA");

        /// <summary>
        /// Level of Delegate Mark NPC of the current group leader (not your own ability level)
        /// </summary>
        public uint? LADelegateMarkNPC => GetMember<IntType>("LADelegateMarkNPC");

        /// <summary>
        /// Level of Inspect Buffs of the current group leader (not your own ability level)
        /// </summary>
        public uint? LAInspectBuffs => GetMember<IntType>("LAInspectBuffs");

        /// <summary>
        /// Level of Spell Awareness of the current group leader (not your own ability level)
        /// </summary>
        public uint? LASpellAwareness => GetMember<IntType>("LASpellAwareness");

        /// <summary>
        /// Level of Offense Enhancement of the current group leader (not your own ability level)
        /// </summary>
        public uint? LAOffenseEnhancement => GetMember<IntType>("LAOffenseEnhancement");

        /// <summary>
        /// Level of Mana Enhancement of the current group leader (not your own ability level)
        /// </summary>
        public uint? LAManaEnhancement => GetMember<IntType>("LAManaEnhancement");

        /// <summary>
        /// Level of Health Enhancement of the current group leader (not your own ability level)
        /// </summary>
        public uint? LAHealthEnhancement => GetMember<IntType>("LAHealthEnhancement");

        /// <summary>
        /// Level of Health Regen of the current group leader (not your own ability level)
        /// </summary>
        public uint? LAHealthRegen => GetMember<IntType>("LAHealthRegen");

        /// <summary>
        /// Level of Find Path PC of the current group leader (not your own ability level)
        /// </summary>
        public uint? LAFindPathPC => GetMember<IntType>("LAFindPathPC");

        /// <summary>
        /// Level of HoTT of the current group leader (not your own ability level)
        /// </summary>
        public uint? LAHoTT => GetMember<IntType>("LAHoTT");

        /// <summary>
        /// If Tribute is active, how much it is costing you every 10 minutes. Returns NULL if tribute is inactive.
        /// </summary>
        public int? ActiveFavorCost => GetMember<IntType>("ActiveFavorCost");

        /// <summary>
        /// Returns one of the following: COMBAT, DEBUFFED, COOLDOWN, ACTIVE, RESTING, UNKNOWN
        /// </summary>
        public CombatState? CombatState => GetMember<StringType>("CombatState");

        /// <summary>
        /// Corruption resist
        /// </summary>
        public uint? svCorruption => GetMember<IntType>("svCorruption");

        /// <summary>
        /// The average of your character's resists
        /// </summary>
        public uint? svPrismatic => GetMember<IntType>("svPrismatic");

        /// <summary>
        /// Your character's lowest resist
        /// </summary>
        public int? svChromatic => GetMember<IntType>("svChromatic");

        /// <summary>
        /// Doubloons (alt currency)
        /// </summary>
        public uint? Doubloons => GetMember<IntType>("Doubloons");

        /// <summary>
        /// Orux (alt currency)
        /// </summary>
        public uint? Orux => GetMember<IntType>("Orux");

        /// <summary>
        /// Phosphenes (alt currency)
        /// </summary>
        public uint? Phosphenes => GetMember<IntType>("Phosphenes");

        /// <summary>
        /// Phosphites (alt currency)
        /// </summary>
        public uint? Phosphites => GetMember<IntType>("Phosphites");

        /// <summary>
        /// Faycitum (alt currency)
        /// </summary>
        public uint? Faycites => GetMember<IntType>("Faycites");

        /// <summary>
        /// Chronobines on your character
        /// </summary>
        public uint? Chronobines => GetMember<IntType>("Chronobines");

        /// <summary>
        /// Commemorative Coins (alt currency)
        /// </summary>
        public uint? Commemoratives => GetMember<IntType>("Commemoratives");

        /// <summary>
        /// Nobles (alt currency)
        /// </summary>
        public uint? Nobles => GetMember<IntType>("Nobles");

        /// <summary>
        /// Fists of Bayle (alt currency)
        /// </summary>
        public uint? Fists => GetMember<IntType>("Fists");

        /// <summary>
        /// Energy Crystals (alt currency)
        /// </summary>
        public uint? EnergyCrystals => GetMember<IntType>("EnergyCrystals");

        /// <summary>
        /// Pieces of Eight (alt currency)
        /// </summary>
        public uint? PiecesofEight => GetMember<IntType>("PiecesofEight");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? SilverTokens => GetMember<IntType>("SilverTokens");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? GoldTokens => GetMember<IntType>("GoldTokens");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? McKenzie => GetMember<IntType>("McKenzie");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? Bayle => GetMember<IntType>("Bayle");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? Reclamation => GetMember<IntType>("Reclamation");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? Brellium => GetMember<IntType>("Brellium");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? Motes => GetMember<IntType>("Motes");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? RebellionChits => GetMember<IntType>("RebellionChits");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? DiamondCoins => GetMember<IntType>("DiamondCoins");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? BronzeFiats => GetMember<IntType>("BronzeFiats");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? Voucher => GetMember<IntType>("Voucher");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? VeliumShards => GetMember<IntType>("VeliumShards");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? CrystallizedFear => GetMember<IntType>("CrystallizedFear");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? ShadowStones => GetMember<IntType>("ShadowStones");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? DreadStones => GetMember<IntType>("DreadStones");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? MarksOfValor => GetMember<IntType>("MarksOfValor");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? MedalsOfHeroism => GetMember<IntType>("MedalsOfHeroism");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? RemnantOfTranquility => GetMember<IntType>("RemnantOfTranquility");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? BifurcatedCoin => GetMember<IntType>("BifurcatedCoin");

        /// <summary>
        /// ?
        /// </summary>
        public int? AdoptiveCoin => GetMember<IntType>("AdoptiveCoin");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? SathirsTradeGems => GetMember<IntType>("SathirsTradeGems");

        /// <summary>
        /// AncientSebilisianCoins
        /// </summary>
        public uint? AncientSebilisianCoins => GetMember<IntType>("AncientSebilisianCoins");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? BathezidTradeGems => GetMember<IntType>("BathezidTradeGems");

        /// <summary>
        /// AncientDraconicCoin
        /// </summary>
        public IntType AncientDraconicCoin => GetMember<IntType>("AncientDraconicCoin");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? FetterredIfritCoins => GetMember<IntType>("FetterredIfritCoins");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? EntwinedDjinnCoins => GetMember<IntType>("EntwinedDjinnCoins");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? CrystallizedLuck => GetMember<IntType>("CrystallizedLuck");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? FroststoneDucat => GetMember<IntType>("FroststoneDucat");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? WarlordsSymbol => GetMember<IntType>("WarlordsSymbol");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? OverseerTetradrachm => GetMember<IntType>("OverseerTetradrachm");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? WarforgedEmblem => GetMember<IntType>("WarforgedEmblem");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? RestlessMark => GetMember<IntType>("RestlessMark");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? ScarletMarks => GetMember<IntType>("ScarletMarks");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? MedalsOfConflict => GetMember<IntType>("MedalsOfConflict");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? ShadedSpecie => GetMember<IntType>("ShadedSpecie");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? SpiritualMedallions => GetMember<IntType>("SpiritualMedallions");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? LoyaltyTokens => GetMember<IntType>("LoyaltyTokens");

        /// <summary>
        /// Fellowship character is in
        /// </summary>
        public FellowshipType Fellowship => GetMember<FellowshipType>("Fellowship");

        /// <summary>
        /// Ticks remaining before able to rest.
        /// Cast to <see cref="TimeSpan"/> to get the value. Value field is <see cref="MQ2VarPtr.Dword"/>.
        /// </summary>
        public TicksType Downtime => GetMember<TicksType>("Downtime");

        /// <summary>
        /// Damage absorption remaining (eg. from Rune-type spells).
        /// Returns combined number of spell and damage "absorbment"
		/// should probably split these into spell vs melee.
        /// </summary>
        public long? Dar => GetMember<Int64Type>("Dar");

        /// <summary>
        /// Total number of counters on you
        /// </summary>
        public long? TotalCounters => GetMember<Int64Type>("TotalCounters");

        /// <summary>
        /// Total number of disease counters
        /// </summary>
        public long? CountersDisease => GetMember<Int64Type>("CountersDisease");

        /// <summary>
        /// Total number of poison counters
        /// </summary>
        public long? CountersPoison => GetMember<Int64Type>("CountersPoison");

        /// <summary>
        /// Total number of curse counters
        /// </summary>
        public long? CountersCurse => GetMember<Int64Type>("CountersCurse");

        /// <summary>
        /// Total number of corruption counters
        /// </summary>
        public long? CountersCorruption => GetMember<Int64Type>("CountersCorruption");

        /// <summary>
        /// The state of your Mercenary, ACTIVE, SUSPENDED, or UNKNOWN (If it's dead). Returns NULL if you do not have a Mercenary.
        /// </summary>
        public MercenaryType Mercenary => GetMember<MercenaryType>("Mercenary");

        /// <summary>
        /// Number of slots available in your XTarget window
        /// </summary>
        public uint? XTargetSlots => GetMember<IntType>("XTargetSlots");

        /// <summary>
        /// Number of mobs on your XTarget, excluding your current target, that have less than the supplied % of aggro on you.
        /// Cast to <see cref="uint"/> to get the value. Value field is <see cref="MQ2VarPtr.Dword"/>.
        /// </summary>
        private readonly IndexedMember<IntType, int> _xTAggroCount;

        /// <summary>
        /// Number of spawns in auto hater slots in your XTarget
        /// </summary>
        public uint? XTHaterCount => GetMember<IntType>("XTHaterCount");

        /// <summary>
        /// Returns a spawn from your XTarget by index (1 - 13) or name.
        /// If no index was provided then it returns the count. Cast to <see cref="uint"/> to get the value since the value field is <see cref="MQ2VarPtr.Dword"/> when no index is provided.
        /// </summary>
        private readonly IndexedMember<XTargetType, int, XTargetType, string> _xTarget;

        /// <summary>
        /// Total Combined Haste (worn and spell) as shown in Inventory Window stats
        /// </summary>
        public uint? Haste => GetMember<IntType>("Haste");

        /// <summary>
        /// Returns the total amount of a SPA your character has.
        /// Cast to <see cref="uint"/> to get the value. The value field is <see cref="MQ2VarPtr.Dword"/>.
        /// </summary>
        private readonly IndexedMember<IntType, int> _sPA;

        /// <summary>
        /// Current active mercenary stance as a string, default is NULL.
        /// </summary>
        public string MercenaryStance => GetMember<StringType>("MercenaryStance");

        /// <summary>
        /// Recast time remaining on a spell gem by number or spell name.
        /// Cast to <see cref="TimeSpan"/> to get the value. Value field is <see cref="MQ2VarPtr.Int64"/>.
        /// </summary>
        private readonly IndexedMember<TimeStampType, int, TimeStampType, string> _gemTimer;

        /// <summary>
        /// Returns TRUE/FALSE if you have that expansion by # or name
        /// </summary>
        private readonly IndexedMember<BoolType, int, BoolType, string> _haveExpansion;

        /// <summary>
        /// Your aggro percentage
        /// </summary>
        public uint? PctAggro => GetMember<IntType>("PctAggro");

        /// <summary>
        /// Secondary aggro as a percentage
        /// </summary>
        public uint? SecondaryPctAggro => GetMember<IntType>("SecondaryPctAggro");

        /// <summary>
        /// Spawn that has secondary aggro
        /// </summary>
        public SpawnType SecondaryAggroPlayer => GetMember<SpawnType>("SecondaryAggroPlayer");

        /// <summary>
        /// Spawn info for aggro lock player
        /// </summary>
        public SpawnType AggroLock => GetMember<SpawnType>("AggroLock");

        /// <summary>
        /// Zone you are bound in
        /// </summary>
        public ZoneType ZoneBound => GetMember<ZoneType>("ZoneBound");

        /// <summary>
        /// X location of your bind point
        /// </summary>
        public float? ZoneBoundX => GetMember<FloatType>("ZoneBoundX");

        /// <summary>
        /// Y location of your bind point
        /// </summary>
        public float? ZoneBoundY => GetMember<FloatType>("ZoneBoundY");

        /// <summary>
        /// Z location of your bind point
        /// </summary>
        public float? ZoneBoundZ => GetMember<FloatType>("ZoneBoundZ");

        /// <summary>
        /// Current mercenary AA experience as a oercentage
        /// </summary>
        public float? PctMercAAExp => GetMember<FloatType>("PctMercAAExp");

        /// <summary>
        /// Mercenary AA experience, out of 1000
        /// </summary>
        public long? MercAAExp => GetMember<Int64Type>("MercAAExp");

        /// <summary>
        /// Krono on your character
        /// </summary>
        public uint? Krono => GetMember<IntType>("Krono");

        /// <summary>
        /// Subscription type GOLD, FREE, (Silver?)
        /// TODO: Source states - Fix this. Its a struct not an int*
        /// </summary>
        [Obsolete("TODO: Fix this. Its a struct not an int*")]
        public string Subscription => GetMember<StringType>("Subscription");

        /// <summary>
        /// Quantity of an alt currency by name or number.
        /// Cast to <see cref="uint"/> to get the value. The value field is <see cref="MQ2VarPtr.Dword"/>.
        /// </summary>
        private readonly IndexedMember<IntType, int, IntType, string> _altCurrency;

        /// <summary>
        /// Debuff with a slow SPA
        /// </summary>
        public BuffType Slowed => GetMember<BuffType>("Slowed");

        /// <summary>
        /// Buff with a root SPA
        /// </summary>
        public BuffType Rooted => GetMember<BuffType>("Rooted");

        /// <summary>
        /// Debuff from the Mez line
        /// </summary>
        public BuffType Mezzed => GetMember<BuffType>("Mezzed");

        /// <summary>
        /// Debuff from the Cripple line
        /// </summary>
        public BuffType Crippled => GetMember<BuffType>("Crippled");

        /// <summary>
        /// Debuff from the Malo line
        /// </summary>
        public BuffType Maloed => GetMember<BuffType>("Maloed");

        /// <summary>
        /// Debuff from the Tash line
        /// </summary>
        public BuffType Tashed => GetMember<BuffType>("Tashed");

        /// <summary>
        /// Debuff with a snare SPA
        /// </summary>
        public BuffType Snared => GetMember<BuffType>("Snared");

        /// <summary>
        /// Buff from the Haste line
        /// </summary>
        public BuffType Hasted => GetMember<BuffType>("Hasted");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public TimeSpan? LastZoned => GetMember<TimeStampType>("LastZoned");

        /// <summary>
        /// Am I zoning?
        /// </summary>
        public bool Zoning => GetMember<BoolType>("Zoning");

        /// <summary>
        /// The buff on you, if any, that is increasing your damage shield
        /// </summary>
        public BuffType DSed => GetMember<BuffType>("DSed");

        /// <summary>
        /// Buff with a reverse damage shield SPA
        /// </summary>
        public BuffType RevDSed => GetMember<BuffType>("RevDSed");

        /// <summary>
        /// Debuff with a charm SPA
        /// </summary>
        public BuffType Charmed => GetMember<BuffType>("Charmed");

        /// <summary>
        /// Buff from the Aegolism line
        /// </summary>
        public BuffType Aego => GetMember<BuffType>("Aego");

        /// <summary>
        /// Buff from the Skin line
        /// </summary>
        public BuffType Skin => GetMember<BuffType>("Skin");

        /// <summary>
        /// Buff from the Focus line
        /// </summary>
        public BuffType Focus => GetMember<BuffType>("Focus");

        /// <summary>
        /// Buff from the Regen line
        /// </summary>
        public BuffType Regen => GetMember<BuffType>("Regen");

        /// <summary>
        /// The buff on you, if any, that is increasing your disease counter
        /// </summary>
        public BuffType Diseased => GetMember<BuffType>("Diseased");

        /// <summary>
        /// The buff on you, if any, that is increasing your poison counter
        /// </summary>
        public BuffType Poisoned => GetMember<BuffType>("Poisoned");

        /// <summary>
        /// The buff on you, if any, that is increasing your cursed counter
        /// </summary>
        public BuffType Cursed => GetMember<BuffType>("Cursed");

        /// <summary>
        /// The buff on you, if any, that is increasing your corruption counter
        /// </summary>
        public BuffType Corrupted => GetMember<BuffType>("Corrupted");
        /// <summary>
        /// Buff from the Symbol line
        /// </summary>
        public BuffType Symbol => GetMember<BuffType>("Symbol");

        /// <summary>
        /// Buff from the Clarity line
        /// </summary>
        public BuffType Clarity => GetMember<BuffType>("Clarity");

        /// <summary>
        /// Buff from the Pred line
        /// </summary>
        public BuffType Pred => GetMember<BuffType>("Pred");

        /// <summary>
        /// Buff from the Strength line
        /// </summary>
        public BuffType Strength => GetMember<BuffType>("Strength");

        /// <summary>
        /// Buff from the Brells line
        /// </summary>
        public BuffType Brells => GetMember<BuffType>("Brells");

        /// <summary>
        /// Buff from the Spiritual Vivacity line
        /// </summary>
        public BuffType SV => GetMember<BuffType>("SV");

        /// <summary>
        /// Buff from the Spiritual Enlightenment line
        /// </summary>
        public BuffType SE => GetMember<BuffType>("SE");

        /// <summary>
        /// Buff from the Hybrid HP line TODO What is this
        /// </summary>
        public BuffType HybridHP => GetMember<BuffType>("HybridHP");

        /// <summary>
        /// Buff with a growth SPA
        /// </summary>
        public BuffType Growth => GetMember<BuffType>("Growth");

        /// <summary>
        /// Buff from the Shining line
        /// </summary>
        public BuffType Shining => GetMember<BuffType>("Shining");

        /// <summary>
        /// Are you in an instanced zone?
        /// </summary>
        public bool InInstance => GetMember<BoolType>("InInstance");

        /// <summary>
        /// Instance you are in
        /// </summary>
        public int? Instance => GetMember<IntType>("Instance");

        /// <summary>
        /// TODO: new member
        /// </summary>
        private readonly IndexedStringMember<int, IntType, string> _mercListInfo;

        /// <summary>
        /// Using advanced looting?
        /// </summary>
        public bool UseAdvancedLooting => GetMember<BoolType>("UseAdvancedLooting");

        /// <summary>
        /// Returns TRUE if you have a spell in cooldown and FALSE when not.
        /// </summary>
        public bool SpellInCooldown => GetMember<BoolType>("SpellInCooldown");

        /// <summary>
        /// Returns true/false if the assist is complete
        /// </summary>
        public bool AssistComplete => GetMember<BoolType>("AssistComplete");

        /// <summary>
        /// Returns the amount of spell gems your toon has
        /// </summary>
        public uint? NumGems => GetMember<IntType>("NumGems");

        /// <summary>
        /// ID number of your guild
        /// </summary>
        public long? GuildID => GetMember<Int64Type>("GuildID");

        /// <summary>
        /// Bit mask of expansions owned
        /// </summary>
        public uint? ExpansionFlags => GetMember<IntType>("ExpansionFlags");

        /// <summary>
        /// Bind location, valid indexes are 0 - 4
        /// </summary>
        private readonly IndexedMember<WorldLocationType, int> _boundLocation;

        /// <summary>
        /// Autoskill by number
        /// </summary>
        private readonly IndexedMember<SkillType, int> _autoSkill;

        /// <summary>
        /// Base strength
        /// </summary>
        public uint? BaseSTR => GetMember<IntType>("BaseSTR");

        /// <summary>
        /// Base stamina
        /// </summary>
        public uint? BaseSTA => GetMember<IntType>("BaseSTA");

        /// <summary>
        /// Base agility
        /// </summary>
        public uint? BaseAGI => GetMember<IntType>("BaseAGI");

        /// <summary>
        /// Base dexterity
        /// </summary>
        public uint? BaseDEX => GetMember<IntType>("BaseDEX");

        /// <summary>
        /// Base wisdom
        /// </summary>
        public uint? BaseWIS => GetMember<IntType>("BaseWIS");

        /// <summary>
        /// Base intelligence
        /// </summary>
        public uint? BaseINT => GetMember<IntType>("BaseINT");

        /// <summary>
        /// Base charisma
        /// </summary>
        public uint? BaseCHA => GetMember<IntType>("BaseCHA");

        /// <summary>
        /// First beneficial buff on character
        /// </summary>
        public BuffType Beneficial => GetMember<BuffType>("Beneficial");

        /// <summary>
        /// Krono on your cursor
        /// </summary>
        public uint? CursorKrono => GetMember<IntType>("CursorKrono");

        /// <summary>
        /// Number of mercenary AA points available to spend
        /// </summary>
        public uint? MercAAPoints => GetMember<IntType>("MercAAPoints");

        /// <summary>
        /// Number of mercenary AA points spent
        /// </summary>
        public uint? MercAAPointsSpent => GetMember<IntType>("MercAAPointsSpent");

        /// <summary>
        /// Bandolier set by slot number (1 - 20) or name
        /// </summary>
        private readonly IndexedMember<BandolierType, string, BandolierType, int> _bandolier;

        /// <summary>
        /// Fear debuff if the target has one
        /// </summary>
        public BuffType Feared => GetMember<BuffType>("Feared");

        /// <summary>
        /// Silence debuff if the target has one
        /// </summary>
        public BuffType Silenced => GetMember<BuffType>("Silenced");

        /// <summary>
        /// Invulnerability buff if the target has one
        /// </summary>
        public BuffType Invulnerable => GetMember<BuffType>("Invulnerable");

        /// <summary>
        /// DoT debuff if the target has one
        /// </summary>
        public BuffType Dotted => GetMember<BuffType>("Dotted");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? ParcelStatus => GetMember<IntType>("ParcelStatus");

        /// <summary>
        /// Can you use a mount here?
        /// </summary>
        public BoolType CanMount => GetMember<BoolType>("CanMount");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? SpellRankCap => GetMember<IntType>("SpellRankCap");

        /// <summary>
        /// Ability with this name or on this button # ready?
        /// Cast to <see cref="TimeSpan"/> to get the value. Value field is <see cref="MQ2VarPtr.Int64"/>.
        /// </summary>
        private readonly IndexedMember<TimeStampType, int, TimeStampType, string> _abilityTimer;

        /// <summary>
        /// TODO: new member
        /// </summary>
        public TimeSpan? CastTimeLeft => GetMember<TimeStampType>("CastTimeLeft");

        /// <summary>
        /// Maximum level, inclusive
        /// </summary>
        public int? MaxLevel => GetMember<IntType>("MaxLevel");

        /// <summary>
        /// AirSupply
        /// </summary>
        public int? AirSupply => GetMember<IntType>("AirSupply");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? MaxAirSupply => GetMember<IntType>("MaxAirSupply");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? PctAirSupply => GetMember<IntType>("PctAirSupply");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? NumBagSlots => GetMember<IntType>("NumBagSlots");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string Inviter => GetMember<StringType>("Inviter");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool Invited => GetMember<BoolType>("Invited");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? IsBerserk => GetMember<IntType>("IsBerserk");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public float? GroupLeaderExp => GetMember<FloatType>("GroupLeaderExp");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? GroupLeaderPoints => GetMember<IntType>("GroupLeaderPoints");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public float? PctGroupLeaderExp => GetMember<FloatType>("PctGroupLeaderExp");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public float? RaidLeaderExp => GetMember<FloatType>("RaidLeaderExp");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? RaidLeaderPoints => GetMember<IntType>("RaidLeaderPoints");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public float? PctRaidLeaderExp => GetMember<FloatType>("PctRaidLeaderExp");

        /// <summary>
        /// Equivalent of the command /stand on
        /// </summary>
        public void Stand() => GetMember<MQ2DataType>("Stand");

        /// <summary>
        /// Equivalent of the command /sit on
        /// </summary>
        public void Sit() => GetMember<MQ2DataType>("Sit");

        /// <summary>
        /// Equivalent of the command /dismount
        /// </summary>
        public void Dismount() => GetMember<MQ2DataType>("Dismount");

        /// <summary>
        /// Equivalent of the command /stopcast
        /// </summary>
        public void StopCast() => GetMember<MQ2DataType>("StopCast");
    }
}