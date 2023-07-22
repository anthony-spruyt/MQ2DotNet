using MediatR;
using System.Collections.Generic;
using System.Threading;

namespace MQFlux.Commands
{
    public class LoadMQCommands : IRequest<IEnumerable<CancellationToken>>
    {
    }

    public class UnloadMQCommands : IRequest
    {
    }
}
