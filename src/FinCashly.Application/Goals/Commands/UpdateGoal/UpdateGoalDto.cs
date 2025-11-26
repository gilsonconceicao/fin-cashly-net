namespace FinCashly.Application.Goals.Commands.UpdateGoal;

public class UpdateGoalDto
{
    /// <summary>
    /// Título do objetivo
    /// </summary>
    public string? Title { get; set; } = null;
    
    /// <summary>
    /// Valor no qual almeija alcançar
    /// </summary>
    public decimal? TargetAmount { get; set; } = null;
    
    /// <summary>
    /// Saldo atual no momento onde foi definido o obejtivo
    /// </summary>
    public decimal? CurrentAmount { get; set; } = null;
}