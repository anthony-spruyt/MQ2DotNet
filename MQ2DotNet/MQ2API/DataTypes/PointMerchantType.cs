using JetBrains.Annotations;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a point merchant.
    /// Last Verified: 2023-07-03
    /// Not documented at https://docs.macroquest.org
    /// </summary>
    [PublicAPI]
    [MQ2Type("pointmerchant")]
    public class PointMerchantType : SpawnType
    {
        public const int MAX_ITEMS = 1000;

        internal PointMerchantType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _item = new IndexedMember<PointMerchantItemType, string, PointMerchantItemType, int>(this, "Item");
        }

        /// <summary>
        /// Item by name or slot number (1 based)
        /// </summary>
        private readonly IndexedMember<PointMerchantItemType, string, PointMerchantItemType, int> _item;

        /// <summary>
        /// Item by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PointMerchantItemType GetItem(string name) => _item[name];

        /// <summary>
        /// Item by slot number (1 based)
        /// </summary>
        /// <param name="index">The base 1 slot number</param>
        /// <returns></returns>
        public PointMerchantItemType GetItem(int index) => _item[index];

        public IEnumerable<PointMerchantItemType> Items
        {
            get
            {
                var index = 1;

                while (index <= MAX_ITEMS)
                {
                    var item = GetItem(index);

                    if (item != null)
                    {
                        index++;

                        yield return item;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}