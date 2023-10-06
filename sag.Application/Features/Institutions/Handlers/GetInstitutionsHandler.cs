using MediatR;
using Microsoft.EntityFrameworkCore;
using sag.Application.Common.Structs;
using sag.Application.Features.Institutions.Queries;
using sag.Domain.Entities;
using sag.Persistence.Contexts;

namespace sag.Application.Features.Institutions.Handlers;

public class GetInstitutionsHandler : IRequestHandler<GetInstitutionsQuery, Response<IEnumerable<Institution>>>
{
    private readonly SagDbContext _dbContext;

    public GetInstitutionsHandler(SagDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<IEnumerable<Institution>>> Handle(GetInstitutionsQuery request,
        CancellationToken cancellationToken)
    { 
        var institutions = await _dbContext.Institutions
            .Where(i => i.Type == request.Filters.Type)
            .ToListAsync(cancellationToken);
        
        //TODO: Implement all the filters 

        return Response<IEnumerable<Institution>>.Success(institutions);
    }
}