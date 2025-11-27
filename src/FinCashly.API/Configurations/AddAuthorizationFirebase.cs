using FinCashly.API.Auth;
using FinCashly.API.Services;
using FinCashly.Application.Common.Interfaces;
using FinCashly.Infrastructure.Firebase;
using FinCashly.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
namespace FinCashly.API.Configurations;

public static class Auth
{
    public static IServiceCollection AddAuthorizationFirebase(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IFirebaseUserAdminService, FirebaseUserAdminService>();
        services.AddSingleton<FirebaseConnectService>();
        
        var firebaseconnect = new FirebaseConnectService(configuration);

        services.AddAuthentication("FirebaseAuth")
            .AddScheme<AuthenticationSchemeOptions, FirebaseAuthHandler>("FirebaseAuth", options => { });

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder("FirebaseAuth")
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;
    }
}