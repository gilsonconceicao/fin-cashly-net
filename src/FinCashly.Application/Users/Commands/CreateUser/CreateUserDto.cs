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

}