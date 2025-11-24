using FinCashly.Application.Transactions.Commands.UpdateTransaction;
using FinCashly.Application.Utils;
using FinCashly.Domain.Enums;
using FluentValidation;

namespace FinCashly.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionDto>
{
    public UpdateTransactionValidator()
    {
        RuleFor(field => field.Amount)
            .Must(value => value > 0)
            .WithMessage("Valor não pode ser negativo ou zero");

        RuleFor(field => field.Type)
            .IsInEnum()
            .WithMessage($"Tipo não suportado, considere as opções: {StringUtils.GetAvailableValues<TransactionTypeEnum>()}");
    }
}