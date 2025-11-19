using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
#nullable disable

namespace FinCashly.Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    protected readonly ApplicationDbContext DbContext;

    protected RepositoryBase(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<Paginated<TEntity>> GetGenericPaginatedList(int page, int size)
    {
        int skipCount = page * size; 
        
        var dataAll = DbContext.Set<TEntity>(); 

        var data = await dataAll
                        .Where(e => e.IsDeleted == false)
                        .OrderBy(e => e.CreatedAt)
                        .Skip(skipCount)
                        .Take(size)
                        .ToListAsync();

        return new Paginated<TEntity>
        {
            Data = data, 
            Page = page,
            TotalItems = dataAll.Count(),
            TotalPages = (int)Math.Ceiling(dataAll.Count() / (double)size)
        };
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);

    }

    public Task DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        DbContext.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteForEverAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }
}