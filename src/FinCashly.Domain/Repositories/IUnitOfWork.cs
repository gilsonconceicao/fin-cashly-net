namespace FinCashly.Domain.Repositories;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveChangesAsync();
}