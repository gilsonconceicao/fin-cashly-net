using FinCashly.Domain.Common;
using FinCashly.Domain.Common.interfaces;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FinCashly.Infrastructure.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Paginated<Category>> GetCategoriesPaginatedList(ICurrentUserService currentUserService, int page = 0, int size = 5)
    {
        int skipCount = page * size;
        var dataAll = DbContext.Categories;

        var data = await dataAll
                        .Include(u => u.Transactions.Where(x => !x.IsDeleted))
                        .Include(u => u.Goals.Where(x => !x.IsDeleted))
                        .Where(c => c.CreatedById == currentUserService.UserId)
                        .Where(e => e.IsDeleted == false)
                        .OrderBy(e => e.CreatedAt)
                        .Skip(skipCount)
                        .Take(size)
                        .ToListAsync();

        return new Paginated<Category>
        {
            Data = data,
            Page = page,
            TotalItems = dataAll.Count(),
            TotalPages = (int)Math.Ceiling(dataAll.Count() / (double)size)
        };
    }
}