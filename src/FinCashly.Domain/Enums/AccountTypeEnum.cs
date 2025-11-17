using System.ComponentModel;

namespace FinCashly.Domain.Enums;

public enum AccountTypeEnum
{
    /// <summary>
    /// Conta corrente
    /// </summary>
    [Description("Conta corrente")]
    Checking = 1,
    
    /// <summary>
    /// Conta poupança
    /// </summary>
    [Description("Conta poupança")]
    Savings = 2,

    /// <summary>
    /// Conta de investimento
    /// </summary>
    [Description("Conta de investimento")]
    Investment = 3,
    
    /// <summary>
    /// Dinheiro em espécie
    /// </summary>
    [Description("Dinheiro em espécie")]
    Cash = 4,

    /// <summary>
    /// Cartão de crédito
    /// </summary>
    [Description("Cartão de crédito")]
    Credit = 5
}