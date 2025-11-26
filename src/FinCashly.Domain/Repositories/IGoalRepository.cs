using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Domain.Repositories;

public interface IGoalRepository : IRepositoryBase<Goal>
{
    Task<Paginated<Goal>> GetGoalsPaginatedList(int Page = 0, int size = 5);
}