using JetBrains.Annotations;
using MQ2DotNet.EQ;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a character race.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("race")]
    public class RaceType : MQ2DataType
    {
        internal RaceType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// ID of the race, this should correspond to the <see cref="Race"/> enum
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");
        
        /// <summary>
        /// Full name of the race e.g. Froglok
        /// TODO: map to an enum.
        /// </summary>
        public string Name => GetMember<StringType>("Name");
    }
}