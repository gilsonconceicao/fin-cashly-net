using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Domain.Repositories;

public interface ITransactionsRepository : IRepositoryBase<Transaction>
{
    Task<Paginated<Transaction>> GetTransactionPaginated(int Page = 0, int size = 5);

}