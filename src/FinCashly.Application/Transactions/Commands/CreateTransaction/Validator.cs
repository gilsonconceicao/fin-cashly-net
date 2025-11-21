using FinCashly.Application.Utils;
using FinCashly.Domain.Enums;
using FluentValidation;

namespace FinCashly.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionDto>
{
    public CreateTransactionValidator()
    {
        RuleFor(field => field.Amount)
            .NotNull()
            .WithMessage("Valor da transação precisa ser informado")
            .NotEmpty()
            .WithMessage("Valor da transação não pode ser vazio")
            .Must(value => value > 0)
            .WithMessage("Valor não pode ser negativo ou zero");

        RuleFor(field => field.Date.Date)
            .LessThanOrEqualTo(DateTime.Now.Date) // ignore time
            .WithMessage("Data da transação não pode estar no futuro.");
      
        RuleFor(field => field.Type)
           .NotNull()
                .WithMessage("Tipo de transação precisa ser informado")
                .NotEmpty()
                .WithMessage("Tipo de transação não pode ser vazio")
                .IsInEnum()
                .WithMessage($"Tipo não suportado, considere as opções: {string.Join(", ", Enum.GetValues<TransactionTypeEnum>().Select(v => $"{(int)v}: {v.GetDescription()}"))}");
    }
}