using JetBrains.Annotations;
using MQ2DotNet.EQ;
using MQ2DotNet.Utility;
using System;
using System.Runtime.InteropServices;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a string.
    /// Last Verified: 2023-06-28
    /// </summary>
    [PublicAPI]
    [MQ2Type("string")]
    public class StringType : MQ2DataType
    {
        private readonly string _string;

        internal StringType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            // Since most MQ2 strings share the same storage (DataTypeTemp), lazy evaluation is a bad idea
            _string = typeVar.VarPtr.Ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(typeVar.VarPtr.Ptr) : null;
        }

        /// <summary>
        /// Implicit conversion to a string
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator string(StringType typeVar)
        {
            return typeVar?._string;
        }

        /// <summary>
        /// Implicit conversion to a LDoNTheme
        /// TODO: havent tested this type of parsing yet.
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator LDoNTheme?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<LDoNTheme>();
        }

        /// <summary>
        /// Implicit conversion to a DMGBonusType
        /// TODO: havent tested this type of parsing yet.
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator DMGBonusType?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<DMGBonusType>();
        }

        /// <summary>
        /// Implicit conversion to a EffectType
        /// TODO: havent tested this type of parsing yet.
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator EffectType?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<EffectType>();
        }

        /// <summary>
        /// Implicit conversion to a CombatState
        /// TODO: havent tested this type of parsing yet.
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator CombatState?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<CombatState>();
        }

        /// <summary>
        /// Implicit conversion to a DZStatus
        /// TODO: havent tested this type of parsing yet.
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator DZStatus?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<DZStatus>();
        }

        /// <summary>
        /// Implicit conversion to a GameState
        /// TODO: havent tested this type of parsing yet.
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator GameState?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<GameState>();
        }

        /// <summary>
        /// Implicit conversion to a PetStance
        /// TODO: havent tested this type of parsing yet.
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator PetStance?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<PetStance>();
        }
    }
}