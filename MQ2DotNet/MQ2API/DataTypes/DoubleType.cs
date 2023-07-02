using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a double precision (64-bit) floating point number.
    /// A floating-point number is one which has a decimal component(e.g. 1.01).
    /// Members of this DataType generally manipulate the number's precision (i.e. how many decimal places).
    /// They all round correctly with the exception of int.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-double/
    /// </summary>
    [PublicAPI]
    [MQ2Type("double")]
    public class DoubleType : MQ2DataType
    {
        internal DoubleType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        // Dont need these TBH, commented out.

        ///// <summary>
        ///// TODO: new member
        ///// </summary>
        //public string Deci => GetMember<StringType>("Deci");
        //
        ///// <summary>
        ///// TODO: new member
        ///// </summary>
        //public string Centi => GetMember<StringType>("Centi");
        //
        ///// <summary>
        ///// TODO: new member
        ///// </summary>
        //public string Milli => GetMember<StringType>("Milli");
        //
        ///// <summary>
        ///// TODO: new member
        ///// </summary>
        //public int? Int => GetMember<IntType>("Int");
        //
        ///// <summary>
        ///// TODO: new member
        ///// </summary>
        //public string Precision => GetMember<StringType>("Precision");
        //
        ///// <summary>
        ///// TODO: new member
        ///// </summary>
        //public string Prettify => GetMember<StringType>("Prettify");

        /// <summary>
        /// Implicit conversion to double
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator double?(DoubleType typeVar)
        {
            return typeVar?.VarPtr.Double;
        }
    }
}