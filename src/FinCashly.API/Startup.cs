using FinCashly.API.Configurations;
using FinCashly.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
        services.EnableFluentValidations();
        services.AddMediators();
        services.AddRepositories();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Fin Cashly",
                Description = "Management your transactions",
            });
            options.SchemaFilter<SchemeFilterSwashbuckle>();

            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var xmlFile in xmlFiles)
            {
                options.IncludeXmlComments(xmlFile, includeControllerXmlComments: true);
            }
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

    private static ILogger<Startup> GetLogger(IServiceCollection services)
    {
        return (ILogger<Startup>)services.BuildServiceProvider().GetService(typeof(ILogger<Startup>))!;
    }

}