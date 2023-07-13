using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class LoadMQCommandsHandler : IRequestHandler<LoadMQCommands, CancellationToken[]>
    {
        private readonly IMQCommandProvider commandProvider;

        public LoadMQCommandsHandler(IMQCommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public Task<CancellationToken[]> Handle(LoadMQCommands request, CancellationToken cancellationToken)
        {
            commandProvider.Load();

            return Task.FromResult(GetLinkedTokens(request.CancellationToken));
        }

        private CancellationToken[] GetLinkedTokens(CancellationToken cancellationToken)
        {
            CancellationToken[] cancellationTokens;

            int tokenCount = commandProvider.Commands.Count() + commandProvider.AsyncCommands.Count();

            cancellationTokens = new CancellationToken[tokenCount + 1];

            for (int i = 0; i < commandProvider.Commands.Count(); i++)
            {
                cancellationTokens[i] = commandProvider.Commands.ElementAt(i).CancellationToken;
            }

            for (int i = 0; i < commandProvider.AsyncCommands.Count(); i++)
            {
                cancellationTokens[commandProvider.Commands.Count() + i] = commandProvider.AsyncCommands.ElementAt(i).CancellationToken;
            }

            cancellationTokens[tokenCount] = cancellationToken;

            return cancellationTokens;
        }
    }
}
