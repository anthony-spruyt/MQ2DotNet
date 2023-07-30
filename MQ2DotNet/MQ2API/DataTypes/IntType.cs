using MQ2DotNet.EQ;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a 32-bit integer. Can hold values from -2,147,483,648 to 2,147,483,647. <- hah if only this was true. It is used as int, uint, long and others, big mess this class internally along with the time related data types.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-int/
    /// </summary>
    [MQ2Type("int")]
    public class IntType : MQ2DataType
    {
        internal IntType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        // MQ2 type has a bunch of members, but it hardly seems worth implementing them here
        /*
         
        ScopedTypeMember(IntMembers, Float);
	    ScopedTypeMember(IntMembers, Double);
	    ScopedTypeMember(IntMembers, Hex);
	    ScopedTypeMember(IntMembers, Reverse);
	    ScopedTypeMember(IntMembers, LowPart);
	    ScopedTypeMember(IntMembers, HighPart);
	    ScopedTypeMember(IntMembers, Prettify);
         */

        /// <summary>
        /// Implicit conversion to a nullable int
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator int?(IntType typeVar)
        {
            return typeVar?.VarPtr.Int;
        }

        /// <summary>
        /// Implicit conversion to a nullable uint
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator uint?(IntType typeVar)
        {
            return typeVar?.VarPtr.Dword;
        }

        /// <summary>
        /// Implicit conversion to a nullable long
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator long?(IntType typeVar)
        {
            //GroupType::AvgHPs
            return typeVar?.VarPtr.Int64;
        }

        /// <summary>
        /// Implicit conversion to a nullable IntPtr
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator IntPtr?(IntType typeVar)
        {
            return typeVar?.VarPtr.Ptr;
        }

        /// <summary>
        /// Implicit conversion to a bool
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator bool(IntType typeVar)
        {
            uint? nVal = typeVar?.VarPtr.Dword;

            return nVal.HasValue && nVal.Value > 0;
        }

        /// <summary>
        /// Implicit conversion to a nullable Class enum
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator Class?(IntType typeVar)
        {
            return (Class?)typeVar?.VarPtr.Int;
        }

        /// <summary>
        /// Implicit conversion to a WindowType. This happens when a window (parent, child etc) member internally is a 0, IE null.
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator WindowType(IntType _)
        {
            return null;
        }

        /// <summary>
        /// Implicit conversion to an nullable ItemSize enum
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator ItemSize?(IntType typeVar)
        {
            return (ItemSize?)typeVar?.VarPtr.Dword;
        }

        /// <summary>
        /// Implicit conversion to a nullable SpawnType enum
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator SpawnCategory?(IntType typeVar)
        {
            return (SpawnCategory?)typeVar?.VarPtr.Dword;
        }

        /// <summary>
        /// Implicit conversion to a nullable ZoneType enum
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator EQ.ZoneType?(IntType typeVar)
        {
            return (EQ.ZoneType?)typeVar?.VarPtr.Dword;
        }

        /// <summary>
        /// Implicit conversion to a nullable RaidLootType enum
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator RaidLootType?(IntType typeVar)
        {
            return (RaidLootType?)typeVar?.VarPtr.Dword;
        }

        /// <summary>
        /// Implicit conversion to a nullable TimeSpan
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator TimeSpan?(IntType typeVar)
        {
            // SkillType::ReuseTime
            // ItemSpellType::CastTime
            return typeVar == null ? TimeSpan.Zero : TimeSpan.FromMilliseconds(typeVar.VarPtr.Dword);
        }

        /// <summary>
        /// Implicit conversion to an Expansion bitmask.
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator Expansion(IntType typeVar)
        {
            return (Expansion)((int)(typeVar?.VarPtr.Dword).GetValueOrDefault(0u));
        }

        /// <summary>
        /// Implicit conversion to SpellCategory.
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator SpellCategory(IntType typeVar)
        {
            return ((SpellCategory?)(typeVar?.VarPtr.Dword)).GetValueOrDefault(SpellCategory.UNKNOWN);
        }
    }
}