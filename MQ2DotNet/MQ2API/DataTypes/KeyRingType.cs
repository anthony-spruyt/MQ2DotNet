using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a keyring.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("keyring")]
    public class KeyRingType : MQ2DataType
    {
        internal KeyRingType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? Count => GetMember<IntType>("Count");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public KeyRingItemType Stat => GetMember<KeyRingItemType>("Stat");
    }
}