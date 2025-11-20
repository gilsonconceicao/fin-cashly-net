using FinCashly.Application.Accounts.Commands.CreateAccount;
using FinCashly.Application.Accounts.Commands.DeleteAccount;
using FinCashly.Application.Accounts.Commands.UpdateAccount;
using FinCashly.Application.Accounts.Queries.GetAccountsList;
using FinCashly.Application.Users.Commands.CreateUser;
using FinCashly.Application.Users.Commands.DeleteUser;
using FinCashly.Application.Users.Commands.UpdateUser;
using FinCashly.Application.Users.Queries.GetUsersList;
using FinCashly.Domain.Common;
using MediatR;
namespace FinCashly.API.Configurations;

public static class Mediators
{
    public static IServiceCollection AddMediators(this IServiceCollection services)
    {
        #region User
        services.AddTransient<IRequestHandler<GetUsersListQuery, Paginated<GetUserPaginatedDto>>, GetUsersListHandler>();
        services.AddTransient<IRequestHandler<CreateUserCommand, Guid>, CreateUserHandler>();
        services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUserHandler>();
        services.AddTransient<IRequestHandler<UpdateUserCommand, Guid>, UpdateUserHandler>();
        #endregion
        #region Accounts
        services.AddTransient<IRequestHandler<GetAccountsListQuery, Paginated<GetAccountsListDto>>, GetAccountsListHandler>();
        services.AddTransient<IRequestHandler<CreateAccountCommand, Guid>, CreateAccountHandler>();
        services.AddTransient<IRequestHandler<UpdateAccountCommand, Guid>, UpdateAccountHandler>();
        services.AddTransient<IRequestHandler<DeleteAccountCommand, bool>, DeleteAccountHandler>();
        #endregion
        return services;
    }

}