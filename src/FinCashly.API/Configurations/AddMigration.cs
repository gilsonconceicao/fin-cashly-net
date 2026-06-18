using FinCashly.Domain.Settings;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FinCashly.API.Configurations
{
    public static class AddMigration
    {
        public static IApplicationBuilder MigrationManagement(this IApplicationBuilder app, IConfiguration configuration)
        {
            var featureFlagsSettings = configuration.GetSection("FeatureFlagsSettings").Get<FeatureFlagsSettings>();

            if (featureFlagsSettings is null)
            {
                return app;
            }

            if (featureFlagsSettings?.EnableRunMigrateDb == true)
            {
                RunMigration(app);
            }

            return app;
        }

        public static IApplicationBuilder ConfigureTraceCorrolationIdentifier(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                var correlationId =
                    context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                    ?? context.TraceIdentifier;

                using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
                {
                    context.Response.Headers["X-Correlation-ID"] = correlationId;
                    await next();
                }
            });
        }

        private static void RunMigration(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                logger.LogInformation("Aplicando migrations...");
                context.Database.Migrate();
                logger.LogInformation("Migrations aplicadas com sucesso.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Não foi possível rodar a migration");
            }
        }
    }
}