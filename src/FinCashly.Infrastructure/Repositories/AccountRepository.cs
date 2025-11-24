using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FinCashly.Infrastructure.Repositories;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Paginated<Account>> GetAccountsPaginated(int page = 0, int size = 5)
    {
        int skipCount = page * size;
        var dataAll = DbContext.Accounts;

        var data = await dataAll
                        .Include(u => u.Transactions.Where(x => !x.IsDeleted))
                        .Where(e => e.IsDeleted == false)
                        .OrderBy(e => e.CreatedAt)
                        .Skip(skipCount)
                        .Take(size)
                        .ToListAsync();

        return new Paginated<Account>
        {
            Data = data,
            Page = page,
            TotalItems = dataAll.Count(),
            TotalPages = (int)Math.Ceiling(dataAll.Count() / (double)size)
        };
    }
}