using MediatR;
using System.Threading;

namespace MQFlux.Commands
{
    public class LoadMQCommands : IRequest<CancellationToken[]>
    {
        public CancellationToken CancellationToken { get; set; }
    }

    public class UnloadMQCommands : IRequest
    {
    }
}
