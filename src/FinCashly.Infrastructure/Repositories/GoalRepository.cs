using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FinCashly.Infrastructure.Repositories;

public class GoalRepository : RepositoryBase<Goal>, IGoalRepository
{
    public GoalRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Paginated<Goal>> GetGoalsPaginatedList(int page = 0, int size = 5)
    {
        int skipCount = page * size;
        var dataAll = DbContext.Goals;

        var data = await dataAll
                        .Include(u => u.User)
                        .Where(e => e.IsDeleted == false)
                        .OrderBy(e => e.CreatedAt)
                        .Skip(skipCount)
                        .Take(size)
                        .ToListAsync();

        return new Paginated<Goal>
        {
            Data = data,
            Page = page,
            TotalItems = dataAll.Count(),
            TotalPages = (int)Math.Ceiling(dataAll.Count() / (double)size)
        };
    }
}