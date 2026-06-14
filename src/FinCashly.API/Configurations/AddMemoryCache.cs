using FinCashly.Domain.Common.interfaces;
using FinCashly.Infrastructure.Cache;

namespace FinCashly.API.Configurations;

public static class MemoryCacheServices
{
    public static IServiceCollection AddMemoryCacheService(this IServiceCollection services)
    {
        services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
        services.AddSingleton<ICacheKeysService, CacheKeysService>();
        services.AddMemoryCache();
        return services;
    }

}