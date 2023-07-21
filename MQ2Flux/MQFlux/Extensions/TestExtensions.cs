using MQ2DotNet.MQ2API;
using MQ2DotNet.MQ2API.DataTypes;

namespace MQFlux.Extensions
{
    public static class TestExtensions
    {
        public static MQ2DataType ID(this TargetType @this)
        {
            return @this.GetMember<MQ2DataType>("ID");
        }
    }
}
