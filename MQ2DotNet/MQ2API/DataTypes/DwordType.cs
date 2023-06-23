using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a unsigned 32 bit integer
    /// </summary>
    [PublicAPI]
    [MQ2Type("dword")]
    public class DwordType : MQ2DataType
    {
        internal DwordType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Implicit conversion to a nullable long
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator UInt32?(DwordType typeVar)
        {
            return typeVar?.VarPtr.Dword;
        }
    }
}