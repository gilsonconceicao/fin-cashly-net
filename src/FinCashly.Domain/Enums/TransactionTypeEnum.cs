using System.ComponentModel;

namespace FinCashly.Domain.Enums;

public enum TransactionTypeEnum
{
    /// <summary>
    /// Saída
    /// </summary>
    [Description("Saída")]
    Expense = 1,

    /// <summary>
    /// Entrada
    /// </summary>
    [Description("Entrada")]
    Income = 2
}