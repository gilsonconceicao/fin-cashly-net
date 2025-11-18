using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Domain.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<Paginated<User>> GetUsersPaginatedList(int Page = 0, int size = 5);

}