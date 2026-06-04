using FinCashly.API.Configurations;
using FinCashly.API.Extensions;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

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
        services.AddDependencyInjections();
        services.AddSwaggerSetting();
        services.ConnectionWithDataBase(_configuration);
        services.AddAuthorizationFirebase(_configuration);
        services.EnableFluentValidations();
        services.AddMediators();
        services.AddRepositories();
        services.AddHttpContextAccessor();
        services.AddAddRateLimitingMiddleware();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        services.AddControllers(opt =>
        {
            opt.Filters.Add<CustomExceptionFilter>();
        });

        var logger = GetLogger(services);

        try
        {
            logger.LogInformation("Application started");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Error on migration (Não foi possível concluir a migração do DB)");
            Console.WriteLine("Não foi possível concluir a migração do DB." + ex.ToString());
            throw;
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            RunMigration(app);
        }

        app.UseExceptionHandler(exceptionHandlerApp =>
            exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));

        app.UseStatusCodePages(async statusCodeContext =>
            await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
            .ExecuteAsync(statusCodeContext.HttpContext));

        app.UseSwagger(o =>
        {
            o.RouteTemplate = "docs/{documentName}/docs.json";
        });
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "docs";
            c.SwaggerEndpoint("/docs/v1/docs.json", "FinCashlyAPI");

            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
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
    private void RunMigration(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        logger.LogInformation("Aplicando migrations...");

        context.Database.Migrate();

        logger.LogInformation("Migrations aplicadas com sucesso.");
    }
    private static ILogger<Startup> GetLogger(IServiceCollection services)
    {
        return (ILogger<Startup>)services.BuildServiceProvider().GetService(typeof(ILogger<Startup>))!;
    }

}