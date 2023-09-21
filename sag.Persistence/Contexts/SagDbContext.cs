using Microsoft.EntityFrameworkCore;
using sag.Domain.Entities;

namespace sag.Persistence.Contexts;

public class SagDbContext : DbContext
{
    public SagDbContext(DbContextOptions<SagDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
    public virtual DbSet<Loan> Loans { get; set; }
    public virtual DbSet<InvestmentAccount> InvestmentAccounts { get; set; }
    public virtual DbSet<Institution>? Institutions { get; set; }
    public virtual DbSet<BankAccountDetail> BankAccountDetails { get; set; }
    public virtual DbSet<BankAccount> BankAccounts { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.DetectChanges();
        var dateTime = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries().Where(e => e.State is EntityState.Added))
        {
            entry.Property("CreatedAt").CurrentValue = dateTime;
            // TODO: Implement property CreatedBy
        }
        foreach (var entry in ChangeTracker.Entries().Where(e => e.State is EntityState.Modified))
        {
            entry.Property("UpdatedAt").CurrentValue = dateTime;
            // TODO: Implement property UpdatedBy
        }


        return base.SaveChangesAsync(cancellationToken);
    }
}