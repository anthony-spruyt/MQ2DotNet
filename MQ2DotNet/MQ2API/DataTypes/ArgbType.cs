using JetBrains.Annotations;
using System;
using System.Drawing;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a colour.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("argb")]
    public class ArgbType : MQ2DataType
    {
        internal ArgbType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Implicit conversion to a .NET colour type
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator Color?(ArgbType typeVar)
        {
            try
            {
                if (typeVar != null && typeVar.VarPtr != null)
                {
                    return Color.FromArgb((int)typeVar.VarPtr.Dword);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Failed to cast from type {nameof(ArgbType)} to {nameof(Color)}", ex);
            }
        }
    }
}