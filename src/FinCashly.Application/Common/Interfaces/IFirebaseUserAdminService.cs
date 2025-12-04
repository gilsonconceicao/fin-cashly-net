using FirebaseAdmin.Auth;

namespace FinCashly.Application.Common.Interfaces;

public interface IFirebaseService
{
    Task SetRoleAsync(string uid, string rule);
    Task<string?> GetRoleAsync(string uid);
    Task<UserRecord> GetUserByIdAsync(string uid );
}