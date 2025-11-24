using FinCashly.Application.Categories.Commands.CreateCategory;
using FinCashly.Application.Categories.Commands.UpdateCategory;
using FinCashly.Application.Transactions.Commands.CreateTransaction;
using FinCashly.Application.Transactions.Commands.UpdateTransaction;
using FinCashly.Application.Users.Commands.CreateUser;
using FluentValidation;
using FluentValidation.AspNetCore;
namespace FinCashly.API.Configurations; 

public static class Validations
{
    public static IServiceCollection EnableFluentValidations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        #region User
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateAccountValidator>();
        #endregion

        #region Transaction
        services.AddValidatorsFromAssemblyContaining<CreateTransactionValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateTransactionValidator>();
        #endregion

        #region Categorias
        services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateCategoryValidator>();
        #endregion

        return services; 
    }
    
}