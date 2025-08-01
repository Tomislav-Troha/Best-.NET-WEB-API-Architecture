﻿namespace BestArchitecture.Application.Repositories.Cache
{
    public interface IMemoryCacheRepository
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
        Task<bool> ExistsAsync<T>(string key);
        Task RemoveAsync(string key);
        Task<List<string>> GetAllKeysAsync();
        Task ClearAllAsync();
        Task RemoveByPrefixAsync(string prefix);

    }
}
