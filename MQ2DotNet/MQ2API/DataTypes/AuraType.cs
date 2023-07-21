namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Describes an aura.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-auratype/
    /// </summary>
    [MQ2Type("auratype")]
    public class AuraType : MQ2DataType
    {
        internal AuraType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _find = new IndexedMember<IntType, string>(this, "Find");
        }

        /// <summary>
        /// ID of the Aura.
        /// Appears to be the slot the aura is in. 1 based
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Returns the position of the index if found within the aura's name.
        /// Cast to <see cref="uint"/> to get the value.
        /// Not documented at https://docs.macroquest.org/reference/data-types/datatype-auratype/
        /// </summary>
        private readonly IndexedMember<IntType, string> _find;

        /// <summary>
        /// Returns the position of the index if found within the aura's name.
        /// Not documented at https://docs.macroquest.org/reference/data-types/datatype-auratype/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public uint? GetIndex(string name) => (uint?)_find[name];

        /// <summary>
        /// Name of the aura.
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Spawn ID of the caster / ID of the spawn that emits aura.
        /// </summary>
        public uint? SpawnID => GetMember<IntType>("SpawnID");

        /// <summary>
        /// Remove the aura
        /// </summary>
        public void Remove() => GetMember<MQ2DataType>("Remove");
    }
}