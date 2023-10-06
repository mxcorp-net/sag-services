using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using sag.Domain.Common.Enums;
using sag.Domain.Entities;
using sag.Infrastructure.Services;
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

        // TODO: add generic context
        var context = new SagDbContext(options, new AuthService(new HttpContextAccessor()));

        #region Seeders

        context.Add(new User
            { Name = "TestUser", Email = "test@email.com", Password = BCrypt.Net.BCrypt.HashPassword("123456789") });

        context.Add(new Institution { Name = "Banamex", Type = InstitutionType.Banking, Status = EntityStatus.Enable });
        context.Add(new Institution { Name = "BBVA", Type = InstitutionType.Banking, Status = EntityStatus.Enable });
        context.Add(new Institution { Name = "GBM", Type = InstitutionType.Investment, Status = EntityStatus.Enable });

        #endregion

        context.SaveChanges();
        return context;
    }
}