using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Domain.Repositories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<Paginated<Category>> GetCategoriesPaginatedList(int Page = 0, int size = 5);
}