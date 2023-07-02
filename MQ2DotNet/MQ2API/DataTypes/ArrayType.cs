using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data related to arrays. Not well supported.
    /// Note: Array indexing starts at 1.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-array/
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

        // There is also a Size[ N ]	Total number of elements stored in the N th dimension of the array version. Wont bother with this here.

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}