﻿using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for the advanced loot window. Moreso the contents than the window itself.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("advloot")]
    public class AdvLootType : MQ2DataType
    {
        internal AdvLootType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            PList = new IndexedMember<AdvLootItemType>(this, "PList");
            SList = new IndexedMember<AdvLootItemType>(this, "SList");
            Filter = new IndexedMember<ItemFilterDataType, int>(this, "Filter");
        }

        /// <summary>
        /// Number of items in the personal loot list
        /// </summary>
        public int? PCount => GetMember<IntType>("PCount");

        /// <summary>
        /// Returns an item from the personal loot list
        /// </summary>
        public IndexedMember<AdvLootItemType> PList { get; }

        /// <summary>
        /// Number of items in the shared loot list
        /// </summary>
        public int? SCount => GetMember<IntType>("SCount");

        /// <summary>
        /// Returns an item from shared loot list
        /// </summary>
        public IndexedMember<AdvLootItemType> SList { get; }

        /// <summary>
        /// Number of items in the personal loot list with either Need, Always Need, Greed, or Always Greed checked
        /// </summary>
        public uint? PWantCount => GetMember<IntType>("PWantCount");

        /// <summary>
        /// Number of items in the shared loot list with either Need, Always Need, Greed, or Always Greed checked
        /// </summary>
        public uint? SWantCount => GetMember<IntType>("SWantCount");

        /// <summary>
        /// True if any item is currently being looted? TODO: Confirm this
        /// </summary>
        public bool LootInProgress => GetMember<BoolType>("LootInProgress");

        /// <summary>
        /// Returns a filter from the advanced loot filters TODO: By what? Number in list or item ID?
        /// </summary>
        public IndexedMember<ItemFilterDataType, int> Filter { get; }

        public override string ToString()
        {
            return nameof(AdvLootType);
        }
    }
}