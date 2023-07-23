using MediatR;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers.Handlers
{
    public class SaveConfigCommandHandler : CommandHandler<SaveConfigCommand, Unit>
    {
        private readonly IConfig config;

        public SaveConfigCommandHandler(IConfig config)
        {
            this.config = config;
        }

        public override async Task<CommandResponse<Unit>> Handle(SaveConfigCommand request, CancellationToken cancellationToken)
        {
            await config.Save(request.Notify);

            return CommandResponse.FromResult(Unit.Value);
        }
    }
}
