﻿using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a corpse
    /// </summary>
    [PublicAPI]
    public class CorpseType : SpawnType
    {
        internal CorpseType(MQ2TypeVar typeVar) : base(typeVar)
        {
            Item = new IndexedMember<ItemType, int, ItemType, string>(this, "Item");
        }

        /// <summary>
        /// Corpse open?
        /// </summary>
        public bool Open => GetMember<BoolType>("Open");

        /// <summary>
        /// An item on the corpse by name or number
        /// </summary>
        public IndexedMember<ItemType, int, ItemType, string> Item;

        /// <summary>
        /// Number of items on the corpse
        /// </summary>
        public int? Items => GetMember<IntType>("Items");
    }
}