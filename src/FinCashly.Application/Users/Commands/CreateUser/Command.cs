using FinCashly.Application.Common.DTOs;
using MediatR;

namespace FinCashly.Application.Users.Commands.CreateUser;
#nullable disable
public class CreateUserCommand : IRequest<Guid>
{
    public CreateUserDto Payload { get; set; }
}