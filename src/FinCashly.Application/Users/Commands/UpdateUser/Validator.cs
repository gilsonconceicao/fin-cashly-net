using FinCashly.Application.Users.Commands.UpdateUser;
using FluentValidation;
using MediatR;

#nullable disable
namespace FinCashly.Application.Users.Commands.CreateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(field => field.Id)
               .NotNull()
                .WithMessage("Id do usuário precisa ser informado")
                .NotEmpty()
                .WithMessage("Id do usuário não pode ser vazio");
        }
    }
}