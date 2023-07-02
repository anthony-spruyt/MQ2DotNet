using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an evolving item.
    /// Last Verified: 2023-07-03
    /// Not documented at https://docs.macroquest.org
    /// </summary>
    [PublicAPI]
    [MQ2Type("Evolving")]
    public class EvolvingItemType : MQ2DataType
    {
        internal EvolvingItemType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Is experience enabled?
        /// </summary>
        public bool ExpOn => GetMember<BoolType>("ExpOn");

        /// <summary>
        /// Current percentage experience
        /// </summary>
        public float? ExpPct => GetMember<FloatType>("ExpPct");

        /// <summary>
        /// Current level of the item
        /// </summary>
        public int? Level => GetMember<IntType>("Level");

        /// <summary>
        /// Maximum level of the item
        /// </summary>
        public int? MaxLevel => GetMember<IntType>("MaxLevel");
    }
}