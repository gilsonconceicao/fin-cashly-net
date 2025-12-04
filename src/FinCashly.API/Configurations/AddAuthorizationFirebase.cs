using FinCashly.API.Auth;
using FinCashly.API.Extensions;
using FinCashly.API.Services;
using FinCashly.Application.Common.Interfaces;
using FinCashly.Domain.Common.interfaces;
using FinCashly.Domain.Security;
using FinCashly.Infrastructure.Firebase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
namespace FinCashly.API.Configurations;

public static class Auth
{
    public static IServiceCollection AddAuthorizationFirebase(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        services.AddScoped<IFirebaseService, FirebaseService>();
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationResultHandler>();

        services.AddSingleton<FirebaseConnectService>();

        var firebaseconnect = new FirebaseConnectService(configuration);

        services.AddAuthentication("FirebaseAuth")
            .AddScheme<AuthenticationSchemeOptions, FirebaseAuthHandler>("FirebaseAuth", options => { });

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder("FirebaseAuth")
                .RequireAuthenticatedUser()
                .Build();

            foreach (var rolePerm in RolePermissions.Map.SelectMany(x => x.Value))
            {
                options.AddPolicy(rolePerm, policy =>
                    policy.Requirements.Add(new PermissionRequirement(rolePerm)));
            }
        });

        return services;
    }
}