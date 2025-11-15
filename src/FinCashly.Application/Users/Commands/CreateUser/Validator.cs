using FluentValidation;
using MediatR;

namespace FinCashly.Application.Users.Commands.CreateUser;
#nullable disable
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(field => field.Email)
            .NotNull()
            .WithMessage("Email precisa ser informado")
            .NotEmpty()
            .WithMessage("Email não pode ser vazio")
            .EmailAddress()
            .WithMessage("Endereço de e-mail está no formato incorreto");

        RuleFor(field => field.Name)
             .NotNull()
            .WithMessage("Nome precisa ser informado")
            .NotEmpty()
            .WithMessage("Nome não pode ser vazio");
    }
}