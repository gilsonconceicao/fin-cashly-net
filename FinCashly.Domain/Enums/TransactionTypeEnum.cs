using System.ComponentModel;

namespace FinCashly.Domain.Enums;

public enum TransactionTypeEnum
{
    [Description("Saída")]
    Expense = 1,
    
    [Description("Entrada")]
    Income = 2
}