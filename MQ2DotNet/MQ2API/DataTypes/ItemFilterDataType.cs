using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a filter in advanced loot.
    /// Last Verified: 2023-06-30
    /// https://docs.macroquest.org/reference/top-level-objects/tlo-advloot/#itemfilterdata-type
    /// </summary>
    [PublicAPI]
    [MQ2Type("itemfilterdata")]
    public class ItemFilterDataType : MQ2DataType
    {
        internal ItemFilterDataType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The Name of the item.
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// The ID of the item.
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// The Auto Roll state (dice icon).
        /// </summary>
        public bool AutoRoll => GetMember<BoolType>("AutoRoll");

        /// <summary>
        /// The Need (ND) state.
        /// </summary>
        public bool Need => GetMember<BoolType>("Need");

        /// <summary>
        /// The Greed (GD) state.
        /// </summary>
        public bool Greed => GetMember<BoolType>("Greed");

        /// <summary>
        /// The Never (NV) state.
        /// </summary>
        public bool Never => GetMember<BoolType>("Never");

        /// <summary>
        /// The ID of the icon.
        /// </summary>
        public uint? IconID => GetMember<IntType>("IconID");

        /// <summary>
        /// Bit field representing all the loot filter flags for this item.
        /// TODO: <see cref="EQ.ItemFilterSetting"/> Bitmask 1 = AutoRoll, 2 = Need, 4 = Greed, 8 = Never
        /// </summary>
        public uint? Types => GetMember<IntType>("Types");

        /// <summary>
        /// Same as Name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}