using JetBrains.Annotations;
using MQ2DotNet.EQ;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains data related to deity members
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-deity/
    /// </summary>
    [PublicAPI]
    [MQ2Type("Deity")]
    public class DeityType : MQ2DataType
    {
        public const int NUM_OF_DEITIES = 16;

        internal DeityType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Name = GetMember<StringType>("Name");
        }

        /// <summary>
        /// The deity's ID #
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// The full deity name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The team name, one of "good", "evil", "neutral", "none"
        /// </summary>
        public string Team => GetMember<StringType>("Team");

        /// <summary>
        /// TODO: Not implemented.
        /// </summary>
        /// <param name="race"></param>
        public static implicit operator Deity?(DeityType deity) => throw new NotImplementedException();

        /// <summary>
        /// Same as <see cref="Name"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}