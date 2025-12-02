using FinCashly.Domain.Common;
using FinCashly.Domain.Common.interfaces;
using FinCashly.Domain.Entities;

namespace FinCashly.Domain.Repositories;

public interface IAccountRepository : IRepositoryBase<Account>
{
        Task<Paginated<Account>> GetAccountsPaginated(ICurrentUserService currentUserService, int Page = 0, int size = 5);
}