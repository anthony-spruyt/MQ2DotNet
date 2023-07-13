using MQ2DotNet.MQ2API.DataTypes;
using System;

namespace MQFlux.Extensions
{
    public static class SpellTypeExtensions
    {
        public static bool HasCastTime(this SpellType @this)
        {
            return @this.CastTime.HasValue ?
                @this.CastTime.Value > TimeSpan.Zero :
                false;
        }
    }
}
