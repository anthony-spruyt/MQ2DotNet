using MediatR;
using MQFlux.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class LoadMQCommandsHandler : IRequestHandler<LoadMQCommands, IEnumerable<CancellationToken>>
    {
        private readonly IMQCommandProvider commandProvider;

        public LoadMQCommandsHandler(IMQCommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public Task<IEnumerable<CancellationToken>> Handle(LoadMQCommands request, CancellationToken cancellationToken)
        {
            commandProvider.Load();

            return Task.FromResult(GetLinkedTokens());
        }

        private IEnumerable<CancellationToken> GetLinkedTokens()
        {
            var cancellationTokens = new List<CancellationToken>();

            foreach (var command in commandProvider.Commands)
            {
                cancellationTokens.Add(command.CancellationToken);
            }

            foreach (var asyncCommand in commandProvider.AsyncCommands)
            {
                cancellationTokens.Add(asyncCommand.CancellationToken);
            }

            return cancellationTokens;
        }
    }
}
