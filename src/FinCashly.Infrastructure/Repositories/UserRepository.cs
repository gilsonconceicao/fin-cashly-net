using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FinCashly.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Paginated<User>> GetUsersPaginatedList(int page = 0, int size = 5)
    {
        int skipCount = page * size;

        var dataAll = DbContext.Users;

        var data = await dataAll
                        .Include(u => u.Accounts.Where(acc => !acc.IsDeleted))
                        .Include(u => u.Goals.Where(g => !g.IsDeleted))
                        .Where(e => e.IsDeleted == false)
                        .OrderBy(e => e.CreatedAt)
                        .Skip(skipCount)
                        .Take(size)
                        .ToListAsync();

        return new Paginated<User>
        {
            Data = data,
            Page = page,
            TotalItems = dataAll.Count(),
            TotalPages = (int)Math.Ceiling(dataAll.Count() / (double)size)
        };
    }
}