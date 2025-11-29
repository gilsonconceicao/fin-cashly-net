using System.Security.Claims;
using System.Text.Encodings.Web;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
#nullable disable
namespace FinCashly.API.Auth;

public class FirebaseAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    [Obsolete]
    public FirebaseAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
    ) : base(options, logger, encoder, clock) { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Authorization header not found");

        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, decodedToken.Uid),
                new Claim("uid", decodedToken.Uid)
            };

            if (decodedToken.Claims.TryGetValue("email", out var email))
            {
                claims.Add(new Claim("email", email.ToString()));
            }

            if (decodedToken.Claims.TryGetValue("name", out var name))
            {
                claims.Add(new Claim("name", name.ToString()));
            }

            if (decodedToken.Claims.TryGetValue("role", out var role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            if (decodedToken.Claims.TryGetValue("permissions", out var permsObj)
                && permsObj is IEnumerable<object> perms)
            {
                claims.AddRange(
                    perms.Select(p => new Claim("permission", p.ToString()))
                );
            }

            var identity = new ClaimsIdentity(claims, nameof(FirebaseAuthHandler));
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Invalid Firebase token: {ex.Message}");
        }
    }
}
