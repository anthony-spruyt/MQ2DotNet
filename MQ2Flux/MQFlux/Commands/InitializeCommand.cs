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
        ICharacterConfigRequest,
        IContextRequest
    {
        public IContext Context { get; set; }
        public FluxConfig Config { get; set; }
        public CharacterConfig Character { get; set; }
    }

    public class InitializeCommandHandler : PCCommandHandler<InitializeCommand>
    {
        private readonly IMediator mediator;

        public InitializeCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<CommandResponse<bool>> Handle(InitializeCommand request, CancellationToken cancellationToken)
        {
            request.Context.MQ.DoCommand("/assist off");

            await mediator.Send(new IdleSinceCommand(), cancellationToken);

            return CommandResponse.FromResult(true);
        }
    }
}
