using MediatR;
using System.Threading;

namespace MQ2Flux.Commands
{
    public class LoadMQ2Commands : IRequest<CancellationToken[]>
    {
        public CancellationToken CancellationToken { get; set; }
    }

    public class UnloadMQ2Commands : IRequest
    {
    }
}
