using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an aura.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("auratype")]
    public class AuraType : MQ2DataType
    {
        internal AuraType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Find = new IndexedMember<IntType, string>(this, "Find");
        }

        /// <summary>
        /// Appears to be the slot the aura is in. 1 based
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Returns the position of the index if found within the aura's name.
        /// The value is stored in the <see cref="IntType.Dword"/> property.
        /// </summary>
        public IndexedMember<IntType, string> Find { get; }

        /// <summary>
        /// Name of the aura
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Spawn ID of the caster
        /// </summary>
        public uint? SpawnID => GetMember<IntType>("SpawnID");

        /// <summary>
        /// Remove the aura
        /// </summary>
        public void Remove() => GetMember<MQ2DataType>("Remove");
    }
}