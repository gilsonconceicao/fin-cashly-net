using FinCashly.Application.Common.Interfaces;
using FinCashly.Domain.Security;
using FirebaseAdmin.Auth;

namespace FinCashly.Infrastructure.Firebase;

public class FirebaseUserAdminService : IFirebaseUserAdminService
{
    public async Task SetRoleAsync(string uid, string role)
    {
        if (!RolePermissions.Map.TryGetValue(role.ToString(), out var permissions))
            throw new ArgumentException($"permissão '{role}' não encontrada.");

        var claims = new Dictionary<string, object>
        {
            { "role", role },
            { "permissions", permissions }
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
