using FinCashly.Domain.Common;

namespace FinCashly.Domain.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<Paginated<T>> GetGenericPaginatedList(int Page = 0, int size = 5);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}