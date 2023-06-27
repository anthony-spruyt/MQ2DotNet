using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains various mathematical functions. Not implemented for .NET.
    /// Last Verified: 2023-06-27
    /// </summary>
    [Obsolete("Use System.Math")]
    [MQ2Type("math")]
    public class MathType : MQ2DataType
    {
        internal MathType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        // Distance might possibly be useful in the future, but not likely if we can get locs easier from spawn and pc type data.

        public override string ToString()
        {
            return typeof(MathType).FullName;
        }
    }
}