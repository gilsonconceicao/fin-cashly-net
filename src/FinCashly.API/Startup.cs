using System.Reflection;
using FinCashly.API.Configurations;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        services.AddEndpointsApiExplorer();
        services.AddDependencyInjections();
        services.ConnectionWithDataBase(connectionString);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Fin Cashly",
                Description = "Management your transactions",
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        });

        services.AddControllers();

        // var postgreSql = GetPostgreSql(services);
        var logger = GetLogger(services);

        try
        {
            // postgreSql.MigrateAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Error on migration");
            Console.WriteLine("Não foi possível concluir a migração do DB." + ex.ToString());
            throw;
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        app.UseSwagger(o =>
        {
            o.RouteTemplate = "docs/{documentName}/docs.json";
        });
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "docs";
            c.SwaggerEndpoint("/docs/v1/docs.json", "FoodAPI");

            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });

        app.UseRouting();
        
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

    // private static DataBaseContext GetPostgreSql(IServiceCollection services)
    // {
    //     return (DataBaseContext)services.BuildServiceProvider().GetService(typeof(DataBaseContext))!;
    // }

    private static ILogger<Startup> GetLogger(IServiceCollection services)
    {
        return (ILogger<Startup>)services.BuildServiceProvider().GetService(typeof(ILogger<Startup>))!;
    }

}