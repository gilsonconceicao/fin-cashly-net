namespace FinCashly.Domain.Repositories;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IAccountRepository Accounts { get; }
    ITransactionsRepository Transactions { get; }
    ICategoryRepository Categories { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveChangesAsync();
}