using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Domain.Repositories;

public interface IAccountRepository : IRepositoryBase<Account>
{
        Task<Paginated<Account>> GetAccountsPaginated(int Page = 0, int size = 5);
}