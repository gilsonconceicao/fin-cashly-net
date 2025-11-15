using FinCashly.Application.Users.Commands.CreateUser;
using FinCashly.Application.Users.Queries.GetUsersList;
using MediatR;
namespace FinCashly.API.Configurations;

public static class Mediators
{
    public static IServiceCollection AddMediators(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<GetUsersListQuery, List<string>>, GetUsersListHandler>();
        services.AddTransient<IRequestHandler<CreateUserCommand, Guid>, CreateUserHandler>();
        return services;
    }

}