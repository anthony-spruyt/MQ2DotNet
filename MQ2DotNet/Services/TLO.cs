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
    /// Last Verified: 2023-06-30 WIP...
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

            //AlertByNumber = new IndexedTLO<AlertType>(this, "Alert");
            //Defined = new IndexedTLO<BoolType>(this, "Defined");
            //Familiar = new IndexedTLO<KeyRingType, string, KeyRingType, int>(this, "Familiar");
            //FindItem = new IndexedTLO<ItemType>(this, "FindItem");
            //FindItemBank = new IndexedTLO<ItemType>(this, "FindItemBank");
            //FindItemBankCount = new IndexedTLO<IntType>(this, "FindItemBankCount");
            //FindItemCount = new IndexedTLO<IntType>(this, "FindItemCount");
            //GroundItem = new IndexedTLO<GroundType>(this, "GroundItem");
            //GroundItemCount = new IndexedTLO<IntType>(this, "GroundItemCount");
            //Heading = new IndexedTLO<HeadingType, string>(this, "");
            //Illusion = new IndexedTLO<KeyRingType, string, KeyRingType, int>(this, "Illusion");
            //InvSlot = new IndexedTLO<InvSlotType, string, InvSlotType, int>(this, "InvSlot");
            //LineOfSight = new IndexedTLO<BoolType>(this, "LineOfSight");
            //Mount = new IndexedTLO<KeyRingType, string, KeyRingType, int>(this, "Mount");
            //NearestSpawn = new IndexedTLO<SpawnType>(this, "NearestSpawn");
            //Plugin = new IndexedTLO<PluginType, string, PluginType, int>(this, "Plugin");
            //Skill = new IndexedTLO<SkillType, string, SkillType, int>(this, "Skill");
            //Spawn = new IndexedTLO<SpawnType>(this, "Spawn");
            //SpawnCount = new IndexedTLO<IntType>(this, "SpawnCount");
            //Spell = new IndexedTLO<SpellType, string, SpellType, int>(this, "Spell");
            //SubDefined = new IndexedTLO<BoolType>(this, "SubDefined");
            //Task = new IndexedTLO<TaskType, string, TaskType, int>(this, "Task");
            //Window = new IndexedTLO<WindowType>(this, "Window");
            //Zone = new IndexedTLO<ZoneType>(this, "Zone");
            //LastSpawn = new IndexedTLO<SpawnType, int>(this, "LastSpawn");
        }

        /// <summary>
        /// TODO: NOT IMPLEMENTED YET!! Use the text API => ${Achievement[Master of Claws of Veeshan].ID} 
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
        public IEnumerable<int> AlertIDs => ((string)_alert[""])?.Split('|').Select(id => int.Parse(id));

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
        /// Look up an AltAbility by its altability id
        /// AltAbility[ Number ]
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-altability/#forms
        /// </summary>
        /// <param name="altabilityID"></param>
        /// <returns></returns>
        public AltAbilityType GetAltAbility(int altabilityID) => _altAbility[altabilityID];

        /// <summary>
        /// Danger: The AltAbility TLO should not be used except for when experimenting with data. If you've already purchased the AA, use <see cref="CharacterType._altAbility"/>, which is tailored to your character and is much faster.
        /// Look up an AltAbility by its name
        /// AltAbility[ Name ]
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-altability/#forms
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AltAbilityType GetAltAbility(string name) => _altAbility[name];

        /// <summary>
        /// Character object which allows you to get properties of you as a character.
        /// https://docs.macroquest.org/reference/top-level-objects/tlo-me/
        /// </summary>
        public CharacterType Me => GetTLO<CharacterType>("Me");

        //
        ///// <summary>
        ///// Your target
        ///// </summary>
        //public TargetType Target => GetTLO<TargetType>("Target");
        //
        ///// <summary>
        ///// Your current door target
        ///// </summary>
        //public SwitchType Switch => GetTLO<SwitchType>("Switch");
        //
        ///// <summary>
        ///// Your mercenary
        ///// </summary>
        //public MercenaryType Mercenary => GetTLO<MercenaryType>("Mercenary");
        //
        ///// <summary>
        ///// Your pet
        ///// </summary>
        //public PetType Pet => GetTLO<PetType>("Pet");
        //
        ///// <summary>
        ///// Merchant that is currently open
        ///// </summary>
        //public MerchantType Merchant => GetTLO<MerchantType>("Merchant");
        //
        ///// <summary>
        ///// Corpse that is currently open
        ///// </summary>
        //public CorpseType Corpse => GetTLO<CorpseType>("Corpse");
        //
        ///// <summary>
        ///// Macro that is running
        ///// </summary>
        //public MacroType Macro => GetTLO<MacroType>("Macro");
        //
        ///// <summary>
        ///// <see cref="MacroQuestType"/> instance
        ///// </summary>
        //public MacroQuestType MacroQuest => GetTLO<MacroQuestType>("MacroQuest");
        //
        ///// <summary>
        ///// <see cref="EverQuestType"/> instance
        ///// </summary>
        //public EverQuestType EverQuest => GetTLO<EverQuestType>("EverQuest");
        //
        ///// <summary>
        ///// <see cref="GroupType"/> instance
        ///// </summary>
        //public GroupType Group => GetTLO<GroupType>("Group");
        //
        ///// <summary>
        ///// Item on your cursor
        ///// </summary>
        //public ItemType Cursor => GetTLO<ItemType>("Cursor");
        //
        ///// <summary>
        ///// Current in game time
        ///// </summary>
        //public TimeType GameTime => GetTLO<TimeType>("GameTime");
        //
        ///// <summary>
        ///// TODO: What does SelectedItem give?
        ///// </summary>
        //public ItemType SelectedItem => GetTLO<ItemType>("SelectedItem");
        //
        ///// <summary>
        ///// <see cref="RaidType"/> instance
        ///// </summary>
        //public RaidType Raid => GetTLO<RaidType>("Raid");
        //
        ///// <summary>
        ///// Spawn whose name is currently being drawn
        ///// </summary>
        //public SpawnType NamingSpawn => GetTLO<SpawnType>("NamingSpawn");
        //
        ///// <summary>
        ///// Your current door target
        ///// </summary>
        //public SpawnType DoorTarget => GetTLO<SpawnType>("DoorTarget");
        //
        ///// <summary>
        ///// Your current item target
        ///// </summary>
        //public SpawnType ItemTarget => GetTLO<SpawnType>("ItemTarget");
        //
        ///// <summary>
        ///// <see cref="DynamicZoneType"/> instance
        ///// </summary>
        //public DynamicZoneType DynamicZone => GetTLO<DynamicZoneType>("DynamicZone");
        //
        ///// <summary>
        ///// <see cref="FriendsType"/> instance
        ///// </summary>
        //public FriendsType Friends => GetTLO<FriendsType>("Friends");
        //
        ///// <summary>
        ///// Point merchnat that is currently open
        ///// </summary>
        //public PointMerchantType PointMerchant => GetTLO<PointMerchantType>("PointMerchant");
        //
        ///// <summary>
        ///// Zone you are currently in
        ///// </summary>
        //public CurrentZoneType CurrentZone => GetTLO<CurrentZoneType>("Zone");
        //
        ///// <summary>
        ///// Heading to a location in y,x format.
        ///// TODO: I think this is incorrect and shoud be an IndexedTLO so converted it but havent tested yet...
        ///// </summary>
        ////public HeadingType Heading => GetTLO<HeadingType>("Heading");
        //[JsonIgnore]
        //public IndexedTLO<HeadingType, string> Heading { get; }
        //
        ///// <summary>
        ///// First spawn that matches a search string
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<SpawnType> Spawn { get; }
        //
        ///// <summary>
        ///// Spell by name or ID
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<SpellType, string, SpellType, int> Spell { get; }
        //
        ///// <summary>
        ///// Ground item by name (partial match), or your current ground target if an empty index is supplied
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<GroundType> GroundItem { get; }
        //
        ///// <summary>
        ///// Number of ground items by name (partial match), or total number of ground items if an empty index is supplied
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<IntType> GroundItemCount { get; }
        //
        ///// <summary>
        ///// Window by name
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<WindowType> Window { get; }
        //
        ///// <summary>
        ///// Zone by ID or short name. For current zone, use <see cref="CurrentZone"/>
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<ZoneType> Zone { get; }
        //
        ///// <summary>
        ///// Spawn by position in the list, from the end for negative numbers
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<SpawnType, int> LastSpawn { get; }
        //
        ///// <summary>
        ///// Nth nearest spawn that matches a search e.g. "2,npc" for the 2nd closest NPC
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<SpawnType> NearestSpawn { get; }
        //
        ///// <summary>
        ///// Total number of spawns that match a search
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<IntType> SpawnCount { get; }
        //
        ///// <summary>
        ///// Is a variable by the given name defined?
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<BoolType> Defined { get; }
        //
        ///// <summary>
        ///// Item by name, partial match unless it begins with an = e.g. "=Water Flask"
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<ItemType> FindItem { get; }
        //
        ///// <summary>
        ///// An item in your bank, partial match unless it begins with an = e.g. "=Water Flask"
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<ItemType> FindItemBank { get; }
        //
        ///// <summary>
        ///// Total number of an item you have, partial match unless it begins with an = e.g. "=Water Flask"
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<IntType> FindItemCount { get; }
        //
        ///// <summary>
        ///// Total number of an item you have in your bank, partial match unless it begins with an = e.g. "=Water Flask"
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<IntType> FindItemBankCount { get; }
        //
        ///// <summary>
        ///// An inventory slot by name or number
        ///// </summary>
        ///// <remarks>Valid slot numbers are:
        ///// 2000-2015 bank window
        ///// 2500-2503 shared bank
        ///// 5000-5031 loot window
        ///// 3000-3015 trade window (including npc) 3000-3007 are your slots, 3008-3015 are other character's slots
        ///// 4000-4010 world container window
        ///// 6000-6080 merchant window
        ///// 7000-7080 bazaar window
        ///// 8000-8031 inspect window</remarks>
        //[JsonIgnore]
        //public IndexedTLO<InvSlotType, string, InvSlotType, int> InvSlot { get; }
        //
        ///// <summary>
        ///// Plugin by name or number
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<PluginType, string, PluginType, int> Plugin { get; }
        //
        ///// <summary>
        ///// Skill by name or number
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<SkillType, string, SkillType, int> Skill { get; }
        //
        ///// <summary>
        ///// Is there line of sight between two locations, in the format "y,x,z:y,x,z"
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<BoolType> LineOfSight { get; }
        //
        ///// <summary>
        ///// Task by name or position in window (1 based)
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<TaskType, string, TaskType, int> Task { get; }
        //
        ///// <summary>
        ///// Mount (on keyring) by name or position in window (1 based). Name is partial match unless it begins with =
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<KeyRingType, string, KeyRingType, int> Mount { get; }
        //
        ///// <summary>
        ///// Illusion (on keyring) by name or position in window (1 based). Name is partial match unless it begins with =
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<KeyRingType, string, KeyRingType, int> Illusion { get; }
        //
        ///// <summary>
        ///// Familiar (on keyring) by name or position in window (1 based). Name is partial match unless it begins with =
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<KeyRingType, string, KeyRingType, int> Familiar { get; }
        //
        ///// <summary>
        ///// Requires EXPANSION_LEVEL_TOL
        ///// TeleportationItem (on keyring) by name or position in window (1 based). Name is partial match unless it begins with =
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<KeyRingType, string, KeyRingType, int> TeleportationItem { get; }
        //
        ///// <summary>
        ///// Is an alias set for a command, including the slash e.g. Alias["/chaseon"]
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<BoolType> Alias { get; }
        //
        ///// <summary>
        ///// An alert list by number
        ///// For the equivalent of ${Alert}, see <see cref="Alerts"/>
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<AlertType> AlertByNumber { get; }
        //
        ///// <summary>
        ///// Currently open context menu
        ///// </summary>
        //public MenuType Menu => GetTLO<MenuType>("Menu");
        //
        ///// <summary>
        ///// Is a sub with the given name defined?
        ///// </summary>
        //[JsonIgnore]
        //public IndexedTLO<BoolType> SubDefined { get; }
        //
        ///// <summary>
        ///// Dependency on MQ2Cast.
        ///// </summary>
        //public CastType Cast => GetTLO<CastType>("Cast");

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