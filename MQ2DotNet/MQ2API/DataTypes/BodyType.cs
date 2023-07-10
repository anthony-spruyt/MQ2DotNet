using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains data about spawn body types.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-body/
    /// </summary>
    [PublicAPI]
    [MQ2Type("body")]
    public class BodyType : MQ2DataType
    {
        internal BodyType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Name = GetMember<StringType>("Name");
        }

        /// <summary>
        /// The ID of the body type.
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// The full name of the body type e.g. Humanoid
        /// </summary>
        public string Name { get; }

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