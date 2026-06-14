using FirebaseAdmin.Auth;

namespace FinCashly.Domain.Common.interfaces
{
    public interface IFirebaseService
    {
        Task SetRoleAsync(string uid, string rule);
        Task<string?> GetRoleAsync(string uid);
        Task<UserRecord> GetUserByIdAsync(string uid);
    }
}