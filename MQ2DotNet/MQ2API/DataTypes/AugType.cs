using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Describes data about an augmentation slot in an item.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-augtype/
    /// </summary>
    [PublicAPI]
    [MQ2Type("augtype")]
    public class AugType : MQ2DataType
    {
        internal AugType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Name = GetMember<StringType>("Name");
        }

        /// <summary>
        /// Index of the augment slot.
        /// </summary>
        public uint? Slot => GetMember<IntType>("Slot");

        /// <summary>
        /// Type of augment slot.
        /// </summary>
        public uint? Type => GetMember<IntType>("Type");

        /// <summary>
        /// True if this slot is visible to the user.
        /// </summary>
        public bool Visible => GetMember<BoolType>("Visible");

        /// <summary>
        /// True if this is a hidden energeian power source slot.
        /// </summary>
        public bool Infusable => GetMember<BoolType>("Infusable");

        /// <summary>
        /// True if the slot is empty
        /// </summary>
        public bool Empty => GetMember<BoolType>("Empty");

        /// <summary>
        /// The name of the item socketed in this slot, if any.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The item socketed in this slot, if any.
        /// </summary>
        public ItemType Item => GetMember<ItemType>("Item");

        /// <summary>
        /// Solvent required to remove the augmentation
        /// </summary>
        public SolventType Solvent => GetMember<SolventType>("Solvent");
    }
}