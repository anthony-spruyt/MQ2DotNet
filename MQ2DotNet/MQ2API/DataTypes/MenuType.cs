using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a context menu.
    /// Last Verified: 2023-06-27
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
        /// </summary>
        private IndexedMember<StringType, int> _items;
    }
}