using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux.Services
{
    public interface ICateringService
    {

    }

    public static class CateringServiceExtensions
    {
        public static IServiceCollection AddCateringService(this IServiceCollection services)
        {
            return services
                .AddSingleton<ICateringService, CateringService>();
        }
    }

    public class CateringService : ICateringService
    {

    }
}
