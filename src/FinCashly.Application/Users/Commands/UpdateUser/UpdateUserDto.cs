using FinCashly.Application.Accounts.Commands.UpdateAccount;
using FinCashly.Application.Goals.Commands.UpdateGoal;

namespace FinCashly.Application.Users.Commands.UpdateUser;
public class UpdateUserDto
{
    /// <summary>
    /// Nome do usuário
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Email do usuário
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Contas bancárias do usuário
    /// </summary>
    public List<UpdateAccountDto> Accounsts { get; set; } = new List<UpdateAccountDto>();

    /// <summary>
    /// Objetivos financeiros do usuário
    /// </summary>
    public List<UpdateGoalDto> Goals { get; set; } = new List<UpdateGoalDto>();

}