
using FinCashly.Domain.Repositories;
using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinCashly.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    private readonly ApplicationDbContext _context;

    ITransactionsRepository? _transactionRepository;
    IAccountRepository? _accountRepository;
    ICategoryRepository? _categoryRepository;
    IGoalRepository? _goalRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ITransactionsRepository TransactionsRepository => _transactionRepository ??= new TransactionRepository(_context);
    public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_context);
    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
    public IGoalRepository GoalRepository => _goalRepository ??= new GoalRepository(_context);

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No active transaction.");
        }

        await _context.SaveChangesAsync();
        await _transaction!.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}