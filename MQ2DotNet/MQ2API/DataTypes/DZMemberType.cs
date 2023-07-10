using JetBrains.Annotations;
using MQ2DotNet.EQ;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// This DataType contains information on the members of the current dynamic zone instance.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-dzmember/
    /// </summary>
    [PublicAPI]
    [MQ2Type("dzmember")]
    public class DZMemberType : MQ2DataType
    {
        internal DZMemberType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Name = GetMember<StringType>("Name");
        }

        /// <summary>
        /// The name of the member
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Returns true if the dzmember can successfully enter the dz.
        /// </summary>
        public bool Flagged => GetMember<BoolType>("Flagged");

        /// <summary>
        /// The status of the member - one of the following: Unknown, Online, Offline, In Dynamic Zone, Link Dead.
        /// TODO: test enum conversion.
        /// </summary>
        public DZStatus? Status => GetMember<StringType>("Status");

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