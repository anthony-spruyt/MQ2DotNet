using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents an 8 bit integer, with values randing from 0 to 255. This is a pure DataType and has no members.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-byte/
    /// </summary>
    [PublicAPI]
    [MQ2Type("byte")]
    public class ByteType : MQ2DataType
    {
        internal ByteType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Implicit conversion to a byte
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator byte(ByteType typeVar)
        {
            return (byte) (typeVar.VarPtr.Dword & 0xFF);
        }
    }
}