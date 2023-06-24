using MediatR;
using System.Threading;

namespace MQ2Flux
{
    public class LoadCommandsRequest : IRequest<CancellationToken[]>
    {
        public CancellationToken CancellationToken { get; set; }
    }
}
