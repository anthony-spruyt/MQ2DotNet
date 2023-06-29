using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for the character in the select list.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("charselectlist")]
    public class CharSelectListType : MQ2DataType
    {
        internal CharSelectListType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Total number of characters in the character select list
        /// </summary>
        public uint? Count => GetMember<IntType>("Count");

        /// <summary>
        /// Character's name
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Character's level
        /// </summary>
        public uint? Level => GetMember<IntType>("Level");

        /// <summary>
        /// Character's class.
        /// TODO: map to an enum.
        /// </summary>
        public string Class => GetMember<StringType>("Class");

        /// <summary>
        /// Character's race.
        /// TODO: map to an enum.
        /// </summary>
        public string Race => GetMember<StringType>("Race");

        /// <summary>
        /// ID of the zone the character is in
        /// </summary>
        public uint? ZoneID => GetMember<IntType>("ZoneID");

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}