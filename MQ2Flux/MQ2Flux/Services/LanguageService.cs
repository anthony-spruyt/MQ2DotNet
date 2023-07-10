using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux.Services
{
    public interface ILanguageService
    {

    }

    public static class LanguageServiceExtensions
    {
        public static IServiceCollection AddLanguageService(this IServiceCollection services)
        {
            return services
                .AddSingleton<ILanguageService, LanguageService>();
        }
    }

    public class LanguageService : ILanguageService
    {

    }
}
