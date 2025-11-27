using FirebaseAdmin.Auth;
namespace FinCashly.API.Auth;
public static class FirebaseRoleManager
{
    public static async Task SetUserRoleAsync(string uid, string role)
    {
        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, new Dictionary<string, object>
        {
            { "role", role }
        });
    }

    public static async Task SetUserRolesAsync(string uid, IEnumerable<string> roles)
    {
        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, new Dictionary<string, object>
        {
            { "roles", roles }
        });
    }
}
