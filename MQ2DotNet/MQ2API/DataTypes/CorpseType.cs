using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data related to the current lootable corpse.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-corpse/
    /// </summary>
    [MQ2Type("corpse")]
    public class CorpseType : SpawnType
    {
        internal CorpseType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _item = new IndexedMember<ItemType, int, ItemType, string>(this, "Item");
        }

        /// <summary>
        /// Corpse open?
        /// </summary>
        public bool Open => GetMember<BoolType>("Open");

        /// <summary>
        /// Nth item on the corpse.
        /// Item[ N ]
        /// Finds an item by partial name in this corpse (use =<name> for exact match).
        /// Item [ name ]
        /// </summary>
        private readonly IndexedMember<ItemType, int, ItemType, string> _item;

        /// <summary>
        /// Nth item on the corpse.
        /// Item[ N ]
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public ItemType GetItem(int nth) => _item[nth];

        /// <summary>
        /// Finds an item by partial name in this corpse (use =<name> for exact match).
        /// Item [ name ]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ItemType GetItem(string name) => _item[name];

        public IEnumerable<ItemType> Items
        {
            get
            {
                var count = Count ?? 0;
                List<ItemType> items = new List<ItemType>((int)count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetItem(i + 1));
                }

                return items;
            }
        }

        /// <summary>
        /// Number of items on the corpse
        /// </summary>
        public uint? Count => GetMember<IntType>("Items");

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