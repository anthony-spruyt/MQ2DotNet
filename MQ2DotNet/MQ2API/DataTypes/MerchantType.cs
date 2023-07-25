using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// This contains information related to the active merchant.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-merchant/
    /// </summary>
    [MQ2Type("merchant")]
    public class MerchantType : SpawnType
    {
        internal MerchantType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _item = new IndexedMember<ItemType, int, ItemType, string>(this, "Item");
        }

        /// <summary>
        /// Select item by partial name match, case insensitive. Prefix name with = for EXACT match
        /// </summary>
        /// <param name="itemName">The name to match. No need to prefix with "=" for exact match, set the partialMatch param to false.</param>
        /// <param name="partialMatch">Optional partial match parameter. Default is true. If false an exact case insensitive match is required.</param>
        public void SelectItem(string itemName, bool partialMatch = true) => GetMember<MQ2DataType>("SelectItem", partialMatch ? itemName : $"={itemName}");

        /// <summary>
        /// Buys # of whatever is selected with <see cref="SelectItem(string)"/>
        /// </summary>
        /// <param name="quantity"></param>
        public void Buy(int quantity) => GetMember<MQ2DataType>("Buy", quantity.ToString());

        /// <summary>
        /// Sell count of selected item.
        /// </summary>
        /// <param name="quantity"></param>
        public void Sell(int quantity) => GetMember<MQ2DataType>("Sell", quantity.ToString());

        /// <summary>
        /// Will open the merchant closest to you, or if you have a merchant target.
        /// </summary>
        public void OpenWindow() => GetMember<MQ2DataType>("OpenWindow");

        /// <summary>
        /// Will close the merchant window.
        /// </summary>
        public void CloseWindow() => GetMember<MQ2DataType>("CloseWindow");

        /// <summary>
        /// Returns True if the merchant window is open.
        /// </summary>
        public bool Open => GetMember<BoolType>("Open");

        /// <summary>
        /// True if the merchant's item list has been populated.
        /// </summary>
        public bool ItemsReceived => GetMember<BoolType>("ItemsReceived");

        /// <summary>
        /// Item number # on the merchant's list (1 based).
        /// Item[ # ]
        /// 
        /// Find an item by partial name on the merchant's list. Prefix with "=" for an exact match.
        /// Item[name]
        /// </summary>
        private readonly IndexedMember<ItemType, int, ItemType, string> _item;

        /// <summary>
        /// Item number # on the merchant's list (1 based).
        /// Item[ # ]
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public ItemType GetItem(int nth) => _item[nth];

        /// <summary>
        /// Find an item by partial name on the merchant's list. Prefix with "=" for an exact match.
        /// Item[name]
        /// </summary>
        /// <param name="itemName">The name to match. No need to prefix with "=" for exact match, set the partialMatch param to false.</param>
        /// <param name="partialMatch">Optional partial match parameter. Default is true. If false an exact case insensitive match is required.</param>
        public ItemType GetItem(string itemName, bool partialMatch = true) => _item[partialMatch ? itemName : $"={itemName}"];

        /// <summary>
        /// All of the merchant items.
        /// </summary>
        public IEnumerable<ItemType> Stock
        {
            get
            {
                var count = (int)Items.GetValueOrDefault(0u);

                for (int i = 0; i < count; i++)
                {
                    yield return GetItem(i + 1);
                }
            }
        }

        /// <summary>
        /// Number of items on the merchant.
        /// </summary>
        public uint? Items => GetMember<IntType>("Items");

        /// <summary>
        /// The currently selected item in the merchant window. Items can be selected by using <see cref="SelectItem(string)"/>
        /// </summary>
        public ItemType SelectedItem => GetMember<ItemType>("SelectedItem");

        /// <summary>
        /// The number used to calculate the buy and sell value for an item. (This is what is changed by charisma and faction). This value is capped at 1.05.
        /// Markup * Item Value = Amount you buy item for
        /// Item Value * (1/Markup) = Amount you sell item for
        /// </summary>
        public float? Markup => GetMember<FloatType>("Markup");

        /// <summary>
        /// Returns True if the merchant's inventory is full.
        /// </summary>
        public bool Full => GetMember<BoolType>("Full");

        /// <summary>
        /// Same as <see cref="Open"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}