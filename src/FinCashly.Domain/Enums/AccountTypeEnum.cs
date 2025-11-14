using System.ComponentModel;

namespace FinCashly.Domain.Enums;

public enum AccountTypeEnum
{
    [Description("Conta corrente")]
    Checking = 1,

    [Description("Conta poupança")]
    Savings = 2,

    [Description("Conta de investimento")]
    Investment = 3,

    [Description("Dinheiro em espécie")]
    Cash = 4,
    
    [Description("Cartão de crédito")]
    Credit = 5
}