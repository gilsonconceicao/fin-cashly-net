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

            RuleFor(field => field.Type)
                .IsInEnum()
                .WithMessage($"Tipo não suportado, considere as opções entre: {StringUtils.GetAvailableValues<AccountTypeEnum>()}");
        }
    }
}