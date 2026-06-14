using System.Text.Json;
using FinCashly.API.seed;
using FinCashly.Application.Utils;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Enums;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using FinCashly.Domain.Settings;
using FinCashly.Infrastructure.DataBase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace FinCashly.Application.Transactions.Queries.GetTransactionList;

public class DatabaseSeederCommand : IRequest<bool>
{

}
public class DatabaseSeederCommandHandler : IRequestHandler<DatabaseSeederCommand, bool>
{
    private readonly IUnitOfWork _uow;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DatabaseSeederCommandHandler> _logger;
    private readonly FeatureFlagsSettings _featureFlagsSettings;

    public DatabaseSeederCommandHandler(IUnitOfWork unitOfWork, ILogger<DatabaseSeederCommandHandler> logger, IOptions<FeatureFlagsSettings> options, ApplicationDbContext applicationDbContext)
    {
        _uow = unitOfWork;
        _logger = logger;
        _featureFlagsSettings = options.Value;
        _context = applicationDbContext;
    }

    public async Task<bool> Handle(DatabaseSeederCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Iniciando processo seed...");

            if (_featureFlagsSettings.EnableRunSeedFile == false)
            {
                _logger.LogInformation("Ambiente não permite realizar processo para popular banco");
                throw new BusinessException("Ambiente não permite executar esse comando. Entre em contato com administrador.");
            }

            var jsonFile = GenericMethodsUtils.ReadFileTypeJson("seed/FinCashlySeed.json");

            if (string.IsNullOrWhiteSpace(jsonFile))
            {
                throw new BusinessException("Arquivo informado não é válido");
            }

            var data = JsonSerializer.Deserialize<List<SeedFinCashly>>(jsonFile) ?? new List<SeedFinCashly>();

            if (data.Count <= 0)
            {
                _logger.LogInformation("Não foi possivel processaar o arquivo. Arquivo sem conteúdo.");
                throw new BusinessException("Não foi possivel processaar o arquivo. Arquivo sem conteúdo");
            }

            await _uow.BeginTransactionAsync();

            var accountMap = new Dictionary<string, Guid>();
            var categoryMap = new Dictionary<string, Guid>();

            List<Account> accounts = new();
            List<Category> categories = new();
            List<Goal> goals = new();

            foreach (var item in data.Where(x => x.dbDestination == "account"))
            {
                var existingAccount = _context.Accounts.FirstOrDefault(acc =>
                    acc.Name == item.name &&
                    acc.Type == Enum.Parse<AccountTypeEnum>(item.type!));

                if (existingAccount != null)
                {
                    accountMap[item.seedId!] = existingAccount.Id;
                    continue;
                }

                var newAccount = new Account
                {
                    Name = item.name!,
                    Balance = item.balance ?? 0,
                    Type = Enum.Parse<AccountTypeEnum>(item.type!)
                };

                accountMap[item.seedId!] = newAccount.Id;
                accounts.Add(newAccount);
            }

            foreach (var item in data.Where(x => x.dbDestination == "category"))
            {
                var existingCategory = await _context.Categories
                    .FirstOrDefaultAsync(cat =>
                        cat.Name == item.name &&
                        cat.Type == Enum.Parse<CategoryTypeEnum>(item.type!),
                        cancellationToken);

                if (existingCategory != null)
                {
                    categoryMap[item.seedId!] = existingCategory.Id;
                    continue;
                }

                var category = new Category
                {
                    Name = item.name!,
                    Type = Enum.Parse<CategoryTypeEnum>(item.type!),
                    IsDefault = item.isDefault ?? false
                };

                categoryMap[item.seedId!] = category.Id;

                categories.Add(category);
            }

            foreach (var item in data.Where(x => x.dbDestination == "goal"))
            {
                var existingGoal = await _context.Goals
                    .FirstOrDefaultAsync(g =>
                        g.Title == item.title,
                        cancellationToken);

                if (existingGoal != null)
                {
                    continue;
                }

                goals.Add(new Goal
                {
                    Title = item.title!,
                    CurrentAmount = item.currentAmount ?? 0,
                    TargetAmount = item.targetAmount ?? 0,
                    Deadline = item.deadline,
                    IsCompleted = item.isCompleted ?? false
                });
            }

            List<Transaction> transactions = new();

            foreach (var item in data.Where(x => x.dbDestination == "transaction"))
            {
                var accountId = accountMap[item.accountSeedId!];
                var categoryId = categoryMap[item.categorySeedId!];

                var exists = await _context.Transactions
                    .AnyAsync(t =>
                        t.AccountId == accountId &&
                        t.CategoryId == categoryId &&
                        t.Amount == (item.amount ?? 0) &&
                        t.Date == (item.date ?? DateTime.UtcNow) &&
                        t.Type == Enum.Parse<TransactionTypeEnum>(item.type!),
                        cancellationToken);

                if (exists)
                {
                    continue;
                }

                transactions.Add(new Transaction
                {
                    AccountId = accountId,
                    CategoryId = categoryId,
                    Amount = item.amount ?? 0,
                    Description = item.description,
                    Date = item.date ?? DateTime.UtcNow,
                    Type = Enum.Parse<TransactionTypeEnum>(item.type!)
                });
            }
            
            await _uow.Accounts.AddRangeAsync(accounts);
            await _uow.Categories.AddRangeAsync(categories);
            await _uow.Goals.AddRangeAsync(goals);
            await _uow.Transactions.AddRangeAsync(transactions);

            await _uow.SaveChangesAsync();
            await _uow.CommitTransactionAsync();
            _logger.LogInformation("Seed executado com sucesso.");
            return true;
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            _logger.LogError(ex, "Não foi possível realizar o processo seed");
            throw new Exception("Não foi possível realizar o processo seed", ex);
        }
    }
}