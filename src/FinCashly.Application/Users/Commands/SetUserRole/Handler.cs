using FinCashly.Application.Common.Interfaces;
using MediatR;
namespace FinCashly.Application.Users.Commands.SetUserRole;

public class SetUserRoleHandler : IRequestHandler<SetUserRoleCommand, bool>
{
    private readonly IFirebaseService _firebase;

    public SetUserRoleHandler(IFirebaseService firebase)
    {
        _firebase = firebase;
    }

    public async Task<bool> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        await _firebase.SetRoleAsync(request.UserId, request.Role);
        return true;
    }
}
