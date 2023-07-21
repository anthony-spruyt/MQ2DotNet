﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// This is the type used for spell information.
    /// Last Verified: 2023-07-13
    /// https://docs.macroquest.org/reference/data-types/datatype-spell/
    /// </summary>
    [MQ2Type("spell")]
    public class SpellType : MQ2DataType
    {
        public const int MAX_RESTRICTIONS = 100;

        internal SpellType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _stacks = new IndexedMember<BoolType, string>(this, "Stacks");
            _stacksPet = new IndexedMember<BoolType, string>(this, "StacksPet");
            _stacksWith = new IndexedMember<BoolType, string>(this, "StacksWith");
            _stacksSpawn = new IndexedMember<BoolType, int>(this, "StacksSpawn");
            _restrictions = new IndexedStringMember<int>(this, "Restrictions");
            _base = new IndexedMember<Int64Type, int>(this, "Base");
            _base2 = new IndexedMember<Int64Type, int>(this, "Base2");
            _max = new IndexedMember<Int64Type, int>(this, "Max");
            _calc = new IndexedMember<IntType, int>(this, "Calc");
            _attrib = new IndexedMember<IntType, int>(this, "Attrib");
            _reagentID = new IndexedMember<IntType, int>(this, "ReagentID");
            _noExpendReagentID = new IndexedMember<IntType, int>(this, "NoExpendReagentID");
            _reagentCount = new IndexedMember<IntType, int>(this, "ReagentCount");
            _hasSPA = new IndexedMember<BoolType, int>(this, "HasSPA");
            _trigger = new IndexedMember<SpellType, int>(this, "Trigger");
            _link = new IndexedStringMember<string>(this, "Link");
        }

        /// <summary>
        /// Opens the spell display window for this spell.
        /// </summary>
        /// <param name="spellNameOverride"></param>
        public void Inspect(string spellNameOverride) => GetMember<MQ2DataType>("Inspect", spellNameOverride);

        /// <summary>
        /// Opens the spell display window for this spell.
        /// </summary>
        public void Inspect() => GetMember<MQ2DataType>("Inspect");

        /// <summary>
        /// Spell ID.
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");
        
        /// <summary>
        /// Spell Name
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Level
        /// </summary>
        public int? Level => GetMember<IntType>("Level");
        
        /// <summary>
        /// Mana cost (unadjusted).
        /// </summary>
        public int? Mana => GetMember<IntType>("Mana");
        
        /// <summary>
        /// Resist adjustment
        /// </summary>
        public int? ResistAdj => GetMember<IntType>("ResistAdj");
        
        /// <summary>
        /// Maximum range to target (use <see cref="AERange"/> for AE and group spells)
        /// </summary>
        public float? Range => GetMember<FloatType>("Range");
        
        /// <summary>
        /// AE range (group spells use this for their range).
        /// </summary>
        public float? AERange => GetMember<FloatType>("AERange");
        
        /// <summary>
        /// Push back amount
        /// </summary>
        public float? PushBack => GetMember<FloatType>("PushBack");
        
        /// <summary>
        /// Cast time (unadjusted).
        /// </summary>
        public TimeSpan? CastTime => GetMember<TimeStampType>("CastTime");
        
        /// <summary>
        /// Time to recover after fizzle.
        /// </summary>
        public TimeSpan? RecoveryTime => FizzleTime;
        
        /// <summary>
        /// Time to recover after fizzle
        /// </summary>
        public TimeSpan? FizzleTime => GetMember<TimeStampType>("FizzleTime");
        
        /// <summary>
        /// Time to recast after successful cast
        /// </summary>
        public TimeSpan? RecastTime => GetMember<TimeStampType>("RecastTime");

        /// <summary>
        /// ResistType will be one of the following:
        /// - Chromatic
        /// - Corruption
        /// - Cold
        /// - Disease
        /// - Fire
        /// - Magic
        /// - Poison
        /// - Unresistable
        /// - Prismatic
        /// - Unknown
        /// TODO: convert to enum
        /// </summary>
        public string ResistType => GetMember<StringType>("ResistType");

        /// <summary>
        /// "Beneficial(Group)", "Beneficial", "Detrimental" or "Unknown"
        /// TODO: convert to enum
        /// </summary>
        public string Type => GetMember<StringType>("SpellType");

        /// <summary>
        /// Target type:
        /// {Target_AE_No_Players_Pets}
        /// {Single Friendly (or Target's Target}
        /// {Pet Owner}
        /// {Target of Target}
        /// {Free Target}
        /// {Beam}
        /// {Single in Group}
        /// {Directional AE}
        /// {Group v2}
        /// {AE PC v2}
        /// {No Pets}
        /// {Pet2}
        /// {Caster PB NPC}
        /// {Caster PB PC}
        /// {Special Muramites}
        /// {Chest}
        /// {Hatelist2}
        /// {Hatelist}
        /// {AE Summoned}
        /// {AE Undead}
        /// {Targeted AE Tap}
        /// {Uber Dragons}
        /// {Uber Giants}
        /// {Plant}
        /// {Corpse}
        /// {Pet}
        /// {LifeTap}
        /// {Summoned}
        /// {Undead}
        /// {Animal}
        /// {Targeted AE}
        /// {Self}
        /// {Single}
        /// {PB AE}
        /// {Group v1}
        /// {AE PC v1}
        /// {Line of Sight}
        /// {None}
        /// {Unknown}
        /// TODO: convert to enum
        /// </summary>
        public string TargetType => GetMember<StringType>("TargetType");

        /// <summary>
        /// Skill will be one of the following:
        /// - Abjuration
        /// - Alteration
        /// - Conjuration
        /// - Divination
        /// - Evocation
        /// TODO: map to enum
        /// </summary>
        public string Skill => GetMember<StringType>("Skill");
        
        /// <summary>
        /// Adjusted cast time.
        /// </summary>
        public TimeSpan? MyCastTime => GetMember<TimeStampType>("MyCastTime");

        /// <summary>
        /// Duration of the spell (if any), MQ2 version
        /// </summary>
        public TimeSpan? MyDuration => GetMember<TicksType>("MyDuration");

        /// <summary>
        /// Duration of the spell (if any), MQ2 version
        /// </summary>
        public TimeSpan? Duration => GetMember<TicksType>("Duration");
        
        /// <summary>
        /// Duration of the spell (if any), EQ version
        /// </summary>
        public TimeSpan? EQSpellDuration => Duration;
        
        /// <summary>
        /// Message when you cast the spell
        /// </summary>
        public string CastByMe => GetMember<StringType>("CastByMe");
        
        /// <summary>
        /// Message when someone else casts the spell.
        /// </summary>
        public string CastByOther => GetMember<StringType>("CastByOther");

        /// <summary>
        /// Message when cast on yourself.
        /// </summary>
        public string CastOnYou => GetMember<StringType>("CastOnYou");

        /// <summary>
        /// Message when cast on others.
        /// </summary>
        public string CastOnAnother => GetMember<StringType>("CastOnAnother");

        /// <summary>
        /// The "wear off" message.
        /// </summary>
        public string WearOff => GetMember<StringType>("WearOff");

        /// <summary>
        /// The resist counter. Will be one of "Disease", "Poison", "Curse" or "Corruption".
        /// TODO: map to enum
        /// </summary>
        public string CounterType => GetMember<StringType>("CounterType");
        
        /// <summary>
        /// The number of counters that the spell adds.
        /// </summary>
        public long? CounterNumber => GetMember<Int64Type>("CounterNumber");

        /// <summary>
        /// Does the selected spell stack with your current buffs (duration is in ticks)?
        /// Stacks[ duration ]
        /// </summary>
        private readonly IndexedMember<BoolType, string> _stacks;

        /// <summary>
        /// Does the selected spell stack with your current buffs (duration is in ticks)?
        /// Stacks[ duration ]
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public bool Stacks(TimeSpan duration) => _stacks[duration.Ticks.ToString()];

        /// <summary>
        /// Does the selected spell stack with your current buffs?
        /// </summary>
        /// <returns></returns>
        public bool Stacks() => _stacks["0"];

        /// <summary>
        /// This extra member is exactly like stacks but without a duration check. The duration
        /// check on stacks implies that we _don't_ want overwrites, but that means that any
        /// spell that will land but overwrites something will fail that check. So provide the
        /// raw "this will land on your target" check as well
        /// </summary>
        public bool WillLand => (int)GetMember<IntType>("WillLand") > 0;

        /// <summary>
        /// Does the selected spell stack with your pet's current buffs (duration is in ticks)?
        /// StacksPet[ duration ]
        /// </summary>
        private readonly IndexedMember<BoolType, string> _stacksPet;

        /// <summary>
        /// Does the selected spell stack with your pet's current buffs (duration is in ticks)?
        /// StacksPet[ duration ]
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public bool StacksPet(TimeSpan duration) => _stacksPet[duration.Ticks.ToString()];

        /// <summary>
        /// Does the selected spell stack with your pet's current buffs?
        /// </summary>
        /// <returns></returns>
        public bool StacksPet() => _stacksPet["0"];

        /// <summary>
        /// This is the same as <see cref="WillLand"/>, but for pets
        /// </summary>
        public bool WillLandPet => (int)GetMember<IntType>("WillLandPet") > 0;

        /// <summary>
        /// Does this spell stack with another spell?
        /// Also known as
        /// - will stack
        /// - new stack with
        /// </summary>
        private readonly IndexedMember<BoolType, string> _stacksWith;

        /// <summary>
        /// Does the selected spell stack with the specific spell name? DOES NOT work with AAs.
        /// Also known as
        /// - will stack
        /// - new stack with
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns></returns>
        public bool StacksWith(string spellName) => _stacksWith[spellName];

        /// <summary>
        /// Uses cached buffs to see if the spell will stack on a spawn by Id. Not recommended.
        /// </summary>
        private readonly IndexedMember<BoolType, int> _stacksSpawn;

        /// <summary>
        /// Uses cached buffs to see if the spell will stack on a spawn by Id. Not recommended.
        /// </summary>
        /// <param name="spawnId"></param>
        /// <returns></returns>
        public bool StacksSpawn(int spawnId) => _stacksSpawn[spawnId];

        /// <summary>
        /// Does the selected spell stack with your target's current buffs?
        /// </summary>
        public bool StacksTarget => GetMember<BoolType>("StacksTarget");

        /// <summary>
        /// Adjusted spell range, including focus effects, etc.
        /// </summary>
        public float? MyRange => GetMember<FloatType>("MyRange");

        /// <summary>
        /// Endurance cost of the spell
        /// </summary>
        public uint? EnduranceCost => GetMember<IntType>("EnduranceCost");

        /// <summary>
        /// Max level the spell can affect
        /// </summary>
        public long? MaxLevel => GetMember<Int64Type>("MaxLevel");

        /// <summary>
        /// Name of the category the spell belongs to (e.g. HP Buffs, Direct Damage, Heals, Unknown).
        /// TODO: map to an enum
        /// </summary>
        public string Category => GetMember<StringType>("Category");

        /// <summary>
        /// Numeric ID of the category this spell belongs to.
        /// </summary>
        public uint? CategoryID => GetMember<IntType>("CategoryID");

        /// <summary>
        /// Name of the subcategory this spell belongs to (e.g. "Shielding").
        /// TODO: map to enum
        /// </summary>
        public string Subcategory => GetMember<StringType>("Subcategory");

        /// <summary>
        /// Numeric Id of the subcategory this spell belongs to.
        /// </summary>
        public uint? SubcategoryID => GetMember<IntType>("SubcategoryID");

        /// <summary>
        /// Text of the nth restriction (1 based) on the spell
        /// </summary>
        private readonly IndexedStringMember<int> _restrictions;

        /// <summary>
        /// Text of the nth restriction (1 based) on the spell
        /// </summary>
        /// <param name="index">The 1 based index.</param>
        /// <returns></returns>
        public string GetRestriction(int index) => _restrictions[index];

        private bool TryGetRestriction(int index, out string restriction)
        {
            const string stop = "Unknown";
            restriction = GetRestriction(index);

            return !string.IsNullOrEmpty(restriction) && string.Compare(restriction, stop, true) != 0;
        }

        /// <summary>
        /// All spell restrictions.
        /// </summary>
        public IEnumerable<string> Restrictions
        {
            get
            {
                var index = 1;

                while (TryGetRestriction(index, out string restriction) && index < MAX_RESTRICTIONS)
                {
                    index++;

                    yield return restriction;
                }
            }
        }

        /// <summary>
        /// Base value of the nth spell effect slot, 1 based
        /// e.g. for a nuke that says Slot 1: Decrease HP by 1000
        /// Base[1] = -1000
        /// </summary>
        private readonly IndexedMember<Int64Type, int> _base;

        /// <summary>
        /// Base value of the nth spell effect slot, 1 based
        /// e.g. for a nuke that says Slot 1: Decrease HP by 1000
        /// 1 = -1000
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public long? GetBase(int slot) => _base[slot];

        /// <summary>
        /// Base2 value of the nth spell effect slot, 1 based
        /// </summary>
        private readonly IndexedMember<Int64Type, int> _base2;

        /// <summary>
        /// Base2 value of the nth spell effect slot, 1 based
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public long? GetBase2(int slot) => _base2[slot];

        /// <summary>
        /// Max value of the nth spell effect slot, 1 based
        /// </summary>
        private readonly IndexedMember<Int64Type, int> _max;

        /// <summary>
        /// Max value of the nth spell effect slot, 1 based
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public long? GetMax(int slot) => _max[slot];

        /// <summary>
        /// Calc value of the nth spell effect slot, 1 based
        /// </summary>
        private readonly IndexedMember<IntType, int> _calc;

        /// <summary>
        /// Calc value of the nth spell effect slot, 1 based
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public int? Calc(int slot) => _calc[slot];

        /// <summary>
        /// Attrib value of the nth spell effect slot, 1 based
        /// </summary>
        private readonly IndexedMember<IntType, int> _attrib;

        /// <summary>
        /// Attrib value of the nth spell effect slot, 1 based
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public int? GetAttrib(int slot) => _attrib[slot];


        /// <summary>
        /// TODO: What is SpellType.CalcIndex
        /// </summary>
        public int? CalcIndex => GetMember<IntType>("CalcIndex");
        
        /// <summary>
        /// Number of spell effect slots this spell has
        /// </summary>
        public int? NumEffects => GetMember<IntType>("NumEffects");
        
        /// <summary>
        /// TODO: What is SpellType.AutoCast
        /// </summary>
        public uint? AutoCast => GetMember<IntType>("AutoCast");
        
        /// <summary>
        /// TODO: What is SpellType.Extra
        /// </summary>
        public string Extra => GetMember<StringType>("Extra");
        
        /// <summary>
        /// Shared recast timer number for this spell
        /// </summary>
        public uint? RecastTimerID => GetMember<IntType>("RecastTimerID");

        /// <summary>
        /// Item ID of the nth required reagent (valid indexes are 1 - 4)
        /// </summary>
        private readonly IndexedMember<IntType, int> _reagentID;

        /// <summary>
        /// Item ID of the nth required reagent (valid indexes are 1 - 4)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint? GetReagentID(int index) => _reagentID[index];

        /// <summary>
        /// Item ID of a non-expended reagent. 1 based index
        /// </summary>
        private readonly IndexedMember<IntType, int> _noExpendReagentID;

        /// <summary>
        /// Item ID of a non-expended reagent. 1 based index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint? GetNoExpendReagentID(int index) => _noExpendReagentID[index];

        /// <summary>
        /// Quantity of the nth required reagent (valid indexes are 1 - 4)
        /// </summary>
        private readonly IndexedMember<IntType, int> _reagentCount;

        /// <summary>
        /// Quantity of the nth required reagent (valid indexes are 1 - 4)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint? GetReagentCount(int index) => _reagentCount[index];

        /// <summary>
        /// Required time of day to cast, 0 = any, 1 = day only, 2 = night only
        /// TODO: map to enum
        /// </summary>
        public uint? TimeOfDay => GetMember<IntType>("TimeOfDay");

        /// <summary>
        /// Which buff window the spell appears in, 0 = long, 1 = short
        /// TODO: map to enum
        /// </summary>
        public uint? DurationWindow => GetMember<IntType>("DurationWindow");

        /// <summary>
        /// Spell can be MGBed
        /// </summary>
        public bool CanMGB => GetMember<BoolType>("CanMGB");

        /// <summary>
        /// is this spell a skill?
        /// </summary>
        public bool IsSkill => GetMember<BoolType>("IsSkill");

        /// <summary>
        /// Whether a spell can be deleted from the spell book.
        /// </summary>
        public bool Deletable => GetMember<BoolType>("Deletable");

        /// <summary>
        /// Icon ID in the spell book
        /// </summary>
        public uint? BookIcon => GetMember<IntType>("BookIcon");

        /// <summary>
        /// Numeric ID of the Icon used to represent the spell.
        /// </summary>
        public uint? SpellIcon => BookIcon;

        /// <summary>
        /// Icon number of the spell. Example ${Spell[blah].GemIcon}.
        /// </summary>
        public uint? GemIcon => BookIcon;

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? ActorTagId => GetMember<IntType>("ActorTagId");

        /// <summary>
        /// Spell effect description from the spell window
        /// </summary>
        public string Description => GetMember<StringType>("Description");

        /// <summary>
        /// Name of the spell, without rank
        /// </summary>
        public string BaseName => GetMember<StringType>("BaseName");

        /// <summary>
        /// Returns either 1, 2 or 3 for spells and 4-30 for clickys and potions.
        /// 1 = Original
        /// 2 = Rk. II
        /// 3 = Rk. III
        /// ?? = didn't have a rank, lets see if we can get it from the name
        /// </summary>
        public uint? Rank => GetMember<IntType>("Rank");

        /// <summary>
        /// Returns the spell/combat ability name for the rank the character has.
        /// </summary>
        [JsonIgnore]
        public SpellType RankName => GetMember<SpellType>("RankName");

        /// <summary>
        /// TODO: What is SpellType.SpellGroup?
        /// </summary>
        public uint? SpellGroup => GetMember<IntType>("SpellGroup");

        /// <summary>
        /// TODO: What is SpellType.SubSpellGroup?
        /// </summary>
        public uint? SubSpellGroup => GetMember<IntType>("SubSpellGroup");

        /// <summary>
        /// Is spell beneficial?
        /// </summary>
        public bool Beneficial => GetMember<BoolType>("Beneficial");

        /// <summary>
        /// Is the spell an active AA?
        /// </summary>
        public bool IsActiveAA => GetMember<BoolType>("IsActiveAA");

        /// <summary>
        /// TODO: what is this? Online doco is also not sure -> "Appears to be max distance".
        /// </summary>
        public uint? Location => GetMember<IntType>("Location");

        /// <summary>
        /// Is this spell a swarm spell?
        /// </summary>
        public bool IsSwarmSpell => GetMember<BoolType>("IsSwarmSpell");

        /// <summary>
        /// Duration of the spell (if any). Note - returns DurationCap member of SPELLINFO
        /// </summary>
        public uint? DurationValue1 => GetMember<IntType>("DurationValue1");

        /// <summary>
        /// No info in online doco for this member.
        /// </summary>
        public bool StacksWithDiscs => GetMember<BoolType>("StacksWithDiscs");

        /// <summary>
        /// Illusion cast by this spell is allowed when you are mounted
        /// </summary>
        public bool IllusionOkWhenMounted => GetMember<BoolType>("IllusionOkWhenMounted");

        /// <summary>
        /// Does this spell have a given SPA?
        /// </summary>
        private readonly IndexedMember<BoolType, int> _hasSPA;

        /// <summary>
        /// Does this spell have a given SPA?
        /// </summary>
        /// <param name="effectIndex"></param>
        /// <returns></returns>
        public bool HasSPA(int effectIndex) => _hasSPA[effectIndex];

        /// <summary>
        /// TODO: What is SpellType.Trigger
        /// </summary>
        private readonly IndexedMember<SpellType, int> _trigger;

        /// <summary>
        /// TODO: What is SpellType.Trigger
        /// </summary>
        /// <param name="index">The 1 based index.</param>
        /// <returns></returns>
        public SpellType GetTrigger(int index) => _trigger[index];

        /// <summary>
        /// Percentage of slow, example of use ${Target.Slowed.SlowPct} or ${Spell[Slowing Helix].SlowPct}
        /// </summary>
        public int? SlowPct => GetMember<IntType>("SlowPct");

        /// <summary>
        /// Percentage of haste, example of use ${Me.Hasted.HastePct} or ${Spell[Speed of Milyex].HastePct}.
        /// </summary>
        public int? HastePct => GetMember<IntType>("HastePct");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? BaseEffectsFocusCap => GetMember<IntType>("BaseEffectsFocusCap");

        /// <summary>
        /// Whether a spell can be dispelled.
        /// </summary>
        public bool Dispellable => GetMember<BoolType>("Dispellable");

        /// <summary>
        /// Generate a clickable spell link. text is optional and overrides the text of the link.
        /// Link[ text ]
        /// </summary>
        private readonly IndexedStringMember<string> _link;

        /// <summary>
        /// Generate a clickable spell link. Text is optional and overrides the text of the link.
        /// Link[ text ]
        /// </summary>
        /// <param name="spellNameOverride"></param>
        /// <returns></returns>
        public string GetLink(string spellNameOverride) => _link[spellNameOverride];

        /// <summary>
        /// Generate a clickable spell link.
        /// </summary>
        /// <returns></returns>
        public string GetLink() => _link[""];

        public int? MinCasterLevel => GetMember<IntType>("MinCasterLevel");

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