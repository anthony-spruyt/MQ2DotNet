using JetBrains.Annotations;
using MQ2DotNet.MQ2API;
using MQ2DotNet.MQ2API.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace MQ2DotNet.Services
{
    /// <summary>
    /// Provides access to all top level objects.
    /// Last Verified: 2023-07-02 WIP...
    /// https://docs.macroquest.org/reference/top-level-objects/
    /// </summary>
    [PublicAPI]
    public class TLO
    {
        private readonly MQ2TypeFactory _typeFactory;

        /// <summary>
        /// Creates a new instance of TLO that uses the supplied MQ2TypeFactory to create MQ2DataType
        /// </summary>
        /// <param name="typeFactory"></param>
        public TLO(MQ2TypeFactory typeFactory)
        {
            _typeFactory = typeFactory;
            _alert = new IndexedTLO<AlertType, int, StringType, string>(this, "Alert");
            _alias = new IndexedTLO<BoolType>(this, "Alias");
            _altAbility = new IndexedTLO<AltAbilityType, int, AltAbilityType, string>(this, "AltAbility");
            //_bool = new IndexedTLO<BoolType>(this, "Bool");
            _defined = new IndexedTLO<BoolType>(this, "Defined");
            _familiar = new IndexedTLO<KeyRingItemType, string, KeyRingItemType, int>(this, "Familiar");
            _findItem = new IndexedTLO<ItemType, string, ItemType, int>(this, "FindItem");
            _findItemBank = new IndexedTLO<ItemType, string, ItemType, int>(this, "FindItemBank");
            _findItemBankCount = new IndexedTLO<IntType, string, IntType, int>(this, "FindItemBankCount");
            _findItemCount = new IndexedTLO<IntType, string, IntType, int>(this, "FindItemCount");
            //_float = new IndexedTLO<FloatType>(this, "Float");
            _groundItemCount = new IndexedTLO<IntType>(this, "GroundItemCount");



            //TODO below







            Heading = new IndexedTLO<HeadingType, string>(this, "");
            Illusion = new IndexedTLO<KeyRingType, string, KeyRingType, int>(this, "Illusion");
            InvSlot = new IndexedTLO<InvSlotType, string, InvSlotType, int>(this, "InvSlot");
            LineOfSight = new IndexedTLO<BoolType>(this, "LineOfSight");
            Mount = new IndexedTLO<KeyRingType, string, KeyRingType, int>(this, "Mount");
            NearestSpawn = new IndexedTLO<SpawnType>(this, "NearestSpawn");
            Plugin = new IndexedTLO<PluginType, string, PluginType, int>(this, "Plugin");
            Skill = new IndexedTLO<SkillType, string, SkillType, int>(this, "Skill");
            Spawn = new IndexedTLO<SpawnType>(this, "Spawn");
            SpawnCount = new IndexedTLO<IntType>(this, "SpawnCount");
            Spell = new IndexedTLO<SpellType, string, SpellType, int>(this, "Spell");
            SubDefined = new IndexedTLO<BoolType>(this, "SubDefined");
            Task = new IndexedTLO<TaskType, string, TaskType, int>(this, "Task");
            Window = new IndexedTLO<WindowType>(this, "Window");
            Zone = new IndexedTLO<ZoneType>(this, "Zone");
            LastSpawn = new IndexedTLO<SpawnType, int>(this, "LastSpawn");
        }

        /// <summary>
        /// TODO: Not supported currently.
        /// Use the text API => ${Achievement[Master of Claws of Veeshan].ID} 
        /// Provides access to achievements.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-achievement/
        /// </summary>
        public AchievementManagerType AchievementManager => GetTLO<AchievementManagerType>("Achievement");

        /// <summary>
        /// The AdvLoot TLO grants access to items in the Advanced Loot window.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-advloot/
        /// </summary>
        public AdvLootType AdvLoot => GetTLO<AdvLootType>("AdvLoot");

        /// <summary>
        /// Provides access to spawn search filter criteria in alerts. Alerts are created using /alert.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-alert/
        /// </summary>
        private IndexedTLO<AlertType, int, StringType, string> _alert;

        /// <summary>
        /// List of all alert IDs in use.
        /// Equivalent of ${Alert}
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-alert/#forms
        /// </summary>
        public IEnumerable<int> AlertIDs
        {
            get
            {
                List<int> items = new List<int>();

                var raw = (string)_alert[""];

                if (!string.IsNullOrWhiteSpace(raw))
                {
                    items.AddRange(raw.Split('|').Select(id => int.Parse(id)));
                }

                return items;
            }
        }

        /// <summary>
        /// Retrieve information for the alert category by its id.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-alert/#forms
        /// </summary>
        /// <param name="alertID"></param>
        /// <returns></returns>
        public AlertType GetAlert(int alertID) => _alert[alertID];

        /// <summary>
        /// Provides access to spawn search filter criteria in alerts. Alerts are created using /alert.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-alert/#forms
        /// </summary>
        public IEnumerable<AlertType> Alerts => AlertIDs.Select(id => GetAlert(id));

        /// <summary>
        /// Provides a way to query whether a given alias exists. See /alias.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-alias/
        /// </summary>
        private IndexedTLO<BoolType> _alias;

        /// <summary>
        /// Returns bool indicating if named aliase exists
        /// Alias[ Name ]
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-alias/#forms
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public bool IsAlias(string name) => (bool)_alias[name];

        /// <summary>
        /// Danger: The AltAbility TLO should not be used except for when experimenting with data. If you've already purchased the AA, use <see cref="CharacterType._altAbility"/>, which is tailored to your character and is much faster.
        /// </summary>
        private IndexedTLO<AltAbilityType, int, AltAbilityType, string> _altAbility;

        /// <summary>
        /// Danger: The AltAbility TLO should not be used except for when experimenting with data. If you've already purchased the AA, use <see cref="CharacterType._altAbility"/>, which is tailored to your character and is much faster.
        /// 
        /// Look up an AltAbility by its altability id
        /// AltAbility[ Number ]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-altability/#forms
        /// </summary>
        /// <param name="altabilityID"></param>
        /// <returns></returns>
        public AltAbilityType GetAltAbility(int altabilityID) => _altAbility[altabilityID];

        /// <summary>
        /// Danger: The AltAbility TLO should not be used except for when experimenting with data. If you've already purchased the AA, use <see cref="CharacterType._altAbility"/>, which is tailored to your character and is much faster.
        /// 
        /// Look up an AltAbility by its name
        /// AltAbility[ Name ]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-altability/#forms
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AltAbilityType GetAltAbility(string name) => _altAbility[name];

        /// <summary>
        /// No point exposing this.
        /// 
        /// Creates a bool object from a string. The resulting value is a bool depending on whether the given string is falsey or not.
        /// "Falsey" is defined as any of the following values:
        /// - Empty String
        /// - FALSE
        /// - NULL
        /// - The string "0"
        /// If the string is one of these values, the resulting bool is false. Otherwise, it is true.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-bool/
        /// </summary>
        //private IndexedTLO<BoolType> _bool;

        /// <summary>
        /// Access to objects of type corpse, which is the currently active corpse (ie. the one you are looting).
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-corpse/
        /// </summary>
        public CorpseType Corpse => GetTLO<CorpseType>("Corpse");

        /// <summary>
        /// Creates an object which references the item on your cursor.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-cursor/
        /// </summary>
        public ItemType Cursor => GetTLO<ItemType>("Cursor");

        /// <summary>
        /// Determines whether a variable, array, or timer with this name exists. The variable, array or timer must not be enclosed with ${}.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-defined/
        /// </summary>
        private IndexedTLO<BoolType> _defined;

        /// <summary>
        /// Determines whether a variable, array, or timer with this name exists. The variable, array or timer must not be enclosed with ${}.
        /// Returns true if the given variable name is defined.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-defined/
        /// </summary>
        /// <param name="name">The variable name.</param>
        /// <returns></returns>
        public bool IsDefined(string name) => _defined[name];

        /// <summary>
        /// This TLO gives you access to all the information in the Item Display window.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-displayitem/
        /// </summary>
        public ItemType DisplayItem => GetTLO<ItemType>("DisplayItem");

        /// <summary>
        /// Object used to return information on your doortarget.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-doortarget/
        /// </summary>
        public SpawnType DoorTarget => GetTLO<SpawnType>("DoorTarget");

        /// <summary>
        /// Provides access to properties of the current dynamic (instanced) zone.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-dynamiczone/
        /// </summary>
        public DynamicZoneType DynamicZone => GetTLO<DynamicZoneType>("DynamicZone");

        /// <summary>
        /// Provides access to general information about the game and its state.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-everquest/
        /// </summary>
        public EverQuestType EverQuest => GetTLO<EverQuestType>("EverQuest");

        /// <summary>
        /// Used to get information about items on your familiars keyring.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-familiar/
        /// </summary>
        private IndexedTLO<KeyRingItemType, string, KeyRingItemType, int> _familiar;

        /// <summary>
        /// Access to the familiar keyring.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-familiar/
        /// </summary>
        public KeyRingType FamiliarKeyRing => GetTLO<KeyRingType>("Familiar");

        /// <summary>
        /// Retrieves the item in your familiar keyring by index (base 1).
        /// Familiar[N]
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-familiar/
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public KeyRingItemType GetFamiliarKeyRingItem(int index) => _familiar[index];

        /// <summary>
        /// Retrieve the item in your familiar keyring by name. A = can be prepended for an exact match.
        /// Familiar[name]
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-familiar/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public KeyRingItemType GetFamiliarKeyRingItem(string name) => _familiar[name];

        /// <summary>
        /// Familiars on the familiar keyring.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-familiar/
        /// </summary>
        public IEnumerable<KeyRingItemType> Familiars
        {
            get
            {
                var count = (int?)FamiliarKeyRing?.Count ?? 0;
                List<KeyRingItemType> items = new List<KeyRingItemType>(count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetFamiliarKeyRingItem(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// A TLO used to find an item on your character, corpse, or a merchant by partial or exact name match. See examples below.
        /// 
        /// Search for an item using the given item id, or partial name match. Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItem[name/id]
        /// 
        /// Search for an item using exact name match (case insensitive). Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItem[=name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditem/
        /// </summary>
        private IndexedTLO<ItemType, string, ItemType, int> _findItem;

        /// <summary>
        /// A TLO used to find an item on your character, corpse, or a merchant by partial or exact name match. See examples below.
        /// 
        /// Search for an item using a partial name match. Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItem[name]
        /// 
        /// Search for an item using exact name match (case insensitive). Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItem[=name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditem/
        /// </summary>
        /// <param name="name">The name to match. No need to prefix with "=" for exact match, set the partialMatch param to false.</param>
        /// <param name="partialMatch">Optional partial match parameter. Default is true. If false an exact case insensitive match is required.</param>
        /// <returns></returns>
        public ItemType FindItem(string name, bool partialMatch = true) => _findItem[partialMatch ? name : $"={name}"];

        /// <summary>
        /// A TLO used to find an item on your character, corpse, or a merchant by partial or exact name match. See examples below.
        /// 
        /// Search for an item using the given item id. Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItem[id]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditem/
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public ItemType FindItem(int itemID) => _findItem[itemID];

        /// <summary>
        /// A TLO used to find an item in your bank by partial or exact name match. See examples below.
        /// Of note: The FindItemBank with ItemSlot REQUIRES that bank item containers be open to function correctly.
        /// Due to potential exploits the command will not work if the bank containers are closed.
        /// This is in contrast to FindItem functionality with character containers, where ItemSlot was designed to allow inventory management without opening containers.
        /// 
        /// Search for an item in your bank using the given item id, or partial name match.
        /// FindItemBank[name/id]
        ///  
        /// Search for an item in your bank using exact name match (case insensitive).
        /// FindItemBank[=name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditembank/
        /// </summary>
        private IndexedTLO<ItemType, string, ItemType, int> _findItemBank;

        /// <summary>
        /// A TLO used to find an item in your bank by partial or exact name match. See examples below.
        /// Of note: The FindItemBank with ItemSlot REQUIRES that bank item containers be open to function correctly.
        /// Due to potential exploits the command will not work if the bank containers are closed.
        /// This is in contrast to FindItem functionality with character containers, where ItemSlot was designed to allow inventory management without opening containers.
        /// 
        /// Search for an item in your bank using the given partial name match.
        /// FindItemBank[name]
        ///  
        /// Search for an item in your bank using exact name match (case insensitive).
        /// FindItemBank[=name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditembank/
        /// </summary>
        /// <param name="name">The name to match. No need to prefix with "=" for exact match, set the partialMatch param to false.</param>
        /// <param name="partialMatch">Optional partial match parameter. Default is true. If false an exact case insensitive match is required.</param>
        /// <returns></returns>
        public ItemType FindItemBank(string name, bool partialMatch = true) => _findItemBank[partialMatch ? name : $"={name}"];

        /// <summary>
        /// A TLO used to find an item in your bank by partial or exact name match. See examples below.
        /// Of note: The FindItemBank with ItemSlot REQUIRES that bank item containers be open to function correctly.
        /// Due to potential exploits the command will not work if the bank containers are closed.
        /// This is in contrast to FindItem functionality with character containers, where ItemSlot was designed to allow inventory management without opening containers.
        /// 
        /// Search for an item in your bank using the given item id.
        /// FindItemBank[id]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditembank/
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public ItemType FindItemBank(int itemID) => _findItemBank[itemID];

        /// <summary>
        /// A TLO used to find a count of items in your bank by partial or exact name match. See examples below.
        /// 
        /// Counts the items in your bank using the given item id, or partial name match.
        /// FindItemBankCount[name/id]
        /// 
        /// Counts the items in your bank using exact name match (case insensitive).
        /// FindItemBankCount[=name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditembankcount/
        /// </summary>
        private IndexedTLO<IntType, string, IntType, int> _findItemBankCount;

        /// <summary>
        /// A TLO used to find a count of items in your bank by partial or exact name match. See examples below.
        /// 
        /// Counts the items in your bank using partial name match.
        /// FindItemBankCount[name]
        /// 
        /// Counts the items in your bank using exact name match (case insensitive).
        /// FindItemBankCount[=name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditembankcount/
        /// </summary>
        /// <param name="name">The name to match. No need to prefix with "=" for exact match, set the partialMatch param to false.</param>
        /// <param name="partialMatch">Optional partial match parameter. Default is true. If false an exact case insensitive match is required.</param>
        /// <returns></returns>
        public uint? FindItemBankCount(string name, bool partialMatch = true) => (uint?)_findItemBankCount[partialMatch ? name : $"={name}"];

        /// <summary>
        /// A TLO used to find a count of items in your bank by partial or exact name match. See examples below.
        /// 
        /// Counts the items in your bank using the given item id.
        /// FindItemBankCount[id]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditembankcount/
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public uint? FindItemBankCount(int itemID) => (uint?)_findItemBankCount[itemID];

        /// <summary>
        /// A TLO used to find a count of items on your character, corpse, or a merchant by partial or exact name match. See examples below.
        /// 
        /// Counts the items using the given item id, or partial name match. Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItemCount[name/id]
        /// 
        /// Counts the items using exact name match (case insensitive). Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItemCount[=name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditemcount/
        /// </summary>
        private IndexedTLO<IntType, string, IntType, int> _findItemCount;

        /// <summary>
        /// A TLO used to find a count of items on your character, corpse, or a merchant by partial or exact name match. See examples below.
        /// 
        /// Counts the items using partial name match. Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItemCount[name]
        /// 
        /// Counts the items using exact name match (case insensitive). Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItemCount[=name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditemcount/
        /// </summary>
        /// <param name="name">The name to match. No need to prefix with "=" for exact match, set the partialMatch param to false.</param>
        /// <param name="partialMatch">Optional partial match parameter. Default is true. If false an exact case insensitive match is required.</param>
        /// <returns></returns>
        public uint? FindItemCount(string name, bool partialMatch = true) => (uint?)_findItemCount[partialMatch ? name : $"={name}"];

        /// <summary>
        /// A TLO used to find a count of items on your character, corpse, or a merchant by partial or exact name match. See examples below.
        /// 
        /// Counts the items using the given item id. Will search character inventory and any items stored in key rings (illusion, mount, etc).
        /// FindItemCount[id]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-finditemcount/
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public uint? FindItemCount(int itemID) => (uint?)_findItemCount[itemID];

        /// <summary>
        /// No point exposing this.
        /// 
        /// Creates a float object from n.
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-float/
        /// </summary>
        //private IndexedTLO<FloatType> _float;

        /// <summary>
        /// The FrameLimiter TLO provides access to the frame limiter feature
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-framelimiter/
        /// </summary>
        public FrameLimiterType FrameLimiter => GetTLO<FrameLimiterType>("FrameLimiter");

        /// <summary>
        /// Grants access to your friends list.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-friends/
        /// </summary>
        public FriendsType Friends => GetTLO<FriendsType>("Friends");

        /// <summary>
        /// A time object indicating EQ Game Time.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-gametime/
        /// </summary>
        public DateTime? GameTime => GetTLO<TimeType>("GameTime");

        /// <summary>
        /// Object which references the ground spawn item you have targeted or the closest if none is targeted, if any in zone.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-ground/
        /// </summary>
        public GroundType Ground => GetTLO<GroundType>("Ground");

        /// <summary>
        /// Access to all Groundspawn item count information.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-grounditemcount/
        /// </summary>
        private IndexedTLO<IntType> _groundItemCount { get; }

        /// <summary>
        /// Access to all Groundspawn item count information.
        /// 
        /// /echo There are ${GroundItemCount[apple]} Apples on the ground.
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-grounditemcount/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int? GetGroundItemCount(string name) => _groundItemCount[name];

        /// <summary>
        /// Access to all Groundspawn item count information.
        /// 
        /// /echo There are ${GroundItemCount} items on the ground in this zone.
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-grounditemcount/
        /// </summary>
        /// <returns></returns>
        public int? GroundItemCount => _groundItemCount[""];

        /// <summary>
        /// Access to all group-related information.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-group/
        /// </summary>
        public GroupType Group => GetTLO<GroupType>("Group");




















        //TODO below

        /// <summary>
        /// Character object which allows you to get properties of you as a character.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-me/
        /// </summary>
        public CharacterType Me => GetTLO<CharacterType>("Me");

        
        /// <summary>
        /// Your target
        /// </summary>
        public TargetType Target => GetTLO<TargetType>("Target");
        
        /// <summary>
        /// Your current door target
        /// </summary>
        public SwitchType Switch => GetTLO<SwitchType>("Switch");
        
        /// <summary>
        /// Your mercenary
        /// </summary>
        public MercenaryType Mercenary => GetTLO<MercenaryType>("Mercenary");
        
        /// <summary>
        /// Your pet
        /// </summary>
        public PetType Pet => GetTLO<PetType>("Pet");
        
        /// <summary>
        /// Merchant that is currently open
        /// </summary>
        public MerchantType Merchant => GetTLO<MerchantType>("Merchant");
        
        /// <summary>
        /// Macro that is running
        /// </summary>
        public MacroType Macro => GetTLO<MacroType>("Macro");
        
        /// <summary>
        /// <see cref="MacroQuestType"/> instance
        /// </summary>
        public MacroQuestType MacroQuest => GetTLO<MacroQuestType>("MacroQuest");
        
        /// <summary>
        /// TODO: What does SelectedItem give?
        /// </summary>
        public ItemType SelectedItem => GetTLO<ItemType>("SelectedItem");
        
        /// <summary>
        /// <see cref="RaidType"/> instance
        /// </summary>
        public RaidType Raid => GetTLO<RaidType>("Raid");
        
        /// <summary>
        /// Spawn whose name is currently being drawn
        /// </summary>
        public SpawnType NamingSpawn => GetTLO<SpawnType>("NamingSpawn");
        
        /// <summary>
        /// Your current item target
        /// </summary>
        public SpawnType ItemTarget => GetTLO<SpawnType>("ItemTarget");
        
        /// <summary>
        /// Point merchnat that is currently open
        /// </summary>
        public PointMerchantType PointMerchant => GetTLO<PointMerchantType>("PointMerchant");
        
        /// <summary>
        /// Zone you are currently in
        /// </summary>
        public CurrentZoneType CurrentZone => GetTLO<CurrentZoneType>("Zone");
        
        /// <summary>
        /// Heading to a location in y,x format.
        /// TODO: I think this is incorrect and shoud be an IndexedTLO so converted it but havent tested yet...
        /// </summary>
        //public HeadingType Heading => GetTLO<HeadingType>("Heading");
        public IndexedTLO<HeadingType, string> Heading { get; }
        
        /// <summary>
        /// First spawn that matches a search string
        /// </summary>
        public IndexedTLO<SpawnType> Spawn { get; }
        
        /// <summary>
        /// Spell by name or ID
        /// </summary>
        public IndexedTLO<SpellType, string, SpellType, int> Spell { get; }
        
        /// <summary>
        /// Window by name
        /// </summary>
        public IndexedTLO<WindowType> Window { get; }
        
        /// <summary>
        /// Zone by ID or short name. For current zone, use <see cref="CurrentZone"/>
        /// </summary>
        public IndexedTLO<ZoneType> Zone { get; }
        
        /// <summary>
        /// Spawn by position in the list, from the end for negative numbers
        /// </summary>
        public IndexedTLO<SpawnType, int> LastSpawn { get; }
        
        /// <summary>
        /// Nth nearest spawn that matches a search e.g. "2,npc" for the 2nd closest NPC
        /// </summary>
        public IndexedTLO<SpawnType> NearestSpawn { get; }
        
        /// <summary>
        /// Total number of spawns that match a search
        /// </summary>
        public IndexedTLO<IntType> SpawnCount { get; }
        
        /// <summary>
        /// An inventory slot by name or number
        /// </summary>
        /// <remarks>Valid slot numbers are:
        /// 2000-2015 bank window
        /// 2500-2503 shared bank
        /// 5000-5031 loot window
        /// 3000-3015 trade window (including npc) 3000-3007 are your slots, 3008-3015 are other character's slots
        /// 4000-4010 world container window
        /// 6000-6080 merchant window
        /// 7000-7080 bazaar window
        /// 8000-8031 inspect window</remarks>
        public IndexedTLO<InvSlotType, string, InvSlotType, int> InvSlot { get; }
        
        /// <summary>
        /// Plugin by name or number
        /// </summary>
        public IndexedTLO<PluginType, string, PluginType, int> Plugin { get; }
        
        /// <summary>
        /// Skill by name or number
        /// </summary>
        public IndexedTLO<SkillType, string, SkillType, int> Skill { get; }
        
        /// <summary>
        /// Is there line of sight between two locations, in the format "y,x,z:y,x,z"
        /// </summary>
        public IndexedTLO<BoolType> LineOfSight { get; }
        
        /// <summary>
        /// Task by name or position in window (1 based)
        /// </summary>
        public IndexedTLO<TaskType, string, TaskType, int> Task { get; }
        
        /// <summary>
        /// Mount (on keyring) by name or position in window (1 based). Name is partial match unless it begins with =
        /// </summary>
        public IndexedTLO<KeyRingType, string, KeyRingType, int> Mount { get; }
        
        /// <summary>
        /// Illusion (on keyring) by name or position in window (1 based). Name is partial match unless it begins with =
        /// </summary>
        public IndexedTLO<KeyRingType, string, KeyRingType, int> Illusion { get; }
        
        /// <summary>
        /// Requires EXPANSION_LEVEL_TOL
        /// TeleportationItem (on keyring) by name or position in window (1 based). Name is partial match unless it begins with =
        /// </summary>
        public IndexedTLO<KeyRingType, string, KeyRingType, int> TeleportationItem { get; }
        
        /// <summary>
        /// Currently open context menu
        /// </summary>
        public MenuType Menu => GetTLO<MenuType>("Menu");
        
        /// <summary>
        /// Is a sub with the given name defined?
        /// </summary>
        public IndexedTLO<BoolType> SubDefined { get; }
        
        /// <summary>
        /// Dependency on MQ2Cast.
        /// </summary>
        public CastType Cast => GetTLO<CastType>("Cast");

        /// <summary>
        /// Get a TLO by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetTLO<T>(string name, string index = "") where T : MQ2DataType
        {
            if (!NativeMethods.FindTLO(name, index, out var typeVar))
            {
                return null;
            }

            T tlo;

            try
            {
                tlo = (T)_typeFactory.Create(new MQ2TypeVar(typeVar));
            }
            catch (Exception ex)
            {
                MQ2DataType.DataTypeErrors.TryAdd($"{name}_{index}_{typeof(T).DeclaringType ?? typeof(T)}", ex);

                tlo = null;
            }

            return tlo;
        }

        #region Helper classes
        /// <summary>
        /// Helper class for a TLO that is accessed with an indexer
        /// </summary>
        /// <typeparam name="T">Data type to return</typeparam>
        /// <typeparam name="TIndex">Type for index parameter</typeparam>
        public class IndexedTLO<T, TIndex> where T : MQ2DataType
        {
            private readonly TLO _tlo;
            private readonly string _name;

            internal IndexedTLO(TLO tlo, string name)
            {
                _tlo = tlo;
                _name = name;
            }

            /// <summary>
            /// Get the TLO using an index
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public T this[TIndex index] => _tlo.GetTLO<T>(_name, index.ToString());
        }

        /// <inheritdoc />
        public class IndexedTLO<T> : IndexedTLO<T, string> where T : MQ2DataType
        {
            internal IndexedTLO(TLO tlo, string name) : base(tlo, name)
            {
            }
        }

        /// <summary>
        /// Helper class for a TLO that is accessed with an indexer
        /// </summary>
        /// <typeparam name="T1">Data type to return given first index type</typeparam>
        /// <typeparam name="TIndex1">First type for index parameter</typeparam>
        /// <typeparam name="T2">Data type to return given second index type</typeparam>
        /// <typeparam name="TIndex2">Second type for index parameter</typeparam>
        public class IndexedTLO<T1, TIndex1, T2, TIndex2> where T1 : MQ2DataType where T2 : MQ2DataType
        {
            private readonly TLO _tlo;
            private readonly string _name;

            internal IndexedTLO(TLO tlo, string name)
            {
                _tlo = tlo;
                _name = name;
            }

            /// <summary>
            /// Get the TLO using an index of type <typeparamref name="TIndex1"/>
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public T1 this[TIndex1 index] => _tlo.GetTLO<T1>(_name, index.ToString());

            /// <summary>
            /// Get the TLO using an index of type <typeparamref name="TIndex2"/>
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public T2 this[TIndex2 index] => _tlo.GetTLO<T2>(_name, index.ToString());
        }
        #endregion

        private static class NativeMethods
        {
            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool FindTLO([MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.LPStr)] string index, out NativeMQ2TypeVar typeVar);
        }
    }
}