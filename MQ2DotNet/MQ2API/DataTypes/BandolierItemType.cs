using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an item in a bandolier set.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("bandolieritem")]
    public class BandolierItemType : MQ2DataType
    {
        internal BandolierItemType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Icon ID of the item
        /// </summary>
        public uint? IconID => GetMember<IntType>("IconID");

        /// <summary>
        /// Item ID
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name => GetMember<StringType>("Name");
    }
}