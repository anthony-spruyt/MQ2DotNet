using MediatR;
using MQ2DotNet.EQ;

namespace MQFlux.Queries
{
    public class GetGameStateQuery : IRequest<GameState>
    {
    }
}
