namespace FinCashly.Domain.Repositories;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    ITransactionsRepository TransactionsRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IGoalRepository GoalRepository { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveChangesAsync();
}