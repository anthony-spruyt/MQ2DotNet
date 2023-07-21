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
    public class TLO
    {
        private readonly MQ2TypeFactory _typeFactory;

        public static TLO Instance { get; private set; }

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
            _heading = new IndexedTLO<HeadingType, string>(this, "Heading");
            _illusion = new IndexedTLO<KeyRingItemType, string, KeyRingItemType, int>(this, "Illusion");
            _lastSpawn = new IndexedTLO<SpawnType, int>(this, "LastSpawn");
            _lineOfSight = new IndexedTLO<BoolType>(this, "LineOfSight");
            _mount = new IndexedTLO<KeyRingItemType, string, KeyRingItemType, int>(this, "Mount");
            _nearestSpawn = new IndexedTLO<SpawnType, int, SpawnType, string>(this, "NearestSpawn");
            _plugin = new IndexedTLO<PluginType, string, PluginType, int>(this, "Plugin");
            _skill = new IndexedTLO<SkillType, string, SkillType, int>(this, "Skill");
            _spawn = new IndexedTLO<SpawnType, string, SpawnType, int>(this, "Spawn");
            _spawnCount = new IndexedTLO<IntType>(this, "SpawnCount");
            _spell = new IndexedTLO<SpellType, string, SpellType, int>(this, "Spell");
            _task = new IndexedTLO<TaskType, string, TaskType, int>(this, "Task");
            _type = new IndexedTLO<TypeType>(this, "Type");
            _window = new IndexedTLO<WindowType>(this, "Window");
            _zone = new IndexedTLO<ZoneType, string, ZoneType, int>(this, "Zone");
            _teleportationItem = new IndexedTLO<KeyRingItemType, string, KeyRingItemType, int>(this, "TeleportationItem");
            _subDefined = new IndexedTLO<BoolType>(this, "SubDefined");
            _invSlot = new IndexedTLO<InvSlotType, string, InvSlotType, int>(this, "InvSlot");

            if (Instance == null)
            {
                Instance = this;
            }
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
        private readonly IndexedTLO<AlertType, int, StringType, string> _alert;

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
        private readonly IndexedTLO<BoolType> _alias;

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
        private readonly IndexedTLO<AltAbilityType, int, AltAbilityType, string> _altAbility;

        /// <summary>
        /// Danger: The AltAbility TLO should not be used except for when experimenting with data. If you've already purchased the AA, use <see cref="CharacterType._altAbility"/>, which is tailored to your character and is much faster.
        /// 
        /// Look up an AltAbility by its altability id
        /// AltAbility[ Number ]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-altability/#forms
        /// </summary>
        /// <param name="altAbilityID"></param>
        /// <returns></returns>
        public AltAbilityType GetAltAbility(int altAbilityID) => _altAbility[altAbilityID];

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
        //private readonly IndexedTLO<BoolType> _bool;

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
        private readonly IndexedTLO<BoolType> _defined;

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
        private readonly IndexedTLO<KeyRingItemType, string, KeyRingItemType, int> _familiar;

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

                for (int i = 0; i < count; i++)
                {
                    yield return GetFamiliarKeyRingItem(i + 1);
                }
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
        private readonly IndexedTLO<ItemType, string, ItemType, int> _findItem;

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
        private readonly IndexedTLO<ItemType, string, ItemType, int> _findItemBank;

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
        private readonly IndexedTLO<IntType, string, IntType, int> _findItemBankCount;

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
        private readonly IndexedTLO<IntType, string, IntType, int> _findItemCount;

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
        //private readonly IndexedTLO<FloatType> _float;

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
        private readonly IndexedTLO<IntType> _groundItemCount;

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

        /// <summary>
        /// Object that refers to the directional heading to of a location or direction.
        /// 
        /// Creates a heading object using degrees (clockwise)
        /// Heading[#]
        /// 
        /// Creates a heading object using the heading to this y,x location
        /// Heading[**y,x]**
        /// 
        /// Same as above, just an alternate method
        /// Heading[**N,W]**
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-heading/
        /// </summary>
        private readonly IndexedTLO<HeadingType, string> _heading;

        /// <summary>
        /// Creates a heading object using the heading to this y,x location
        /// Heading[**y,x]**
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-heading/
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public HeadingType GetHeading(float y, float x) => _heading[$"{y},{x}"];

        // Next TLO is ${If[]} we are not supporting it here
        // Doco: The If TLO is used to provide inline condition expressions for macros. It is not recommended for use with Lua.

        /// <summary>
        /// Used to get information about items on your illusions keyring.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-illusion/
        /// </summary>
        private readonly IndexedTLO<KeyRingItemType, string, KeyRingItemType, int> _illusion;

        /// <summary>
        /// Access to the illusion keyring.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-illusion/
        /// </summary>
        public KeyRingType IllusionKeyRing => GetTLO<KeyRingType>("Illusion");

        /// <summary>
        /// Retrieves the item in your illusion keyring by index (base 1).
        /// Illusion[N]
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-illusion/
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public KeyRingItemType GetIllusionKeyRingItem(int index) => _illusion[index];

        /// <summary>
        /// Retrieve the item in your illusion keyring by name. A = can be prepended for an exact match.
        /// Illusion[name]
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-illusion/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public KeyRingItemType GetIllusionKeyRingItem(string name) => _illusion[name];

        /// <summary>
        /// Illusions on the illusion keyring.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-illusion/
        /// </summary>
        public IEnumerable<KeyRingItemType> Illusions
        {
            get
            {
                var count = (int?)IllusionKeyRing?.Count ?? 0;

                for (int i = 0; i < count; i++)
                {
                    yield return GetIllusionKeyRingItem(i + 1);
                }
            }
        }

        // Next TLO is "Ini" we are not supporting it here
        // Make use of .net configuration

        /// <summary>
        /// No point exposing this.
        /// 
        /// Object that creates an integer from n.
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-int/
        /// </summary>
        //private readonly IndexedTLO<IntType> _int;

        /// <summary>
        /// Gives access to the ground item that is previously targeted using /itemtarget IE <see cref="MQ2API.DataTypes.GroundType.DoTarget"/>.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-itemtarget/
        /// </summary>
        public GroundType ItemTarget => GetTLO<GroundType>("ItemTarget");

        /// <summary>
        /// Information about the spawns that have occurred since you entered the zone.
        /// When you enter a zone you dont know the spawn order of anything already there, just anything that spawns while you are in the zone.
        ///
        /// The useful thing about ${LastSpawn[-1]} is just being able to get the first spawn in the list which you might use in conjunction with other spawn members to go through the entire spawn list in a loop.
        /// 
        /// The nth latest spawn (chronological order)
        /// LastSpawn[**n]**
        /// 
        /// The nth oldest spawn (chronological order)
        /// LastSpawn[-n**]**
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-lastspawn/
        /// </summary>
        private readonly IndexedTLO<SpawnType, int> _lastSpawn;

        /// <summary>
        /// Information about the spawns that have occurred since you entered the zone.
        /// When you enter a zone you dont know the spawn order of anything already there, just anything that spawns while you are in the zone.
        ///
        /// The useful thing about ${LastSpawn[-1]} is just being able to get the first spawn in the list which you might use in conjunction with other spawn members to go through the entire spawn list in a loop.
        /// 
        /// The nth latest spawn (chronological order)
        /// LastSpawn[**n]**
        /// 
        /// The nth oldest spawn (chronological order)
        /// LastSpawn[-n**]**
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-lastspawn/
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public SpawnType GetLastSpawn(int nth) => _lastSpawn[nth];

        /// <summary>
        /// Object that is used to check if there is Line of Sight between two locations.
        /// Note: For ISXEQ all 6 parameters are required and will return NULL otherwise
        /// 
        /// LineOfSight[**y,x,z:y,x,z]**
        /// LineOfSight[**y,x,z,y,x,z]** (For ISXEQ)
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-lineofsight/
        /// </summary>
        private readonly IndexedTLO<BoolType> _lineOfSight;

        /// <summary>
        /// Check if there is Line of Sight between two locations.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-lineofsight/
        /// </summary>
        /// <param name="y1"></param>
        /// <param name="x1"></param>
        /// <param name="z1"></param>
        /// <param name="y2"></param>
        /// <param name="x2"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        public bool IsInLineOfSight(int y1, int x1, int z1, int y2, int x2, int z2) => (bool)_lineOfSight[$"{y1},{x1},{z1}:{y2},{x2},{z2}"];

        /// <summary>
        /// Returns an object related to the macro that is currently running.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-macro/
        /// </summary>
        public MacroType Macro => GetTLO<MacroType>("Macro");

        /// <summary>
        /// Creates an object related to MacroQuest information.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-macroquest/
        /// </summary>
        public MacroQuestType MacroQuest => GetTLO<MacroQuestType>("MacroQuest");

        // Skip Math TLO, we have much better here in .net

        /// <summary>
        /// Character object which allows you to get properties of you as a character.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-me/
        /// </summary>
        public CharacterType Me => GetTLO<CharacterType>("Me");

        /// <summary>
        /// Object used to get information about your mercenary.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-mercenary/
        /// </summary>
        public MercenaryType Mercenary => GetTLO<MercenaryType>("Mercenary");

        /// <summary>
        /// Object that interacts with the currently active merchant.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-merchant/
        /// </summary>
        public MerchantType Merchant => GetTLO<MerchantType>("Merchant");

        /// <summary>
        /// Used to get information about items on your Mount keyring.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-mount/
        /// </summary>
        private readonly IndexedTLO<KeyRingItemType, string, KeyRingItemType, int> _mount;

        /// <summary>
        /// Access to the Mount keyring.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-mount/
        /// </summary>
        public KeyRingType MountKeyRing => GetTLO<KeyRingType>("Mount");

        /// <summary>
        /// Retrieves the item in your mount keyring by index (base 1).
        /// Mount[N]
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-mount/
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public KeyRingItemType GetMountKeyRingItem(int index) => _mount[index];

        /// <summary>
        /// Retrieve the item in your mount keyring by name. A = can be prepended for an exact match.
        /// Mount[name]
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-mount/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public KeyRingItemType GetMountKeyRingItem(string name) => _mount[name];

        /// <summary>
        /// Mounts on the mount keyring.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-mount/
        /// </summary>
        public IEnumerable<KeyRingItemType> Mounts
        {
            get
            {
                var count = (int?)MountKeyRing?.Count ?? 0;

                for (int i = 0; i < count; i++)
                {
                    yield return GetMountKeyRingItem(i + 1);
                }
            }
        }

        /// <summary>
        /// Object that is used in finding spawns nearest to you.
        /// 
        /// The Nth nearest spawn
        /// NearestSpawn[N]
        /// 
        /// The nearest spawn matching this search string (see Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/).
        /// NearestSpawn[search]
        /// 
        /// The Nth nearest spawn matching this search string (see Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/).
        /// NearestSpawn[N,search]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-nearestspawn/
        /// </summary>
        private readonly IndexedTLO<SpawnType, int, SpawnType, string> _nearestSpawn;

        /// <summary>
        /// The Nth nearest spawn (base 1)
        /// NearestSpawn[N]
        /// 
        /// n = 1 will always be yourself.
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-nearestspawn/
        /// </summary>
        /// <param name="nth">The base 1 index.</param>
        /// <returns></returns>
        public SpawnType GetNearestSpawn(int nth) => _nearestSpawn[nth];

        /// <summary>
        /// The nearest spawn matching this search string (see Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/).
        /// NearestSpawn[search]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-nearestspawn/
        /// </summary>
        /// <param name="search">Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/</param>
        /// <returns></returns>
        public SpawnType GetNearestSpawn(string search) => _nearestSpawn[search];

        /// <summary>
        /// The Nth nearest spawn matching this search string (see Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/).
        /// NearestSpawn[N,search]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-nearestspawn/
        /// </summary>
        /// <param name="nth">The base 1 index.</param>
        /// <param name="search">Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/</param>
        /// <returns></returns>
        public SpawnType GetNearestSpawn(int nth, string search) => _nearestSpawn[$"{nth},{search}"];

        /// <summary>
        /// Pet object which allows you to get properties of your pet.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-pet/
        /// </summary>
        public PetType Pet => GetTLO<PetType>("Pet");

        /// <summary>
        /// Object that has access to members that provide information on a plugin.
        /// 
        /// Finds plugin by name, uses full name match, case insensitive.
        /// Plugin[name]
        /// 
        /// Plugin by index, starting with 1 and stopping whenever the list runs out of plugins.
        /// Plugin[N]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-plugin/
        /// </summary>
        private readonly IndexedTLO<PluginType, string, PluginType, int> _plugin;

        /// <summary>
        /// Finds plugin by name, uses full name match, case insensitive.
        /// Plugin[name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-plugin/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PluginType GetPlugin(string name) => _plugin[name];

        /// <summary>
        /// Plugin by index, starting with 1 and stopping whenever the list runs out of plugins.
        /// Plugin[N]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-plugin/
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public PluginType GetPlugin(int index) => _plugin[index];

        public IEnumerable<PluginType> Plugins
        {
            get
            {
                var index = 1;

                while(index <= PluginType.MAX_PLUGINS)
                {
                    var plugin = _plugin[index];

                    if (plugin != null)
                    {
                        index++;
                        yield return plugin;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }

        /// <summary>
        /// Object that has access to members that provide information on your raid.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-raid/
        /// </summary>
        public RaidType Raid => GetTLO<RaidType>("Raid");

        // Next is TLO Range which we are not interested in since no use of it here.

        // Next is TLO Select which we are not interested in since no use of it here.

        /// <summary>
        /// Used to return information on the object that is selected in your own inventory while using a merchant.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-selecteditem/
        /// </summary>
        public ItemType SelectedItem => GetTLO<ItemType>("SelectedItem");

        /// <summary>
        /// Object used to get information on your character's skills.
        /// 
        /// Retrieve skill by name
        /// Skill[name]
        /// 
        /// Retrieve skill by number (base 1)
        /// Skill[N]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-skill/
        /// </summary>
        private readonly IndexedTLO<SkillType, string, SkillType, int> _skill;

        /// <summary>
        /// Retrieve skill by number (base 1)
        /// Skill[N]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-skill/
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public SkillType GetSkill(int index) => _skill[index];

        /// <summary>
        /// Retrieve skill by name
        /// Skill[name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-skill/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SkillType GetSkill(string name) => _skill[name];

        /// <summary>
        /// All skills.
        /// </summary>
        public IEnumerable<SkillType> Skills
        {
            get
            {
                var index = 1;

                while (index <= SkillType.MAX_SKILLS)
                {
                    var skill = _skill[index];

                    if (skill != null)
                    {
                        index++;
                        yield return skill;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }

        /// <summary>
        /// Object used to get information on a specific spawn. Uses the filters under Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/
        /// 
        /// Spawn matching ID N.
        /// Spawn[N]
        /// 
        /// Any spawns matching search string. See Spawn Search.
        /// Spawn[search string]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-spawn/
        /// </summary>
        private readonly IndexedTLO<SpawnType, string, SpawnType, int> _spawn;

        /// <summary>
        /// Spawn matching ID N.
        /// Spawn[N]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-spawn/
        /// </summary>
        /// <param name="spawnID"></param>
        /// <returns></returns>
        public SpawnType GetSpawn(int spawnID) => _spawn[spawnID];

        /// <summary>
        /// Any spawns matching search string. See Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/
        /// Spawn[search string]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-spawn/
        /// </summary>
        /// <param name="search">See Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/</param>
        /// <returns></returns>
        public SpawnType GetSpawn(string search) => _spawn[search];

        /// <summary>
        /// Object used to count spawns based on a set of queries. Uses the filters under Spawn Search.
        /// 
        /// Total number of spawns in current zone
        /// SpawnCount
        /// 
        /// Total number of spawns in current zone matching the search string. See Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/
        /// SpawnCount[search string]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-spawncount/
        /// </summary>
        private readonly IndexedTLO<IntType> _spawnCount;

        /// <summary>
        /// Total number of spawns in current zone
        /// SpawnCount
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-spawncount/
        /// </summary>
        public uint? SpawnCount => (uint?)_spawnCount[""];

        /// <summary>
        /// Total number of spawns in current zone matching the search string. See Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/
        /// SpawnCount[search string]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-spawncount/
        /// </summary>
        /// <param name="search">See Spawn Search - https://docs.macroquest.org/reference/general/spawn-search/</param>
        /// <returns></returns>
        public uint? GetSpawnCount(string search) => (uint?)_spawnCount[search];

        /// <summary>
        /// Object used to return information on a spell by name or by ID.
        /// 
        /// Find spell by ID
        /// Spell[N]
        /// 
        /// Find spell by name
        /// Spell[name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-spell/
        /// </summary>
        private readonly IndexedTLO<SpellType, string, SpellType, int> _spell;

        /// <summary>
        /// Find spell by ID
        /// Spell[N]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-spell/
        /// </summary>
        /// <param name="spellID"></param>
        /// <returns></returns>
        public SpellType GetSpell(int spellID) => _spell[spellID];

        /// <summary>
        /// Find spell by name
        /// Spell[name]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-spell/
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns></returns>
        public SpellType GetSpell(string spellName) => _spell[spellName];

        /// <summary>
        /// Object used when you want to find information on targetted doors or switches such as the portals in PoK.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-switch/
        /// </summary>
        public SwitchType Switch => GetTLO<SwitchType>("Switch");

        /// <summary>
        /// Object used to get information about your current target.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-target/
        /// </summary>
        public TargetType Target => GetTLO<TargetType>("Target");

        /// <summary>
        /// Object used to return information on a current Task.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-task/
        /// </summary>
        private readonly IndexedTLO<TaskType, string, TaskType, int> _task;

        /// <summary>
        /// Task by position (base 1) in window.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-task/
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public TaskType GetTask(int index) => _task[index];

        /// <summary>
        /// Task by name.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-task/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TaskType GetTask(string name) => _task[name];

        /// <summary>
        /// All the character's tasks.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-task/
        /// </summary>
        public IEnumerable<TaskType> Tasks
        {
            get
            {
                var index = 1;

                while (index <= TaskType.MAX_TASKS + TaskType.MAX_SHARED_TASKS)
                {
                    var task = _task[index];

                    if (task != null)
                    {
                        index++;
                        yield return task;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }

        // Next is TLO Time which we are not interested in since no use of it here.

        /// <summary>
        /// Object that interacts with the personal tradeskill depot, introduced in the Night of Shadows expansion.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-tradeskilldepot/
        /// </summary>
        public TradeskillDepotType TradeskillDepot => GetTLO<TradeskillDepotType>("TradeskillDepot");

        /// <summary>
        /// Used to get information on data types.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-type/
        /// </summary>
        private readonly IndexedTLO<TypeType> _type;

        /// <summary>
        /// Used to get information on data types.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-type/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TypeType GetType(string name) => _type[name];

        /// <summary>
        /// Used to find information on a particular UI window.
        /// You can display a list of window names using the /windows command or by using the window inspector.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-window/
        /// </summary>
        private readonly IndexedTLO<WindowType> _window;

        /// <summary>
        /// Used to find information on a particular UI window.
        /// You can display a list of window names using the /windows command or by using the window inspector.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-window/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public WindowType GetWindow(string name) => _window[name];

        /// <summary>
        /// Used to find information about a particular zone.
        /// 
        /// Retrieves the current zone information
        /// Zone
        /// 
        /// Retrieves information about a zone by zone ID. If this zone is the current zone, then this will return currentzone.
        /// Zone[N]
        /// 
        /// Retrieves information about a zone by short name. If this zone is the current zone, then this will return currentzone.
        /// Zone[shortname]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-zone/
        /// </summary>
        private readonly IndexedTLO<ZoneType, string, ZoneType, int> _zone;

        /// <summary>
        /// Retrieves the current zone information.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-zone/
        /// </summary>
        public CurrentZoneType CurrentZone => GetTLO<CurrentZoneType>("Zone");

        /// <summary>
        /// Retrieves information about a zone by zone ID. If this zone is the current zone, then this will return currentzone.
        /// Zone[N]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-zone/
        /// </summary>
        /// <param name="zoneID"></param>
        /// <returns></returns>
        public ZoneType GetZone(int zoneID) => _zone[zoneID];

        /// <summary>
        /// Retrieves information about a zone by short name. If this zone is the current zone, then this will return currentzone.
        /// Zone[shortname]
        /// 
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-zone/
        /// </summary>
        /// <param name="shortName"></param>
        /// <returns></returns>
        public ZoneType GetZone(string shortName) => _zone[shortName];

        /// <summary>
        /// Used to get information about items on your Teleportation Item keyring.
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        private readonly IndexedTLO<KeyRingItemType, string, KeyRingItemType, int> _teleportationItem;

        /// <summary>
        /// Requires EXPANSION_LEVEL_TOL
        /// Access to the Teleportation Item keyring.
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        public KeyRingType TeleportationItemKeyRing => GetTLO<KeyRingType>("TeleportationItem");

        /// <summary>
        /// Retrieves the item in your teleportation item keyring by index (base 1).
        /// TeleportationItem[N]
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public KeyRingItemType GetTeleportationItemKeyRingItem(int index) => _teleportationItem[index];

        /// <summary>
        /// Retrieve the item in your teleportation item keyring by name. A = can be prepended for an exact match.
        /// TeleportationItem[name]
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public KeyRingItemType GetTeleportationItemKeyRingItem(string name) => _teleportationItem[name];

        /// <summary>
        /// Teleportation item on the teleportation item keyring.
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        public IEnumerable<KeyRingItemType> TeleportationItems
        {
            get
            {
                var count = (int?)TeleportationItemKeyRing?.Count ?? 0;

                for (int i = 0; i < count; i++)
                {
                    yield return GetTeleportationItemKeyRingItem(i + 1);
                }
            }
        }

        /// <summary>
        /// Gets the currently selected switch, if any.
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        public SwitchType SwitchTarget => GetTLO<SwitchType>("SwitchTarget");

        /// <summary>
        /// Is a sub with the given name defined?
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        private readonly IndexedTLO<BoolType> _subDefined;

        /// <summary>
        /// Is a sub with the given name defined?
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsSubDefined(string name) => _subDefined[name];

        /// <summary>
        /// Point merchant that is currently open.
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        public PointMerchantType PointMerchant => GetTLO<PointMerchantType>("PointMerchant");

        /// <summary>
        /// Currently open context menu.
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        public MenuType Menu => GetTLO<MenuType>("Menu");

        /// <summary>
        /// An inventory slot by name or number.
        /// Valid slot numbers are:
        /// - 2000-2015 bank window
        /// - 2500-2503 shared bank
        /// - 5000-5031 loot window
        /// - 3000-3015 trade window (including npc) 3000-3007 are your slots, 3008-3015 are other character's slots
        /// - 4000-4010 world container window
        /// - 6000-6080 merchant window
        /// - 7000-7080 bazaar window
        /// - 8000-8031 inspect window
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        private readonly IndexedTLO<InvSlotType, string, InvSlotType, int> _invSlot;

        /// <summary>
        /// An inventory slot by name.
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public InvSlotType GetInvSlot(string name) => _invSlot[name];

        /// <summary>
        /// An inventory slot by number.
        /// Not documented at https://docs.macroquest.org
        /// </summary>
        /// <param name="slotNumber">Valid slot numbers are:
        /// - 2000-2015 bank window
        /// - 2500-2503 shared bank
        /// - 5000-5031 loot window
        /// - 3000-3015 trade window (including npc) 3000-3007 are your slots, 3008-3015 are other character's slots
        /// - 4000-4010 world container window
        /// - 6000-6080 merchant window
        /// - 7000-7080 bazaar window
        /// - 8000-8031 inspect window</param>
        /// <returns></returns>
        public InvSlotType GetInvSlot(int slotNumber) => _invSlot[slotNumber];

        /// <summary>
        /// Dependency on MQ2Cast.
        /// TODO: Update to latest spec.
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
                var thisType = GetType().DeclaringType ?? GetType();
                var memberType = typeof(T).DeclaringType ?? typeof(T);
                var key = string.IsNullOrWhiteSpace(index) ?
                    $"Data Type: \"{thisType}\" Member Name: \"{name}\" Member Type: \"{memberType}\"" :
                    $"Date Type: \"{thisType}\" Member Name: \"{name}\" Member Index: \"{index}\" Member Type: \"{memberType}\"";

                MQ2DataType.DataTypeErrors.TryAdd(key, ex);

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