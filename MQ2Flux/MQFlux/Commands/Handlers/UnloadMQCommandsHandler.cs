using MediatR;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class UnloadMQCommandsHandler : IRequestHandler<UnloadMQCommands, Unit>
    {
        private readonly IMQCommandProvider commandProvider;

        public UnloadMQCommandsHandler(IMQCommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public Task<Unit> Handle(UnloadMQCommands request, CancellationToken cancellationToken)
        {
            commandProvider.Unload();

            return Task.FromResult(Unit.Value);
        }
    }
}
