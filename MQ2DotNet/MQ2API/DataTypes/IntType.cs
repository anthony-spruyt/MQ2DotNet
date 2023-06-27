using System;
using JetBrains.Annotations;
using MQ2DotNet.EQ;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a 32 bit integer. <- hah if only this was true.It is used as int, uint, long and others, big mess this class internally along with the time related data types.
    /// Last Verified: 2023-06-26
    /// </summary>
    [PublicAPI]
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
        /// Implicit conversion to a nullable uint
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator bool(IntType typeVar)
        {
            uint? nVal = typeVar?.VarPtr.Dword;

            return nVal.HasValue && nVal.Value > 0;
        }

        /// <summary>
        /// Implicit conversion to a Class enum
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
        public static implicit operator WindowType(IntType typeVar)
        {
            return null;
        }

        /// <summary>
        /// Implicit conversion to an ItemSize enum
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator ItemSize?(IntType typeVar)
        {
            return (ItemSize?)typeVar?.VarPtr.Dword;
        }

        /// <summary>
        /// Implicit conversion to a SpawnType enum
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator EQ.SpawnType?(IntType typeVar)
        {
            return (EQ.SpawnType?)typeVar?.VarPtr.Dword;
        }

        /// <summary>
        /// Implicit conversion to a ZoneType enum
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator EQ.ZoneType?(IntType typeVar)
        {
            return (EQ.ZoneType?)typeVar?.VarPtr.Dword;
        }

        /// <summary>
        /// Implicit conversion to a RaidLootType enum
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator RaidLootType?(IntType typeVar)
        {
            return (EQ.RaidLootType?)typeVar?.VarPtr.Dword;
        }
    }
}