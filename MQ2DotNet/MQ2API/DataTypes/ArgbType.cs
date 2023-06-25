using System;
using System.Drawing;
using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a colour
    /// Last Verified: 2023-06-23
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
            Color color = default;

            try
            {
                if (typeVar != null && typeVar.VarPtr != null)
                {
                    color = Color.FromArgb((int)typeVar.VarPtr.Dword);
                }
            }
            catch (Exception ex)
            {

                throw new InvalidCastException($"Failed to cast from type {nameof(ArgbType)} to {nameof(Color)}", ex);
            }

            return color;
        }
    }
}