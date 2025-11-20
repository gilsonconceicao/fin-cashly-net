using FinCashly.Application.Utils;
using FinCashly.Domain.Enums;
using FluentValidation;

#nullable disable
namespace FinCashly.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountValidator : AbstractValidator<UpdateAccountDto>
    {
        public UpdateAccountValidator()
        {
            RuleFor(field => field.Balance)
               .NotNull()
                .WithMessage("Valor precisa ser informado")
                .Must(value => value >= 0)
                .WithMessage("Valor não pode ser negativo");

            RuleFor(field => field.Name)
               .NotNull()
                .WithMessage("Nome da conta precisa ser informado")
                .NotEmpty()
                .WithMessage("Nome da conta não pode ser vazio");

            RuleFor(field => field.Type)
                .NotNull()
                .WithMessage("Tipo de conta precisa ser informado")
                .NotEmpty()
                .WithMessage("Tipo de conta não pode ser vazio")
                .IsInEnum()
                .WithMessage($"Tipo não suportado, considere as opções entre: {string.Join(", ", Enum.GetValues<AccountTypeEnum>().Select(v => $"{(int)v}: {v.GetDescription()}"))}");
        }
    }
}