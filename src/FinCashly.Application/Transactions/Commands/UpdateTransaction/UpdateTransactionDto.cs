using FinCashly.Domain.Enums;

namespace FinCashly.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionDto
{
    public Guid? CategoryId { get; set; } = null;
    public decimal? Amount { get; set; } = null;
    public TransactionTypeEnum? Type { get; set; } = null;
    public string? Description { get; set; } = null;
}