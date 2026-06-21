using FinCashly.API.Configurations;
using FinCashly.API.Extensions;
using FinCashly.Infrastructure.BackgroundServices;
using Serilog;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddEndpointsApiExplorer();
        services.RegisterDependencyInjection(_configuration);
        Log.Logger = new LoggerConfiguration().CreateLogger();

        services.AddHostedService<BackgroundServiceValidate>();

        services.AddControllers(opt =>
        {
            opt.Filters.Add<CustomExceptionFilter>();
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.MigrationManagement(_configuration);
        app.ConfigureTraceCorrolationIdentifier();

        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate =
                   "HTTP {RequestMethod} {RequestPath} => {StatusCode} in {Elapsed:0.0000} ms";
        });

        app.Use(async (context, next) =>
        {
            Log.Information("Incoming request {Method} {Path}", context.Request.Method, context.Request.Path);
            await next();
        });

        app.UseExceptionHandler(exceptionHandlerApp =>
            exceptionHandlerApp.Run(async context =>
                await Results.Problem().ExecuteAsync(context)));

        app.UseStatusCodePages(async statusCodeContext =>
            await Results.Problem(
                statusCode: statusCodeContext.HttpContext.Response.StatusCode)
            .ExecuteAsync(statusCodeContext.HttpContext));

        app.UseSwagger(o =>
        {
            o.RouteTemplate = "docs/{documentName}/docs.json";
        });

        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "docs";
            c.SwaggerEndpoint("/docs/v1/docs.json", "FinCashlyAPI");

            c.DocExpansion(
                Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });

        app.UseRouting();

        app.UseRateLimiter();

        app.UseCors(policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}