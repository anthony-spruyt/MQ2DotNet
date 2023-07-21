namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a single precision (32-bit) floatiang point number.
    /// A floating-point number is one which has a decimal component(e.g. 1.01).
    /// Members of this DataType generally manipulate the number's precision (i.e. how many decimal places).
    /// They all round correctly with the exception of int.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-float/
    /// </summary>
    [MQ2Type("float")]
    public class FloatType : MQ2DataType
    {
        internal FloatType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

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
        //public uint? Raw => GetMember<IntType>("Raw");
        //
        ///// <summary>
        ///// TODO: new member
        ///// </summary>
        //public string Prettify => GetMember<StringType>("Prettify");

        /// <summary>
        /// Implicit conversion to nullable float
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator float?(FloatType typeVar)
        {
            return typeVar?.VarPtr.Float;
        }
    }
}