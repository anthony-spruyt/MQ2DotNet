using MediatR;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class InitializeCommand :
        PCCommand,
        ICharacterConfigRequest
    {
        public FluxConfig Config { get; set; }
        public CharacterConfig Character { get; set; }
    }

    public class InitializeCommandHandler : PCCommandHandler<InitializeCommand>
    {
        private readonly IMediator mediator;
        private readonly IContext context;

        public InitializeCommandHandler(IMediator mediator, IContext context)
        {
            this.mediator = mediator;
            this.context = context;
        }

        public override async Task<CommandResponse<bool>> Handle(InitializeCommand request, CancellationToken cancellationToken)
        {
            context.MQ.DoCommand("/assist off");

            await mediator.Send(new IdleSinceCommand(), cancellationToken);

            return CommandResponse.FromResult(true);
        }
    }
}
