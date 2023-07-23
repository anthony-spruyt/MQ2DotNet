using MediatR;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class UnloadMQCommandsHandler : CommandHandler<UnloadMQCommands, Unit>
    {
        private readonly IMQCommandProvider commandProvider;

        public UnloadMQCommandsHandler(IMQCommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public override Task<CommandResponse<Unit>> Handle(UnloadMQCommands request, CancellationToken cancellationToken)
        {
            commandProvider.Unload();

            return CommandResponse.FromResultTask(Unit.Value);
        }
    }
}
