using JetBrains.Annotations;
using MQ2DotNet.EQ;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains information on the specified race
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-race/
    /// </summary>
    [PublicAPI]
    [MQ2Type("race")]
    public class RaceType : MQ2DataType
    {
        public const int NUM_OF_RACES = 15;

        internal RaceType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The ID of the race
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// The name of the race
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// TODO: Not implemented.
        /// </summary>
        /// <param name="race"></param>
        public static implicit operator Race?(RaceType race) => throw new NotImplementedException();

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