using FinCashly.Domain.Common.interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FinCashly.Infrastructure.Cache
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration)
        {
            return await _cache.GetOrCreateAsync(
                key,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = expiration; 
                    return await factory();
                });
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}