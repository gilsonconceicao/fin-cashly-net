using FinCashly.Infrastructure.Settings;

namespace FinCashly.API.Configurations
{
    public static class AddRunSeendFile
    {
        public static IServiceCollection AddRunSeendFileExecute (this IServiceCollection services, IConfiguration configuration)
        {
            var enableRunSeedFile = configuration.GetSection("FeatureFlagsSettings").Get<FeatureFlagsSettings>();
            return services;
        }
    }
}