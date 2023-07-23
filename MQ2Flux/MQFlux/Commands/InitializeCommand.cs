using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class InitializeCommand : PCCommand, ICharacterConfigRequest
    {
        public IContext Context { get; set; }
        public FluxConfig Config { get; set; }
        public CharacterConfig Character { get; set; }
    }

    public class InitializeCommandHandler : PCCommandHandler<InitializeCommand>
    {
        public override Task<CommandResponse<bool>> Handle(InitializeCommand request, CancellationToken cancellationToken)
        {
            request.Context.MQ.DoCommand("/assist off");

            return CommandResponse.FromResultTask(true);
        }
    }
}
