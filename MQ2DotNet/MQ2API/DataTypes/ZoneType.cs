using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains information related to the specified zone.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-zone/
    /// </summary>
    [PublicAPI]
    [MQ2Type("zone")]
    public class ZoneType : MQ2DataType
    {
        internal ZoneType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Name = GetMember<StringType>("Name");
            ShortName = GetMember<StringType>("ShortName");
        }

        /// <summary>
        /// Full zone name e.g. "The Plane of Knowledge"
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Short zone name e.g. "PoKnowledge"
        /// </summary>
        public string ShortName { get; }

        /// <summary>
        /// ID of the zone
        /// </summary>
        public int? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Flags for this zone, see ZONELIST::ZoneFlags in eqdata.h
        /// </summary>
        public long? ZoneFlags => GetMember<Int64Type>("ZoneFlags");

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