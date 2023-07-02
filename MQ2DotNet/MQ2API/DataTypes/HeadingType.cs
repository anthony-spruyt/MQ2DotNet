using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a direction on a compass.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-heading/
    /// </summary>
    [PublicAPI]
    [MQ2Type("heading")]
    public class HeadingType : MQ2DataType
    {
        internal HeadingType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The nearest clock direction, e.g. 1-12
        /// </summary>
        public uint? Clock => GetMember<IntType>("Clock");

        /// <summary>
        /// Heading in degrees (same as casting to float). N = 0, E = 90, S = 180, W = 270
        /// </summary>
        public float? Degrees => GetMember<FloatType>("Degrees");

        /// <summary>
        /// Heading in degrees counter-clockwise (the way the rest of MQ2 and EQ uses it) from north. N = 0, W = 90, S = 180, E = 270
        /// </summary>
        public float? DegreesCCW => GetMember<FloatType>("DegreesCCW");

        /// <summary>
        /// The short compass direction, eg. "S", "SSE"
        /// </summary>
        public string ShortName => GetMember<StringType>("ShortName");

        /// <summary>
        /// The long compass direction, eg. "south", "south by southeast"
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Implicit conversion to a nullable float
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator float?(HeadingType typeVar)
        {
            // GroundType::HeadingTo
            if (typeVar?.VarPtr.Double > 0)
            {
                return (float)typeVar?.VarPtr.Double;
            }

            // Everything else...
            return typeVar?.VarPtr.Float;
        }

        /// <summary>
        /// Same as <see cref="ShortName"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}