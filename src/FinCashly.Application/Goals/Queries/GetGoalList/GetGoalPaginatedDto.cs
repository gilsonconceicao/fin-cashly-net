using FinCashly.Application.Common.DTOs;

#nullable disable

namespace FinCashly.Application.Goals.Queries.GetGoalList;

public class GetGoalPaginatedDto : EntityBaseDto
{
    /// <summary>
    /// Título do objetivo
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Valor no qual almeija alcançar
    /// </summary>
    public decimal TargetAmount { get; set; }

    /// <summary>
    /// Saldo atual no momento onde foi definido o obejtivo
    /// </summary>
    public decimal CurrentAmount { get; set; } = 0;
}