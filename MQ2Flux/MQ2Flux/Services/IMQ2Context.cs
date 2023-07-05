using MQ2DotNet.MQ2API;
using MQ2DotNet.Services;

namespace MQ2Flux.Services
{
    public interface IMQ2Context
    {
        MQ2 MQ2 { get; }
        Chat Chat { get; }
        MQ2DotNet.Services.Commands Commands { get; }
        Events Events { get; }
        Spawns Spawns { get; }
        TLO TLO { get; }
    }
}
