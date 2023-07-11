using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a pet buff.
    /// Last Verified: 2023-07-02.
    /// There is no doco for this type. Only have pet doco here -> https://docs.macroquest.org/reference/data-types/datatype-pet/
    /// </summary>
    [PublicAPI]
    [MQ2Type("petbuff")]
    public class PetBuffType : MQ2DataType
    {
        internal PetBuffType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// TODO: new member, no online doco for it.
        /// </summary>
        public string Caster => GetMember<StringType>("Caster");

        /// <summary>
        /// TODO: new member, no online doco for it.
        /// </summary>
        public TimeSpan? Duration => GetMember<TimeStampType>("Duration");

        /// <summary>
        /// TODO: new member, no online doco for it. Need to test this since this type does not follow the standard pattern. When no member is specified it returns the spell.
        /// </summary>
        public SpellType Spell => GetMember<SpellType>("");
    }
}