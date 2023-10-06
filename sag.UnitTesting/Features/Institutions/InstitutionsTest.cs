using sag.Application.Features.Institutions.Handlers;
using sag.Application.Features.Institutions.Models;
using sag.Application.Features.Institutions.Queries;
using sag.Domain.Common.Enums;
using sag.Persistence.Contexts;
using sag.UnitTesting.Commons;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace sag.UnitTesting.Features.Institutions;

public class InstitutionsTest
{
    private readonly SagDbContext _dbContext = new InMemorySagDbContext().CreateContext();

    [Fact]
    public async Task GetInstitutions_Exceptions()
    {
        var handler = new GetInstitutionsHandler(_dbContext);
        var filters = new InstitutionFilters
        {
            Type = InstitutionType.Banking
        };

        var action = async () => await handler.Handle(new GetInstitutionsQuery(filters), CancellationToken.None);
        
        await action.Should().ThrowAsync<BadHttpRequestException>();
        
    }
}