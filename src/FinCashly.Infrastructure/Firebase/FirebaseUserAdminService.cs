using FinCashly.Application.Common.Interfaces;
using FirebaseAdmin.Auth;

namespace FinCashly.Infrastructure.Firebase;

public class FirebaseUserAdminService : IFirebaseUserAdminService
{
    public async Task SetRoleAsync(string uid, string role)
    {
        var claims = new Dictionary<string, object>
        {
            { "role", role }
        };

        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, claims);
    }

    public async Task<string?> GetRoleAsync(string uid)
    {
        var user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

        if (user.CustomClaims.TryGetValue("role", out var roleObj))
            return roleObj?.ToString();

        return null;
    }
}
