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
            RuleFor(field => field.Type)
                .IsInEnum()
                .WithMessage($"Tipo não suportado, considere as opções entre: {StringUtils.GetAvailableValues<AccountTypeEnum>()}");
        }
    }
}