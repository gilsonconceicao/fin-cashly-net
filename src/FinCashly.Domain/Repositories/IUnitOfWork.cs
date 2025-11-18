namespace FinCashly.Domain.Repositories;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IAccountRepository Accounts { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveChangesAsync();
}