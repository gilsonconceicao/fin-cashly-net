using FinCashly.Application.Common.DTOs;
using MediatR;

namespace FinCashly.Application.Users.Commands.UpdateUser;
#nullable disable
public class UpdateUserCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public UpdateUserDto Payload { get; set; }
}