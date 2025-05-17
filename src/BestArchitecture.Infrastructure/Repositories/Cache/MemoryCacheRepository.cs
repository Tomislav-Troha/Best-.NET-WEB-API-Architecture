
using Microsoft.Extensions.Caching.Memory;

namespace BestArchitecture.Domain.Repositories.Cache
{
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        private readonly IMemoryCache _memoryCache;
        private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(10);
        private const string _allKeysCacheKey = "__ALL_CACHE_KEYS__";

        public MemoryCacheRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _memoryCache.Set(_allKeysCacheKey, new HashSet<string>());
        }

        public Task<T?> GetAsync<T>(string key)
        {
            var obj = _memoryCache.Get(key);

            if (obj is null)
                return Task.FromResult<T?>(default);

            if (obj is T value)
                return Task.FromResult<T?>(value);

            return Task.FromResult<T?>(default);
        }



        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? DefaultExpiration
            };

            _memoryCache.Set(key, value, options);

            if (_memoryCache.TryGetValue(_allKeysCacheKey, out HashSet<string>? keys) && keys != null)
            {
                keys.Add(key);
                _memoryCache.Set(_allKeysCacheKey, keys);
            }

            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(string key)
        {
            return Task.FromResult(_memoryCache.TryGetValue(key, out _));
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);

            if (_memoryCache.TryGetValue(_allKeysCacheKey, out HashSet<string>? keys) && keys != null)
            {
                keys.Remove(key);
                _memoryCache.Set(_allKeysCacheKey, keys);
            }

            return Task.CompletedTask;
        }

        public Task<List<string>> GetAllKeysAsync()
        {
            if (_memoryCache.TryGetValue(_allKeysCacheKey, out HashSet<string>? keys) && keys != null)
                return Task.FromResult(keys.ToList());

            return Task.FromResult(new List<string>());
        }

        public Task ClearAllAsync()
        {
            if (_memoryCache.TryGetValue(_allKeysCacheKey, out HashSet<string>? keys) && keys != null)
            {
                foreach (var key in keys)
                    _memoryCache.Remove(key);

                keys.Clear();
                _memoryCache.Set(_allKeysCacheKey, keys);
            }

            return Task.CompletedTask;
        }

        public Task RemoveByPrefixAsync(string prefix)
        {
            if (_memoryCache.TryGetValue(_allKeysCacheKey, out HashSet<string>? keys) && keys != null)
            {
                var toRemove = keys.Where(k => k.StartsWith(prefix)).ToList();

                foreach (var key in toRemove)
                {
                    _memoryCache.Remove(key);
                    keys.Remove(key);
                }

                _memoryCache.Set(_allKeysCacheKey, keys);
            }

            return Task.CompletedTask;
        }
    }
}
