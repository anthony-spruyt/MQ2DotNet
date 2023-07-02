﻿using JetBrains.Annotations;
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
            _heading = new IndexedTLO<HeadingType, string>(this, "Heading");
            _illusion = new IndexedTLO<KeyRingItemType, string, KeyRingItemType, int>(this, "Illusion");
            _lastSpawn = new IndexedTLO<SpawnType, int>(this, "LastSpawn");
            _lineOfSight = new IndexedTLO<BoolType>(this, "LineOfSight");
            _mount = new IndexedTLO<KeyRingItemType, string, KeyRingItemType, int>(this, "Mount");
            _nearestSpawn = new IndexedTLO<SpawnType, int, SpawnType, string>(this, "NearestSpawn");

            //TODO below









            InvSlot = new IndexedTLO<InvSlotType, string, InvSlotType, int>(this, "InvSlot");
            Plugin = new IndexedTLO<PluginType, string, PluginType, int>(this, "Plugin");
            Skill = new IndexedTLO<SkillType, string, SkillType, int>(this, "Skill");
            Spawn = new IndexedTLO<SpawnType>(this, "Spawn");
            SpawnCount = new IndexedTLO<IntType>(this, "SpawnCount");
            Spell = new IndexedTLO<SpellType, string, SpellType, int>(this, "Spell");
            SubDefined = new IndexedTLO<BoolType>(this, "SubDefined");
            Task = new IndexedTLO<TaskType, string, TaskType, int>(this, "Task");
            Window = new IndexedTLO<WindowType>(this, "Window");
            Zone = new IndexedTLO<ZoneType>(this, "Zone");
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
        public IndexedTLO<KeyRingItemType, string, KeyRingItemType, int> _illusion;

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
        //private IndexedTLO<IntType> _int;

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
        public IndexedTLO<KeyRingItemType, string, KeyRingItemType, int> _mount;

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

        //TODO below


        /// <summary>
        /// Your target
        /// </summary>
        public TargetType Target => GetTLO<TargetType>("Target");
        
        /// <summary>
        /// Your current door target
        /// </summary>
        public SwitchType Switch => GetTLO<SwitchType>("Switch");
        
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
        /// Point merchnat that is currently open
        /// </summary>
        public PointMerchantType PointMerchant => GetTLO<PointMerchantType>("PointMerchant");
        
        /// <summary>
        /// Zone you are currently in
        /// </summary>
        public CurrentZoneType CurrentZone => GetTLO<CurrentZoneType>("Zone");
        
        /// <summary>
        /// First spawn that matches a search string
        /// </summary>
        public IndexedTLO<SpawnType> Spawn;
        
        /// <summary>
        /// Spell by name or ID
        /// </summary>
        public IndexedTLO<SpellType, string, SpellType, int> Spell;
        
        /// <summary>
        /// Window by name
        /// </summary>
        public IndexedTLO<WindowType> Window;
        
        /// <summary>
        /// Zone by ID or short name. For current zone, use <see cref="CurrentZone"/>
        /// </summary>
        public IndexedTLO<ZoneType> Zone;
        
        /// <summary>
        /// Total number of spawns that match a search
        /// </summary>
        public IndexedTLO<IntType> SpawnCount;
        
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
        public IndexedTLO<InvSlotType, string, InvSlotType, int> InvSlot;
        
        /// <summary>
        /// Plugin by name or number
        /// </summary>
        public IndexedTLO<PluginType, string, PluginType, int> Plugin;
        
        /// <summary>
        /// Skill by name or number
        /// </summary>
        public IndexedTLO<SkillType, string, SkillType, int> Skill;
        
        /// <summary>
        /// Task by name or position in window (1 based)
        /// </summary>
        public IndexedTLO<TaskType, string, TaskType, int> Task;
        
        /// <summary>
        /// Requires EXPANSION_LEVEL_TOL
        /// TeleportationItem (on keyring) by name or position in window (1 based). Name is partial match unless it begins with =
        /// </summary>
        public IndexedTLO<KeyRingType, string, KeyRingType, int> TeleportationItem;
        
        /// <summary>
        /// Currently open context menu
        /// </summary>
        public MenuType Menu => GetTLO<MenuType>("Menu");
        
        /// <summary>
        /// Is a sub with the given name defined?
        /// </summary>
        public IndexedTLO<BoolType> SubDefined;
        
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