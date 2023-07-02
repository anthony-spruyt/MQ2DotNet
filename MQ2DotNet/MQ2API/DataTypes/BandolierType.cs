using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Used to access information about bandolier sets on your character.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-bandolier/
    /// </summary>
    [PublicAPI]
    [MQ2Type("bandolier")]
    public class BandolierType : MQ2DataType
    {
        internal BandolierType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _item = new IndexedMember<BandolierItemType, int>(this, "Item");
        }

        /// <summary>
        /// Indicates if the bandolier set is active.
        /// </summary>
        public bool? Active => GetMember<BoolType>("Active");

        /// <summary>
        /// Returns the index (base 1) number of the bandolier set.
        /// </summary>
        public uint? Index => GetMember<IntType>("Index");

        /// <summary>
        /// Provides information about the specified item. Returns the Nth item (base 1) in the set (Primary, Secondary, Ranged, Ammo).
        /// </summary>
        private IndexedMember<BandolierItemType, int> _item;

        /// <summary>
        /// Provides information about the specified item. Returns the Nth item (base 1) in the set (Primary, Secondary, Ranged, Ammo).
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public BandolierItemType GetItem(int index) => _item[index];

        /// <summary>
        /// Provides information about the specified item.
        /// </summary>
        /// <returns></returns>
        public BandolierItemType GetPrimary() => _item[1];

        /// <summary>
        /// Provides information about the specified item.
        /// </summary>
        /// <returns></returns>
        public BandolierItemType GetSecondary() => _item[2];

        /// <summary>
        /// Provides information about the specified item.
        /// </summary>
        /// <returns></returns>
        public BandolierItemType GetRanged() => _item[3];

        /// <summary>
        /// Provides information about the specified item.
        /// </summary>
        /// <returns></returns>
        public BandolierItemType GetAmmo() => _item[4];

        /// <summary>
        /// Returns the name of the bandolier set.
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Activate the bandolier profile.
        /// </summary>
        public void Activate() => GetMember<MQ2DataType>("Activate");
    }
}