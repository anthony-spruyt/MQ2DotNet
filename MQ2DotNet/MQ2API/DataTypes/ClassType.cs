using JetBrains.Annotations;
using MQ2DotNet.EQ;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data about a particular character class
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-class/
    /// </summary>
    [PublicAPI]
    [MQ2Type("class")]
    public class ClassType : MQ2DataType
    {
        public const int NUM_OF_CLASSES = 16;

        internal ClassType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Name = GetMember<StringType>("Name");
            ShortName = GetMember<StringType>("ShortName");
        }

        /// <summary>
        /// The class numeric ID
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// The full name of the class. Ex: "Ranger"
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The short name (three letter code) of the class. Ex: RNG for Ranger
        /// </summary>
        public string ShortName { get; }

        /// <summary>
        /// Can cast spells, including Bard
        /// </summary>
        public bool CanCast => GetMember<BoolType>("CanCast");

        /// <summary>
        /// True if class is a pure caster (Cleric, Druid, Shaman, Necromancer, Wizard, Mage or Enchanter)
        /// </summary>
        public bool PureCaster => GetMember<BoolType>("PureCaster");

        /// <summary>
        /// True if class is a pet class (Shaman, Necromancer, Mage or Beastlord)
        /// </summary>
        public bool PetClass => GetMember<BoolType>("PetClass");

        /// <summary>
        /// True if class is a Druid or Ranger
        /// </summary>
        public bool DruidType => GetMember<BoolType>("DruidType");

        /// <summary>
        /// True if class is Shaman or Beastlord
        /// </summary>
        public bool ShamanType => GetMember<BoolType>("ShamanType");

        /// <summary>
        /// True if class is a Necromancer or Shadow Knight
        /// </summary>
        public bool NecromancerType => GetMember<BoolType>("NecromancerType");

        /// <summary>
        /// True if class is a Cleric or Paladin
        /// </summary>
        public bool ClericType => GetMember<BoolType>("ClericType");

        /// <summary>
        /// True if class is a Healer (Cleric, Druid or Shaman)
        /// </summary>
        public bool HealerType => GetMember<BoolType>("HealerType");

        /// <summary>
        /// True if class is Mercenary
        /// </summary>
        public bool MercType => GetMember<BoolType>("MercType");

        /// <summary>
        /// Implicit conversion to a Class enumeration
        /// TODO: validate this still works.
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator Class?(ClassType typeVar) => (Class?) (1 << (typeVar?.VarPtr.Int - 1));

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