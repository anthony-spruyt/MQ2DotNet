using System.Collections.Generic;

namespace MQ2Flux
{
    public class Mq2CommandProvider : IMq2CommandProvider
    {
        public IEnumerable<IMq2AsyncCommand> AsyncCommands { get; private set; }
        public IEnumerable<IMq2Command> Commands { get; private set; }

        private readonly IMq2Context context;

        public Mq2CommandProvider(IMq2Context context, IFluxAsyncCommand fluxAsyncCommandService)
        {
            this.context = context;
            AsyncCommands = new IMq2AsyncCommand[]
            {
                fluxAsyncCommandService
            };
            Commands = new IMq2Command[] { };
        }

        public void Load()
        {
            foreach (var command in AsyncCommands)
            {
                context.Commands.AddAsyncCommand(command.Command, command.HandleAsync);
            }

            foreach (var command in Commands)
            {
                context.Commands.AddCommand(command.Command, command.Handle);
            }
        }

        public void Unload()
        {
            foreach (var command in AsyncCommands)
            {
                context.Commands.RemoveCommand(command.Command);
            }

            foreach (var command in Commands)
            {
                context.Commands.RemoveCommand(command.Command);
            }
        }
    }
}
