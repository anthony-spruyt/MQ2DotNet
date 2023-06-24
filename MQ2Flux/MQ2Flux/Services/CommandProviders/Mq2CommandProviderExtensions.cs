using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux
{
    public static class Mq2CommandProviderExtensions
    {
        public static IServiceCollection AddMq2CommandProvider(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMq2CommandProvider, Mq2CommandProvider>()
                .AddSingleton<IFluxAsyncCommand, FluxAsyncCommand>();
        }
    }
}
