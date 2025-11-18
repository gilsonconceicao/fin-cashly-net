using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.DataBase;

namespace FinCashly.Infrastructure.Repositories;

public class AccountRepository : RepositoryBase<User>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}