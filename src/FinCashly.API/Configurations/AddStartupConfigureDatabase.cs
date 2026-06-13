using System.Text.Json;
using FinCashly.API.seed;
using FinCashly.Infrastructure.DataBase;
using FinCashly.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

namespace FinCashly.API.Configurations
{
    public static class AddStartupConfigureDatabase
    {
        public static IApplicationBuilder StartupConfigureDatabase(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
        {
            var featureFlagsSettings = configuration.GetSection("FeatureFlagsSettings").Get<FeatureFlagsSettings>();
            
            if (featureFlagsSettings is null)
            {
                return app;
            } 

            if (featureFlagsSettings?.EnableRunMigrateDb== true)
            {
                RunMigration(app);
            }

            if (featureFlagsSettings?.EnableRunSeedFile == true)
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = Path.Combine(dir, "seed/FinCashlySeed.json");
                string jsonConfig = File.ReadAllText(relativePath);
                var data = JsonSerializer.Deserialize<List<SeedFinCashly>>(jsonConfig);

                Console.WriteLine(JsonSerializer.Serialize(data));
            }
            return app;
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