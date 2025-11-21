using FinCashly.Application.Transactions.Commands.CreateTransaction;
using FinCashly.Application.Users.Commands.CreateUser;
using FluentValidation;
using FluentValidation.AspNetCore;
namespace FinCashly.API.Configurations; 

public static class Validations
{
    public static IServiceCollection EnableFluentValidations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateAccountValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateTransactionValidator>();

        return services; 
    }
    
}