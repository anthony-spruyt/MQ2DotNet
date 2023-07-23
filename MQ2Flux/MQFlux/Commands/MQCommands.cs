using MediatR;
using MQFlux.Core;
using System.Collections.Generic;
using System.Threading;

namespace MQFlux.Commands
{
    public class LoadMQCommands : Command<IEnumerable<CancellationToken>>
    {
    }

    public class UnloadMQCommands : Command<Unit>
    {
    }
}
