using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// The AdvLoot TLO grants access to items in the Advanced Loot window.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/top-level-objects/tlo-advloot/
    /// </summary>
    [MQ2Type("advloot")]
    public class AdvLootType : MQ2DataType
    {
        internal AdvLootType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _pList = new IndexedMember<AdvLootItemType, int>(this, "PList");
            _sList = new IndexedMember<AdvLootItemType, int>(this, "SList");
            _filter = new IndexedMember<ItemFilterDataType, int>(this, "Filter");
        }

        /// <summary>
        /// Inspect the item at the specified index in the personal loot list.
        /// PList[ Index ]
        /// </summary>
        public int? PersonalCount => GetMember<IntType>("PCount");

        /// <summary>
        /// Returns an item from the personal loot list
        /// </summary>
        private readonly IndexedMember<AdvLootItemType, int> _pList;

        /// <summary>
        /// Inspect the item at the specified index in the personal loot list.
        /// PList[ Index ]
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public AdvLootItemType GetPersonalListItem(int index) => _pList[index];

        public IEnumerable<AdvLootItemType> PersonalList
        {
            get
            {
                var count = PersonalCount.GetValueOrDefault(0);
                List<AdvLootItemType> items = new List<AdvLootItemType>(count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetPersonalListItem(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// Item count from the Shared list
        /// </summary>
        public int? SharedCount => GetMember<IntType>("SCount");

        /// <summary>
        /// Inspect the item at the specified index in the shared loot list.
        /// SList[ Index ]
        /// </summary>
        private readonly IndexedMember<AdvLootItemType, int> _sList;

        /// <summary>
        /// Inspect the item at the specified index in the shared loot list.
        /// SList[ Index ]
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public AdvLootItemType GetSharedListItem(int index) => _sList[index];

        public IEnumerable<AdvLootItemType> SharedList
        {
            get
            {
                var count = SharedCount.GetValueOrDefault(0);
                List<AdvLootItemType> items = new List<AdvLootItemType>(count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetSharedListItem(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// Want count from the Personal list (AN + AG + ND + GD)
        /// </summary>
        public uint? PWantCount => GetMember<IntType>("PWantCount");

        /// <summary>
        /// Want count from the Shared list (AN + AG + ND + GD)
        /// </summary>
        public uint? SWantCount => GetMember<IntType>("SWantCount");

        /// <summary>
        /// True/False if looting from AdvLoot is in progress
        /// </summary>
        public bool LootInProgress => GetMember<BoolType>("LootInProgress");

        /// <summary>
        /// Inspect the loot filter for a given ItemID.
        /// Filter[ ItemID ]
        /// </summary>
        private readonly IndexedMember<ItemFilterDataType, int> _filter;

        /// <summary>
        /// Inspect the loot filter for a given ItemID.
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public ItemFilterDataType GetItemById(int itemID) => _filter[itemID];

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}