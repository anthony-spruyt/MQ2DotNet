using MediatR;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;
using System.Threading.Tasks;
using System.Threading;

namespace MQFlux.Commands
{
    public class SaveConfigCommand : Command<Unit>, IConfigRequest
    {
        public FluxConfig Config { get; set; }
        public bool Notify { get; set; } = false;
    }

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
