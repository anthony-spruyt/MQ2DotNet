namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a solvent.
    /// Last Verified: 2023-07-03
    /// Not documented at https://docs.macroquest.org
    /// </summary>
    [MQ2Type("solventtype")]
    public class SolventType : MQ2DataType
    {
        internal SolventType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Item name
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Item ID
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// <see cref="ItemType"/> for the solvent, if available
        /// </summary>
        public ItemType Item => GetMember<ItemType>("Item");

        /// <summary>
        /// How many we currently have in inventory
        /// </summary>
        public uint? Count => GetMember<IntType>("Count");
    }
}