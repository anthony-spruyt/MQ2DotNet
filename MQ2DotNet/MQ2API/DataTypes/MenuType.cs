using JetBrains.Annotations;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a context menu.
    /// Last Verified: 2023-07-03
    /// Not documented at https://docs.macroquest.org
    /// </summary>
    [PublicAPI]
    [MQ2Type("menu")]
    public class MenuType : MQ2DataType
    {
        internal MenuType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _items = new IndexedMember<StringType, int>(this, "Items");
        }
        
        /// <summary>
        /// Select the first item in the context menu with text containg a given string
        /// </summary>
        /// <param name="containing">Text to search for</param>
        /// <returns>true if an item was found, otherwise false</returns>
        public bool Select(string containing) => GetMember<BoolType>("Select", containing);
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? NumVisibleMenus => GetMember<IntType>("NumVisibleMenus");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? CurrMenu => GetMember<IntType>("CurrMenu");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public string Name => GetMember<StringType>("Name");
        
        /// <summary>
        /// TODO: What is this?
        /// </summary>
        public uint? NumItems => GetMember<IntType>("NumItems");

        /// <summary>
        /// TODO: What is this?
        /// Looks like it is base 1 index from the MQ2 client source.
        /// </summary>
        private readonly IndexedMember<StringType, int> _items;

        /// <summary>
        /// TODO: What is this?
        /// Looks like it is base 1 index from the MQ2 client source.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetItem(int index) => _items[index];

        public IEnumerable<string>  Items
        {
            get
            {
                var count = (int?)NumItems ?? 0;

                for (int i = 0; i < count; i++)
                {
                    yield return GetItem(i);
                }
            }
        }
    }
}