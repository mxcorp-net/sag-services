using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.Auth.Queries;
using sag.Persistence.Contexts;

namespace sag.Application.Features.Auth.Handlers;

public class CheckHandler : IRequestHandler<CheckQuery, Response<bool>>
{
    private readonly SagDbContext _sagDbContext;

    public CheckHandler(SagDbContext sagDbContext)
    {
        _sagDbContext = sagDbContext;
    }
    public async Task<Response<bool>> Handle(CheckQuery request, CancellationToken cancellationToken)
    {
        return Response<bool>.Success(true);
    }
}