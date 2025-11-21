using FinCashly.Domain.Enums;

namespace FinCashly.Application.Accounts.Queries.GetAccounts;
#nullable disable
public class GetAccountDto
{
    public Guid Id { get; set; }
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
}