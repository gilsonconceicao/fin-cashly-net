using FinCashly.Application.Accounts.Queries.GetAccounts;
using FinCashly.Application.Common.DTOs;
using FinCashly.Application.Goals.Queries.GetGoal;
#nullable disable
namespace FinCashly.Application.Users.Queries.GetUsersList;
public class GetUserPaginatedDto : EntityBaseDto
{
    /// <summary>
    /// Nome do usuário
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Email do usuário
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Contas bancárias do usuário
    /// </summary>
    public List<GetAccountDto> Accounsts { get; set; } = new List<GetAccountDto>();

    /// <summary>
    /// Objetivos financeiros do usuário
    /// </summary>
    public List<GetGoalDto> Goals { get; set; } = new List<GetGoalDto>();

}