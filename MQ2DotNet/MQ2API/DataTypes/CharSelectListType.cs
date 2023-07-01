using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Provides information about the character list.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-charselectlist/
    /// </summary>
    [PublicAPI]
    [MQ2Type("charselectlist")]
    public class CharSelectListType : MQ2DataType
    {
        internal CharSelectListType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Number of characters in the character select list
        /// </summary>
        public uint? Count => GetMember<IntType>("Count");

        /// <summary>
        /// Name of the character
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Level of the character
        /// </summary>
        public uint? Level => GetMember<IntType>("Level");

        /// <summary>
        /// Class of the character
        /// TODO: map to an enum.
        /// </summary>
        public string Class => GetMember<StringType>("Class");

        /// <summary>
        /// Race of the character
        /// TODO: map to an enum.
        /// </summary>
        public string Race => GetMember<StringType>("Race");

        /// <summary>
        /// Id of the zone the character logged out in
        /// </summary>
        public uint? ZoneID => GetMember<IntType>("ZoneID");

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}