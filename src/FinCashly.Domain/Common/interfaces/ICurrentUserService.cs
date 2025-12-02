namespace FinCashly.Domain.Common.interfaces;

public interface ICurrentUserService
{
    string UserId { get; }
    string Email { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
}