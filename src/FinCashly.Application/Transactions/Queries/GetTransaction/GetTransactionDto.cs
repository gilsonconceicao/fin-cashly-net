using FinCashly.Application.Accounts.Queries.GetAccounts;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Enums;
#nullable disable
namespace FinCashly.Application.Transactions.Queries.GetTransaction;

public class GetTransactionDto
{
    public Guid AccountId { get; set; }

    public Guid? CategoryId { get; set; }

    public decimal Amount { get; set; }

    public TransactionTypeEnum Type { get; set; }
    public string TypeDisplay { get; set; }

    public string Description { get; set; }

    public DateTime Date { get; set; }

    public GetAccountDto Account { get; set; }
    public Category Category { get; set; }
}