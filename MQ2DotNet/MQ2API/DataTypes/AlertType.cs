using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an alert list (a list of spawn searches)
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("alert")]
    public class AlertType : MQ2DataType
    {
        internal AlertType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            List = new IndexedMember<AlertListType, int>(this, "List");
        }

        /// <summary>
        /// Information about a spawn search on the alert list (0 based)
        /// </summary>
        public IndexedMember<AlertListType, int> List { get; }

        /// <summary>
        /// Number of spawn searches on the alert list
        /// </summary>
        public uint? Size => GetMember<IntType>("Size");
    }
}