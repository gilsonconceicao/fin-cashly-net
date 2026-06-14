using System.Net;
using System.Reflection;
using System.Threading.RateLimiting;
using MediatR;
using Microsoft.AspNetCore.RateLimiting;

namespace FinCashly.API.Configurations;

public static class RateLimit
{
    public static IServiceCollection AddRateLimitingService(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
            {
                var partitionKey = (httpcontext?.User?.Identity?.Name ?? httpcontext?.Connection?.RemoteIpAddress?.ToString()) ?? "unknown-user";
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: partitionKey,
                    factory: partion => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 10,
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(1)
                    });
            });

            options.AddFixedWindowLimiter("fixed-custom", option =>
            {
                option.PermitLimit = 4;
                option.Window = TimeSpan.FromSeconds(12);
                option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                option.QueueLimit = 2;
            });

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    code = "RATE_LIMIT_EXCEEDED",
                    StatusCode = StatusCodes.Status429TooManyRequests,
                    message = "Too many requests. Please try again later."
                }, token);
                };
            });

        return services;
    }

}