using Microsoft.EntityFrameworkCore;
using sag.Domain.Entities;
using sag.Persistence.Contexts;

namespace sag.UnitTesting.Commons;

public class InMemorySagDbContext
{
    public InMemorySagDbContext()
    {
    }

    public SagDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<SagDbContext>()
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new SagDbContext(options);

        #region Seeders

        context.Add(new User { Name = "TestUser", Email = "test@email.com", Password = BCrypt.Net.BCrypt.HashPassword("123456789") });

        #endregion

        context.SaveChanges();
        return context;
    }
}