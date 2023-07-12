﻿using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace MQ2Flux.Services
{
    public interface ICache
    {
        bool TryAdd<T>(string key, T value);
        bool TryGetValue<T>(string key, out T value);
        bool TryRemove<T>(string key, out T value);
    }

    public static class MemoryCacheExtensions
    {
        public static IServiceCollection AddMemoryCache(this IServiceCollection services)
        {
            return services
                .AddSingleton<ICache, MemoryCache>();
        }
    }

    public class MemoryCache : ICache
    {
        private readonly ConcurrentDictionary<string, object> cache;

        public MemoryCache()
        {
            cache = new ConcurrentDictionary<string, object>();
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
