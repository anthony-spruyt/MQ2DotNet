using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace MQFlux.Services
{
    public static class CacheKeys
    {
        public static readonly string Camping = nameof(Camping);
        public static readonly string CharacterConfig = nameof(CharacterConfig);
        public static readonly string GameState = nameof(GameState);
        public static readonly string IdleSince = nameof(IdleSince);
        public static readonly string IsPaused = nameof(IsPaused);
        public static readonly string Zoning = nameof(Zoning);
    }

    public interface ICache
    {
        T AddOrUpdate<T>(string key, T value);
        bool TryAdd<T>(string key, T value);
        bool TryGetValue<T>(string key, out T value);
        bool TryRemove<T>(string key, out T value);
    }

    public static class MemoryCacheServiceExtensions
    {
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            return services
                .AddSingleton<ICache, MemoryCacheService>();
        }
    }

    public class MemoryCacheService : ICache
    {
        private readonly ConcurrentDictionary<string, object> cache;

        public MemoryCacheService()
        {
            cache = new ConcurrentDictionary<string, object>();
        }

        public T AddOrUpdate<T>(string key, T value)
        {
            return (T)cache.AddOrUpdate(key, value, (k, v) => value);
        }

        public bool TryAdd<T>(string key, T value)
        {
            return cache.TryAdd(key, value);
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            var result = cache.TryGetValue(key, out var cachedValue);

            value = result && cachedValue != null ? (T)cachedValue : default;

            return result;
        }

        public bool TryRemove<T>(string key, out T value)
        {
            var result = cache.TryRemove(key, out var cachedValue);

            value = result && cachedValue != null ? (T)cachedValue : default;

            return result;
        }
    }
}
