using System.Reflection;
using FinCashly.Application.Users.GetUsersList;
using MediatR;

namespace FinCashly.API.Configurations; 

public static class DependencyInjections
{
    public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddTransient<IRequestHandler<GetUsersListQuery, List<string>>, GetUsersListHandler>();
        return services; 
    }
    
}