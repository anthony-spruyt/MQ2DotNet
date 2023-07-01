using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// This datatype deals strictly with information items on a keyring.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-keyringitem/
    /// </summary>
    [PublicAPI]
    [MQ2Type("keyringitem")]
    public class KeyRingItemType : MQ2DataType
    {
        internal KeyRingItemType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Where on the keyring list (1 based)
        /// </summary>
        public uint? Index => GetMember<IntType>("Index");

        /// <summary>
        /// Name of the keyring item
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// The item on the keyring
        /// </summary>
        public ItemType Item => GetMember<ItemType>("Item");
    }
}