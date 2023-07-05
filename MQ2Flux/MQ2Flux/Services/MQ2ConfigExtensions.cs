using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux.Services
{
    public static class MQ2ConfigExtensions
    {
        public static IServiceCollection AddMQ2Config(this IServiceCollection services)
        {
            return services.AddSingleton<IMQ2Config, MQ2Config>();
        }
    }
}
