using Serilog;

namespace FinCashly.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithCorrelationId()
                .Enrich.WithCorrelationIdHeader("X-Correlation-ID")
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithThreadId()
                .WriteTo.Console(outputTemplate: OutputTemplateFormat()
                )
                .CreateBootstrapLogger();
            try
            {
                Log.Information("Starting FinCashly API");

                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static string OutputTemplateFormat()
        {
            return "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] " + "[{EnvironmentName}] " +
                "[{MachineName}] " + "[CID:{CorrelationId}] " + "{Message:lj}{NewLine}{Exception}";
        } 

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) =>
                {
                    configuration
                        .MinimumLevel.Debug()
                        .WriteTo.Console(
                        outputTemplate:
                        "[{Timestamp:HH:mm:ss} {Level:u3}] " +
                        "{Message:lj}{NewLine}{Exception}")
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}