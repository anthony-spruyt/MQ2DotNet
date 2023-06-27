using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a 64 bit integer.
    /// Last Verified: 2023-06-26
    /// </summary>
    [PublicAPI]
    [MQ2Type("int64")]
    public class Int64Type : MQ2DataType
    {
        internal Int64Type(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        // MQ2 type has a bunch of members, but it hardly seems worth implementing them here

        /*
        ScopedTypeMember(Int64Members, Float);
        ScopedTypeMember(Int64Members, Double);
        ScopedTypeMember(Int64Members, Hex);
        ScopedTypeMember(Int64Members, Reverse);
        ScopedTypeMember(Int64Members, LowPart);
        ScopedTypeMember(Int64Members, HighPart);
        ScopedTypeMember(Int64Members, Prettify);
         */

        /// <summary>
        /// Implicit conversion to a nullable long
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator long?(Int64Type typeVar)
        {
            return typeVar?.VarPtr.Int64;
        }

        /// <summary>
        /// Implicit conversion to a nullable TimeSpan
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator TimeSpan?(Int64Type typeVar)
        {
            // MacroType::RunTime
            return typeVar?.VarPtr.Int64 == null ? (TimeSpan?)null : TimeSpan.FromMilliseconds(typeVar.VarPtr.Int64);
        }
    }
}