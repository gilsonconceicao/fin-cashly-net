using MediatR;

namespace FinCashly.Application.Users.Commands.CreateUser; 
#nullable disable
public class CreateUserCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }

}