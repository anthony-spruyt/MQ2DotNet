using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a pet buff.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("petbuff")]
    public class PetBuffType : MQ2DataType
    {
        internal PetBuffType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            
        }

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string Caster => GetMember<StringType>("Caster");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public TimeSpan? Duration => GetMember<TimeStampType>("Duration");

        /// <summary>
        /// TODO: new member. Need to test this since this type does not follow the standard pattern. When no member is specified it returns the spell.
        /// </summary>
        public SpellType Spell => GetMember<SpellType>("");
    }
}