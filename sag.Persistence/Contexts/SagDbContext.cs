using Microsoft.EntityFrameworkCore;
using sag.Domain.Entities;
using sag.Infrastructure.Services;

namespace sag.Persistence.Contexts;

public class SagDbContext : DbContext
{
    private readonly IAuthService _auth;

    public SagDbContext(DbContextOptions<SagDbContext> options, IAuthService auth) : base(options)
    {
        _auth = auth;
    }

    public virtual DbSet<Institution> Institutions { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserAccount> UserAccounts { get; set; }
    public virtual DbSet<UserAccountDetail> UserAccountDetails { get; set; }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.DetectChanges();
        var dateTime = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries().Where(e => e.State is EntityState.Added))
        {
            entry.Property("CreatedAt").CurrentValue = dateTime;
            entry.Property("CreatedBy").CurrentValue = _auth.User.Id;
            
            entry.Property("UpdatedAt").CurrentValue = dateTime;
            entry.Property("UpdatedBy").CurrentValue = _auth.User.Id;
        }

        foreach (var entry in ChangeTracker.Entries().Where(e => e.State is EntityState.Modified))
        {
            entry.Property("UpdatedAt").CurrentValue = dateTime;
            entry.Property("UpdatedBy").CurrentValue = _auth.User.Id;
        }


        return base.SaveChangesAsync(cancellationToken);
    }
}