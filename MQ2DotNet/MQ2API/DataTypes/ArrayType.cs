using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 array type. Not well supported.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("array")]
    public class ArrayType : MQ2DataType
    {
        internal ArrayType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        public uint? Dimensions => GetMember<IntType>("Dimensions");

        public uint? Size => GetMember<IntType>("Size");

        public override string ToString()
        {
            return nameof(ArrayType);
        }
    }
}