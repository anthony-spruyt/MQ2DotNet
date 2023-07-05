using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux.Services
{
    public static class MQ2LoggerExtensions
    {
        public static IServiceCollection AddMQ2Logging(this IServiceCollection services)
        {
            return services.AddSingleton<IMQ2Logger, MQ2Logger>();
        }
    }
}
