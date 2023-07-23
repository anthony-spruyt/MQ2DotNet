using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class InitializeCommandHandler : PCCommandHandler<InitializeCommand>
    {
        public override Task<CommandResponse<bool>> Handle(InitializeCommand request, CancellationToken cancellationToken)
        {
            request.Context.MQ.DoCommand("/assist off");

            return CommandResponse.FromResultTask(true);
        }
    }
}
