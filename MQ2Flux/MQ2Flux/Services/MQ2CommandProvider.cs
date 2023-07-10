using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MQ2Flux.Services
{
    public interface IMQ2CommandProvider
    {
        IEnumerable<IMQ2AsyncCommand> AsyncCommands { get; }
        IEnumerable<IMQ2Command> Commands { get; }
        void Load();
        void Unload();
    }

    public static class MQ2CommandProviderExtensions
    {
        public static IServiceCollection AddMQ2CommandProvider(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMQ2CommandProvider, MQ2CommandProvider>()
                .AddSingleton<IFluxMQ2AsyncCommand, FluxMQ2AsyncCommand>();
        }
    }

    public class MQ2CommandProvider : IMQ2CommandProvider
    {
        public IEnumerable<IMQ2AsyncCommand> AsyncCommands { get; private set; }
        public IEnumerable<IMQ2Command> Commands { get; private set; }

        private readonly IMQ2Context context;

        public MQ2CommandProvider(IMQ2Context context, IFluxMQ2AsyncCommand fluxAsyncCommandService)
        {
            this.context = context;
            AsyncCommands = new IMQ2AsyncCommand[]
            {
                fluxAsyncCommandService
            };
            Commands = new IMQ2Command[]
            {
                
            };
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
