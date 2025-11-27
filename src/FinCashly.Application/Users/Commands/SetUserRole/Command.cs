using MediatR;
namespace FinCashly.Application.Users.Commands.SetUserRole;
public record SetUserRoleCommand(string UserId, string Role) : IRequest<bool>;