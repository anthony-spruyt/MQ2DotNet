namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a 64-bit integer. Can hold values from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-int64/
    /// </summary>
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

        // causes stack overflow
        ///// <summary>
        ///// Implicit conversion to a nullable TimeSpan
        ///// </summary>
        ///// <param name="typeVar"></param>
        //public static implicit operator TimeSpan?(Int64Type typeVar)
        //{
        //    // MacroType::RunTime
        //    return typeVar?.VarPtr.Int64 == null ? (TimeSpan?)null : TimeSpan.FromMilliseconds(typeVar.VarPtr.Int64);
        //}
    }
}