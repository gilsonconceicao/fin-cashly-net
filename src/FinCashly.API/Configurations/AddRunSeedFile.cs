using System.Text.Json;
using FinCashly.API.seed;
using FinCashly.Infrastructure.Settings;

namespace FinCashly.API.Configurations
{
    public static class AddRunSeendFile
    {
        public static IServiceCollection AddRunSeendFileExecute (this IServiceCollection services, IConfiguration configuration)
        {
            var featureFlagsSettings = configuration.GetSection("FeatureFlagsSettings").Get<FeatureFlagsSettings>();
            
            if (featureFlagsSettings?.EnableRunSeedFile == true)
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory; 
                string relativePath = Path.Combine(dir, "seed/FinCashlySeed.json"); 
                string jsonConfig = File.ReadAllText(relativePath); 
                var data = JsonSerializer.Deserialize<List<SeedFinCashly>>(jsonConfig);

                Console.WriteLine(JsonSerializer.Serialize(data));
            } 
            return services;
        }
    }
}