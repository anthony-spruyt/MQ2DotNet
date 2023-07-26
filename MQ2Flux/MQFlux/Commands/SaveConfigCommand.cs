using MediatR;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

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

        public override Task<CommandResponse<Unit>> Handle(SaveConfigCommand request, CancellationToken cancellationToken)
        {
            config.Save(request.Notify);

            return CommandResponse.FromResultTask(Unit.Value);
        }
    }
}
