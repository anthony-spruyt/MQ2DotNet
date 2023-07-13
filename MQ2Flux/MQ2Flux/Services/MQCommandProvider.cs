using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MQ2Flux.Services
{
    public interface IMQCommandProvider
    {
        IEnumerable<IMQAsyncCommand> AsyncCommands { get; }
        IEnumerable<IMQCommand> Commands { get; }
        void Load();
        void Unload();
    }

    public static class MQCommandProviderExtensions
    {
        public static IServiceCollection AddMQCommandProvider(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMQCommandProvider, MQCommandProvider>()
                .AddSingleton<IFluxMQAsyncCommand, FluxMQAsyncCommand>();
        }
    }

    public class MQCommandProvider : IMQCommandProvider
    {
        public IEnumerable<IMQAsyncCommand> AsyncCommands { get; private set; }
        public IEnumerable<IMQCommand> Commands { get; private set; }

        private readonly IMQContext context;

        public MQCommandProvider(IMQContext context, IFluxMQAsyncCommand fluxAsyncCommandService)
        {
            this.context = context;
            AsyncCommands = new IMQAsyncCommand[]
            {
                fluxAsyncCommandService
            };
            Commands = new IMQCommand[]
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
