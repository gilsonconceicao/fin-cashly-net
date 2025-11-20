using FinCashly.Domain.Enums;

namespace FinCashly.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountDto
{
    /// <summary>
    /// Nome da conta
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Saldo da conta
    /// </summary>
    public decimal? Balance { get; set; } 
    /// <summary>
    /// Tipo de conta bancária
    /// </summary>
    public AccountTypeEnum? Type { get; set; }

}