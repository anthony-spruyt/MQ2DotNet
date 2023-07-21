namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a discrete item being looted in an AdvLoot window.
    /// Last Verified: 2023-07-19
    /// https://docs.macroquest.org/reference/top-level-objects/tlo-advloot/#advlootitem-type
    /// </summary>
    [MQ2Type("advlootitem")]
    public class AdvLootItemType : MQ2DataType
    {
        internal AdvLootItemType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The positional index of the item.
        /// </summary>
        public uint? Index => GetMember<IntType>("Index");

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// The ID of the item.
        /// </summary>
        public long? ID => GetMember<Int64Type>("ID");

        /// <summary>
        /// The size of the stack of items being looted.
        /// </summary>
        public uint? StackSize => GetMember<IntType>("StackSize");

        /// <summary>
        /// The spawn representing the corpse that is being looted, if available.
        /// </summary>
        public SpawnType Corpse => GetMember<SpawnType>("Corpse");

        /// <summary>
        /// The Auto Roll state (dice icon) of the item.
        /// </summary>
        public bool AutoRoll => GetMember<BoolType>("AutoRoll");

        /// <summary>
        /// The Need (ND) state of the item.
        /// </summary>
        public bool Need => GetMember<BoolType>("Need");

        /// <summary>
        /// The Greed (GD) state of the item.
        /// </summary>
        public bool Greed => GetMember<BoolType>("Greed");

        /// <summary>
        /// The No state of the item.
        /// </summary>
        public bool No => GetMember<BoolType>("No");

        /// <summary>
        /// The Always Need (AN) state of the item.
        /// </summary>
        public bool AlwaysNeed => GetMember<BoolType>("AlwaysNeed");

        /// <summary>
        /// The Always Greed (AG) state of the item.
        /// </summary>
        public bool AlwaysGreed => GetMember<BoolType>("AlwaysGreed");

        /// <summary>
        /// The Never (NV) state of the item.
        /// </summary>
        public bool Never => GetMember<BoolType>("Never");

        /// <summary>
        /// The ID of the icon for the item.
        /// </summary>
        public uint? IconID => GetMember<IntType>("IconID");

        /// <summary>
        /// Indicates if the item is NO DROP.
        /// </summary>
        public bool NoDrop => GetMember<BoolType>("NoDrop");

        /// <summary>
        /// TODO: doco new member 2023-07-19
        /// </summary>
        public bool FreeGrab => GetMember<BoolType>("FreeGrab");

        /// <summary>
        /// Same as <see cref="Name"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}