using FinCashly.Domain.Common;

namespace FinCashly.Domain.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteForEverAsync(T entity);
    Task DeleteAsync(T entity);
}