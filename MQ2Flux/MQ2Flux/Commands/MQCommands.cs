using MediatR;
using System.Threading;

namespace MQ2Flux.Commands
{
    public class LoadMQCommands : IRequest<CancellationToken[]>
    {
        public CancellationToken CancellationToken { get; set; }
    }

    public class UnloadMQCommands : IRequest
    {
    }
}
