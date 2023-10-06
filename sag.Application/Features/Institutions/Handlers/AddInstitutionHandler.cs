using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.Institutions.Commands;
using sag.Domain.Entities;
using sag.Persistence.Contexts;

namespace sag.Application.Features.Institutions.Handlers;

public class AddInstitutionHandler : IRequestHandler<AddInstitutionCommand, Response<Institution>>
{
    private readonly SagDbContext _dbContext;

    public AddInstitutionHandler(SagDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<Institution>> Handle(AddInstitutionCommand request, CancellationToken cancellationToken)
    {
        var ins = new Institution
        {
            Name = request.Institution.Name,
            Type = request.Institution.Type
        };

        var newInst = await _dbContext.Institutions.AddAsync(ins, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Response<Institution>.Success(newInst.Entity);
    }
}