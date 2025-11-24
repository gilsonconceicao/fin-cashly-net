using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FinCashly.Infrastructure.Repositories;

public class TransactionRepository : RepositoryBase<Transaction>, ITransactionsRepository
{
    public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Paginated<Transaction>> GetTransactionPaginated(int page = 0, int size = 5)
    {
        int skipCount = page * size;

        var dataAll = DbContext.Transactions;

        var data = await dataAll
                        .Include(u => u.Account)
                        .Include(u => u.Category)
                        .Where(e => e.IsDeleted == false)
                        .OrderBy(e => e.CreatedAt)
                        .Skip(skipCount)
                        .Take(size)
                        .ToListAsync();

        return new Paginated<Transaction>
        {
            Data = data,
            Page = page,
            TotalItems = dataAll.Count(),
            TotalPages = (int)Math.Ceiling(dataAll.Count() / (double)size)
        };
    }
}