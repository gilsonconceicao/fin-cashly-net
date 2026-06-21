using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FinCashly.Infrastructure.BackgroundServices;

public class GoalMonitoringBackgroundService : BackgroundServiceBase
{
    public GoalMonitoringBackgroundService(
        ILogger<GoalMonitoringBackgroundService> logger,
        IServiceScopeFactory serviceScope) : base(logger, serviceScope)
    {
    }

    protected override TimeSpan Interval => TimeSpan.FromMinutes(5);

    protected override async Task ProcessAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var uow = serviceProvider.GetRequiredService<IUnitOfWork>();

        try
        {
            var goals = await uow.GoalRepository.GetAllAsync();
            var goalsAsCompletedList = goals.Where(g => g.CurrentAmount >= g.TargetAmount && !g.IsDeleted && !g.IsCompleted).ToList();
            if (goalsAsCompletedList.Count <= 0)
            {
                Logger.LogInformation("[{ServiceName}] Nenhuna meta foi concluída, Log as {date}", GetType().Name, DateTime.UtcNow);
            }

            await uow.BeginTransactionAsync();

            foreach (var goal in goalsAsCompletedList)
            {
                goal.IsCompleted = true;
                await uow.GoalRepository.UpdateAsync(goal);
            }

            await uow.CommitTransactionAsync();
            Logger.LogInformation("[{ServiceName}] Um total de {total} metas atualizada(s)", GetType().Name, goalsAsCompletedList.Count);

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            await uow.RollbackTransactionAsync();
            Logger.LogError("[{ServiceName}] Ocorreu um erro: {ex}", GetType().Name, ex);

        }
    }
}