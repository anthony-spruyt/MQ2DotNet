namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an item in a bandolier set.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-bandolier/#bandolieritem-datatype
    /// </summary>
    [MQ2Type("bandolieritem")]
    public class BandolierItemType : MQ2DataType
    {
        internal BandolierItemType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Returns the icon id for the item.
        /// </summary>
        public uint? IconID => GetMember<IntType>("IconID");

        /// <summary>
        /// Returns the item id for the item.
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Returns the name of the item.
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}