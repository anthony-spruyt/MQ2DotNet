using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public class UnloadCommandsHandler : IRequestHandler<UnloadCommandsRequest>
    {
        private readonly IMq2CommandProvider commandProvider;

        public UnloadCommandsHandler(IMq2CommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public Task Handle(UnloadCommandsRequest request, CancellationToken cancellationToken)
        {
            commandProvider.Unload();

            return Task.CompletedTask;
        }
    }
}
