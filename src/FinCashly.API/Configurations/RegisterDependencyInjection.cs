using FinCashly.Domain.Settings;

namespace FinCashly.API.Configurations
{
    public static class Register
    {
        public static IServiceCollection RegisterDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerSetting();
            services.ConnectionWithDataBase(configuration);
            services.AddAuthorizationFirebase(configuration);
            services.EnableFluentValidations();
            services.AddMediators();
            services.AddRepositories();
            services.AddMemoryCacheService();
            services.AddHttpContextAccessor();
            services.AddRateLimitingService();

            services.Configure<FeatureFlagsSettings>(configuration.GetSection("FeatureFlagsSettings"));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}