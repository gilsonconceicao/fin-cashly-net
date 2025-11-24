using FinCashly.Domain.Enums;

namespace FinCashly.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionDto
    {
        public Guid? CategoryId { get; set; } = null;
        public decimal Amount { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
    }
}