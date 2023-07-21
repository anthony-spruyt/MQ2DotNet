using MQ2DotNet.EQ;
using MQ2DotNet.Utility;
using System;
using System.Runtime.InteropServices;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// A string is an array of characters. In MQ2 there is no single character datatype, so any variable or expression that contains text is considered a string.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-string/
    /// </summary>
    /// <remarks>Since most MQ2 strings share the same storage (DataTypeTemp), lazy evaluation is a bad idea.</remarks>
    [MQ2Type("string")]
    public class StringType : MQ2DataType
    {
        private static readonly object _lock = new object();
        private readonly string _string;

        internal StringType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            lock (_lock)
            {
                // Since most MQ2 strings share the same storage (DataTypeTemp), lazy evaluation is a bad idea.
                _string = typeVar.VarPtr.Ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(typeVar.VarPtr.Ptr) : null;
            }

            // Standardize null, empty, whitespace handling.
            if (string.Compare(_string, "NULL", true) == 0 || string.IsNullOrWhiteSpace(_string))
            {
                _string = string.Empty;
            }
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
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator LDoNTheme?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<LDoNTheme>();
        }

        /// <summary>
        /// Implicit conversion to a DMGBonusType
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator DMGBonusType?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<DMGBonusType>();
        }

        /// <summary>
        /// Implicit conversion to a EffectType
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator EffectType?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<EffectType>();
        }

        /// <summary>
        /// Implicit conversion to a CombatState
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator CombatState?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<CombatState>();
        }

        /// <summary>
        /// Implicit conversion to a DZStatus
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator DZStatus?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<DZStatus>();
        }

        /// <summary>
        /// Implicit conversion to a GameState
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator GameState?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<GameState>();
        }

        /// <summary>
        /// Implicit conversion to a PetStance
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator PetStance?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<PetStance>();
        }

        /// <summary>
        /// Implicit conversion to a ConColor
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator ConColor?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<ConColor>();
        }

        /// <summary>
        /// Implicit conversion to a SpawnState
        /// </summary>
        /// <param name="typeVar"></param>
        /// <returns></returns>
        public static implicit operator SpawnState?(StringType typeVar)
        {
            return typeVar?._string?.ToEnum<SpawnState>();
        }
    }
}