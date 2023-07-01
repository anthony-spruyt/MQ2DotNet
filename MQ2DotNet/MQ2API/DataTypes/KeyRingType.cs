using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// This datatype represents information about a keyring (a.k.a. a collection of mounts, illusions, etc)
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-keyring/
    /// </summary>
    [PublicAPI]
    [MQ2Type("keyring")]
    public class KeyRingType : MQ2DataType
    {
        internal KeyRingType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The number of items in this keyring
        /// </summary>
        public uint? Count => GetMember<IntType>("Count");

        /// <summary>
        /// The keyring item assigned as the stat item
        /// </summary>
        public KeyRingItemType Stat => GetMember<KeyRingItemType>("Stat");

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}