using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a single precision float.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("float")]
    public class FloatType : MQ2DataType
    {
        internal FloatType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string Deci => GetMember<StringType>("Deci");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string Centi => GetMember<StringType>("Centi");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string Milli => GetMember<StringType>("Milli");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? Int => GetMember<IntType>("Int");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string Precision => GetMember<StringType>("Precision");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? Raw => GetMember<IntType>("Raw");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string Prettify => GetMember<StringType>("Prettify");

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