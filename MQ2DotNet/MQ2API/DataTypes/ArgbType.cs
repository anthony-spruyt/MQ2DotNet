using System;
using System.Drawing;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a color
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-argb/
    /// </summary>
    [MQ2Type("argb")]
    public class ArgbType : MQ2DataType
    {
        internal ArgbType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Implicit conversion to a .NET colour type.
        /// TODO: Test this cast.
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

        /// <summary>
        /// The hex value of the integer formed by the ARGB.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}