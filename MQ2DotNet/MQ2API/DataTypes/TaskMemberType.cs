using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Describes a member in your current task.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-taskmember/
    /// </summary>
    [PublicAPI]
    [MQ2Type("taskmember")]
    public class TaskMemberType : MQ2DataType
    {
        internal TaskMemberType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Returns name of the member.
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Returns true if member is leader.
        /// </summary>
        public bool Leader => GetMember<BoolType>("Leader");

        /// <summary>
        /// Returns task index (base 1) for member (i.e., 1-6)
        /// </summary>
        public uint? Index => GetMember<IntType>("Index");
    }
}