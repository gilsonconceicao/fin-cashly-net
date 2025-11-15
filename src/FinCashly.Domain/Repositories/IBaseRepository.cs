namespace FinCashly.Domain.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IList<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}