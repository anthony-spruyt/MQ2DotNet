﻿using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an item in the advanced loot window
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("advlootitem")]
    public class AdvLootItemType : MQ2DataType
    {
        internal AdvLootItemType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Index of the item in either the shared or personal loot list
        /// </summary>
        public uint? Index => GetMember<IntType>("Index");

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// ID of the item
        /// </summary>
        public long? ID => GetMember<Int64Type>("ID");

        /// <summary>
        /// Maximum number of these items in one stack
        /// </summary>
        public uint? StackSize => GetMember<IntType>("StackSize");

        /// <summary>
        /// Spawn the item was dropped by
        /// </summary>
        public SpawnType Corpse => GetMember<SpawnType>("Corpse");

        /// <summary>
        /// State of the Autoroll checkbox for this item
        /// </summary>
        public bool AutoRoll => GetMember<BoolType>("AutoRoll");

        /// <summary>
        /// State of the Need checkbox for this item
        /// </summary>
        public bool Need => GetMember<BoolType>("Need");

        /// <summary>
        /// State of the Greed checkbox for this item
        /// </summary>
        public bool Greed => GetMember<BoolType>("Greed");

        /// <summary>
        /// State of the No checkbox for this item
        /// </summary>
        public bool No => GetMember<BoolType>("No");

        /// <summary>
        /// State of the AlwaysNeed checkbox for this item
        /// </summary>
        public bool AlwaysNeed => GetMember<BoolType>("AlwaysNeed");

        /// <summary>
        /// State of the AlwaysGreed checkbox for this item
        /// </summary>
        public bool AlwaysGreed => GetMember<BoolType>("AlwaysGreed");

        /// <summary>
        /// State of the Never checkbox for this item
        /// </summary>
        public bool Never => GetMember<BoolType>("Never");

        /// <summary>
        /// ID of the icon for this item
        /// </summary>
        public uint? IconID => GetMember<IntType>("IconID");

        /// <summary>
        /// True if the item is no drop
        /// </summary>
        public bool NoDrop => GetMember<BoolType>("NoDrop");
    }
}