using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MQFlux.Services
{
    public interface IMQCommandProvider
    {
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
                .AddSingleton<IFluxCommand, FluxCommandService>();
        }
    }

    public class MQCommandProvider : IMQCommandProvider
    {
        public IEnumerable<IMQCommand> Commands { get; private set; }

        private readonly IContext context;

        public MQCommandProvider(IContext context, IFluxCommand fluxCommandService)
        {
            this.context = context;

            Commands = new IMQCommand[]
            {
                fluxCommandService
            };
        }

        public void Load()
        {
            foreach (var command in Commands)
            {
                context.Commands.AddCommand(command.Command, command.Handle);
            }
        }

        public void Unload()
        {
            foreach (var command in Commands)
            {
                context.Commands.RemoveCommand(command.Command);
            }
        }
    }
}
