using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class UnloadMQ2CommandsHandler : IRequestHandler<UnloadMQ2Commands>
    {
        private readonly IMQ2CommandProvider commandProvider;

        public UnloadMQ2CommandsHandler(IMQ2CommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public Task Handle(UnloadMQ2Commands request, CancellationToken cancellationToken)
        {
            commandProvider.Unload();

            return Task.CompletedTask;
        }
    }
}
