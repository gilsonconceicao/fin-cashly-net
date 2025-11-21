using FinCashly.Application.Common.DTOs;
using FinCashly.Application.Transactions.Queries.GetTransaction;
using FinCashly.Domain.Enums;

namespace FinCashly.Application.Accounts.Queries.GetAccountsList;
#nullable disable
public class GetAccountsListDto : EntityBaseDto
{
    public Guid UserId { get; set; }
    /// <summary>
    /// Nome da conta
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Saldo da conta
    /// </summary>
    public decimal Balance { get; set; } = 0;
    
    /// <summary>
    /// Tipo de conta bancária
    /// </summary>
    public AccountTypeEnum Type { get; set; } = AccountTypeEnum.Checking;
    public string TypeDisplay { get; set; }
    public List<GetTransactionDto> Transactions {get; set; } = new List<GetTransactionDto>();
}