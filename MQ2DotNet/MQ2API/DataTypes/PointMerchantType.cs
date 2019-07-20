﻿using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a point merchant
    /// </summary>
    [PublicAPI]
    public class PointMerchantType : SpawnType
    {
        internal PointMerchantType(MQ2TypeVar typeVar) : base(typeVar)
        {
            Item = new IndexedMember<PointMerchantItemType, string, PointMerchantItemType, int>(this, "Item");
        }

        /// <summary>
        /// Item by name or slot number (1 based)
        /// </summary>
        public IndexedMember<PointMerchantItemType, string, PointMerchantItemType, int> Item { get; }
    }
}