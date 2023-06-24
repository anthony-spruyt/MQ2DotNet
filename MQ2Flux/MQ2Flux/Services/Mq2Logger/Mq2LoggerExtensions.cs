using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux
{
    public static class Mq2LoggerExtensions
    {
        public static IServiceCollection AddMq2Logging(this IServiceCollection services)
        {
            return services.AddSingleton<IMq2Logger, Mq2Logger>();
        }
    }
}
