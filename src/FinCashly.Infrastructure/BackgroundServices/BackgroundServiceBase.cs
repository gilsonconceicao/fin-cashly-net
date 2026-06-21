using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FinCashly.Infrastructure.BackgroundServices;

public abstract class BackgroundServiceBase : BackgroundService
{
    protected readonly ILogger Logger;
    protected readonly IServiceScopeFactory ServiceScope;

    protected BackgroundServiceBase(
        ILogger logger,
        IServiceScopeFactory serviceScope)
    {
        Logger = logger;
        ServiceScope = serviceScope;
    }

    protected abstract TimeSpan Interval { get; }

    protected abstract Task ProcessAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("[{ServiceName}] Started at {StartedAt}", GetType().Name, DateTime.UtcNow);

        using var timer = new PeriodicTimer(Interval);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                Logger.LogInformation("[{ServiceName}] Execution started at {Time}",GetType().Name, DateTime.UtcNow);

                using var scope = ServiceScope.CreateScope();

                await ProcessAsync(scope.ServiceProvider, stoppingToken);
                Logger.LogInformation("[{ServiceName}] Execution finished at {Time}", GetType().Name, DateTime.UtcNow);
            }
            catch (OperationCanceledException)
            {
                Logger.LogInformation("[{ServiceName}] Cancellation requested",GetType().Name);
                break;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,"[{ServiceName}] Error during execution",GetType().Name);
            }
            finally
            {
                stopwatch.Stop();
                Logger.LogInformation("[{ServiceName}] Execution took {ElapsedMs}ms", GetType().Name, stopwatch.ElapsedMilliseconds);
            }
        }

        Logger.LogInformation("[{ServiceName}] Stopped at {StoppedAt}", GetType().Name, DateTime.UtcNow);
    }
}