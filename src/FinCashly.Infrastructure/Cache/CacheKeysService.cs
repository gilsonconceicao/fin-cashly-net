using FinCashly.Domain.Common.interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FinCashly.Infrastructure.Cache
{
    public class CacheKeysService : ICacheKeysService
    {
        public CacheKeysService() { }

        public string Categories(string userId)
        {
            return $"categories:{userId}";
        }

        public string Goals(string userId)
        {
            return $"goals:{userId}";
        }
    }
}