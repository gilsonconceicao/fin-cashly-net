namespace FinCashly.Application.Common.DTOs;
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
    public IList<CreateAccountByUserDto> Accounsts { get; set; } = new List<CreateAccountByUserDto>();

    /// <summary>
    /// Objetivos financeiros do usuário
    /// </summary>
    public IList<CreateGoalByUserDto> Goals { get; set; } = new List<CreateGoalByUserDto>();

}