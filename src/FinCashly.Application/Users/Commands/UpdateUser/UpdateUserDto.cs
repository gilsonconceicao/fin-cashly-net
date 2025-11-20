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

}