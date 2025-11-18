using FinCashly.Application.Accounts.Commands.CreateAccount;
using FinCashly.Application.Goals.Commands.CreateAccount;

namespace FinCashly.Application.Users.Commands.CreateUser;
#nullable disable
public class CreateUserDto
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
    public List<CreateAccountByUserDto> Accounsts { get; set; } = new List<CreateAccountByUserDto>();

    /// <summary>
    /// Objetivos financeiros do usuário
    /// </summary>
    public List<CreateGoalByUserDto> Goals { get; set; } = new List<CreateGoalByUserDto>();

}