using System.Reflection;
using MediatR;

namespace FinCashly.API.Configurations;

public static class DependencyInjections
{
    public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }

}