using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.Repositories;
namespace FinCashly.API.Configurations;

public static class Repositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITransactionsRepository, TransactionRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        return services;
    }

}