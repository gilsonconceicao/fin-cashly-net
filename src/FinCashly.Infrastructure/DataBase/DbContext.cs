
using System.Reflection;
using FinCashly.Application.Common.Interfaces;
using FinCashly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FinCashly.Infrastructure.DataBase;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentUserService _currentUser;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUser = currentUserService;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Goal> Goals { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new UtcDateInterceptor());
    }

    public override int SaveChanges()
    {
        ApplyEntityBaseBehavior();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyEntityBaseBehavior();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyEntityBaseBehavior()
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    if (_currentUser.UserId != null && !string.IsNullOrEmpty(_currentUser.UserId))
                    {
                        entry.Entity.CreatedById = _currentUser.UserId;
                    }
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
    }

    public class UtcDateInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            ConvertDates(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        private void ConvertDates(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                foreach (var prop in entry.Properties)
                {
                    if (prop.Metadata.ClrType == typeof(DateTime)
                        && prop.CurrentValue is DateTime dt
                        && dt.Kind != DateTimeKind.Utc)
                    {
                        prop.CurrentValue = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    }
                }
            }
        }
    }

}