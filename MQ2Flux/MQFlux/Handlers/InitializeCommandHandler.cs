using MediatR;
using MQFlux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class InitializeCommandHandler : IRequestHandler<InitializeCommand>
    {
        public Task Handle(InitializeCommand request, CancellationToken cancellationToken)
        {
            request.Context.MQ.DoCommand("/assist off");

            return Task.CompletedTask;
        }
    }
}
