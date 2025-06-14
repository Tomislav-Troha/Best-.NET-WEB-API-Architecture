using System.Collections.Concurrent;
using BestArchitecture.Application.Repositories.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace BestArchitecture.Infrastructure.Repositories.Cache
{
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        private readonly IMemoryCache _memoryCache;
        private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(10);
        private const string _allKeysCacheKey = "__ALL_CACHE_KEYS__";

        public MemoryCacheRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _memoryCache.Set(_allKeysCacheKey, new ConcurrentDictionary<string, byte>());
        }

        private ConcurrentDictionary<string, byte> GetKeySet()
        {
            if (_memoryCache.TryGetValue(_allKeysCacheKey, out ConcurrentDictionary<string, byte>? keys) && keys != null)
                return keys;

            keys = new ConcurrentDictionary<string, byte>();
            _memoryCache.Set(_allKeysCacheKey, keys);
            return keys;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            var obj = _memoryCache.Get(key);

            if (obj is T value)
                return Task.FromResult(value);

            return Task.FromResult<T?>(default);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? DefaultExpiration
            };

            _memoryCache.Set(key, value, options);

            var keys = GetKeySet();
            keys.TryAdd(key, 0);

            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync<T>(string key)
        {
            if (_memoryCache.TryGetValue(key, out var value) && value is T typedValue)
            {
                if (typedValue is IEnumerable<object> collection)
                    return Task.FromResult(collection.Any());

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);

            var keys = GetKeySet();
            keys.TryRemove(key, out _);

            return Task.CompletedTask;
        }

        public Task<List<string>> GetAllKeysAsync()
        {
            var keys = GetKeySet();
            return Task.FromResult(keys.Keys.ToList());
        }

        public Task ClearAllAsync()
        {
            var keys = GetKeySet();

            foreach (var key in keys.Keys)
                _memoryCache.Remove(key);

            keys.Clear();
            return Task.CompletedTask;
        }

        public Task RemoveByPrefixAsync(string prefix)
        {
            var keys = GetKeySet();

            foreach (var key in keys.Keys.Where(k => k.StartsWith(prefix)).ToList())
            {
                _memoryCache.Remove(key);
                keys.TryRemove(key, out _);
            }

            return Task.CompletedTask;
        }
    }

}
