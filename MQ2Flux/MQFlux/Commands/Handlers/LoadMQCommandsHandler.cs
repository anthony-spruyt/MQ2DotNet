﻿using MQFlux.Core;
using MQFlux.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class LoadMQCommandsHandler : CommandHandler<LoadMQCommands, IEnumerable<CancellationToken>>
    {
        private readonly IMQCommandProvider commandProvider;

        public LoadMQCommandsHandler(IMQCommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public override Task<CommandResponse<IEnumerable<CancellationToken>>> Handle(LoadMQCommands request, CancellationToken cancellationToken)
        {
            commandProvider.Load();

            return CommandResponse.FromResultTask(GetLinkedTokens());
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
