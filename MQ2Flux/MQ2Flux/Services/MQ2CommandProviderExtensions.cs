using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux.Services
{
    public static class MQ2CommandProviderExtensions
    {
        public static IServiceCollection AddMQ2CommandProvider(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMQ2CommandProvider, MQ2CommandProvider>()
                .AddSingleton<IFluxMQ2AsyncCommand, FluxMQ2AsyncCommand>();
        }
    }
}
