﻿namespace MQ2DotNet.MQ2API.DataTypes
{
    public class Int64Type : MQ2DataType
    {
        internal Int64Type(MQ2TypeVar typeVar) : base(typeVar)
        {
        }

        // MQ2 type has a bunch of members, but it hardly seems worth implementing them here
        public static implicit operator long(Int64Type typeVar)
        {
            return typeVar.VarPtr.Int64;
        }
    }
}