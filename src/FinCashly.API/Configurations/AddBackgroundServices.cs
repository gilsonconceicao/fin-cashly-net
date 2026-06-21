using FinCashly.Infrastructure.BackgroundServices;
namespace FinCashly.API.Configurations;

public static class BackgroundServices
{
    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<GoalMonitoringBackgroundService>();
        return services;
    }

}