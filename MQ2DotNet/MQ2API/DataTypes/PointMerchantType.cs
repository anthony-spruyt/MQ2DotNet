using JetBrains.Annotations;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a point merchant.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("pointmerchant")]
    public class PointMerchantType : SpawnType
    {
        internal PointMerchantType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _item = new IndexedMember<PointMerchantItemType, string, PointMerchantItemType, int>(this, "Item");
        }

        /// <summary>
        /// Item by name or slot number (1 based)
        /// </summary>
        private IndexedMember<PointMerchantItemType, string, PointMerchantItemType, int> _item;

        public PointMerchantItemType GetItem(string name) => _item[name];

        public PointMerchantItemType GetItem(int index) => _item[index];

        public IEnumerable<PointMerchantItemType> Items
        {
            get
            {
                var items = new List<PointMerchantItemType>();
                var index = 1;

                while (true)
                {
                    var item = GetItem(index);

                    if (item != null && index <= 10000)
                    {
                        items.Add(item);
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }

                return items;
            }
        }
    }
}