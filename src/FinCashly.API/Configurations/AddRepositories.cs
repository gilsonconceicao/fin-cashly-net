using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.Repositories;
namespace FinCashly.API.Configurations;

public static class Repositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

}