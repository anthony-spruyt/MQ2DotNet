using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class InitializeCommandHandler : IRequestHandler<InitializeCommand, Unit>
    {
        public Task<Unit> Handle(InitializeCommand request, CancellationToken cancellationToken)
        {
            request.Context.MQ.DoCommand("/assist off");

            return Task.FromResult(Unit.Value);
        }
    }
}
