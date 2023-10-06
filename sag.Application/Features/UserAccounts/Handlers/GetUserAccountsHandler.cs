using MediatR;
using Microsoft.EntityFrameworkCore;
using sag.Application.Common.Structs;
using sag.Application.Features.UserAccounts.Queries;
using sag.Domain.Entities;
using sag.Infrastructure.Services;
using sag.Persistence.Contexts;

namespace sag.Application.Features.UserAccounts.Handlers;

public class GetUserAccountsHandler : IRequestHandler<GetUserAccountsQuery, Response<IEnumerable<UserAccount>>>
{
    private readonly SagDbContext _dbContext;
    private readonly IAuthService _auth;

    public GetUserAccountsHandler(SagDbContext dbContext, IAuthService auth)
    {
        _dbContext = dbContext;
        _auth = auth;
    }

    public async Task<Response<IEnumerable<UserAccount>>> Handle(GetUserAccountsQuery request,
        CancellationToken cancellationToken)
    {
        // TODO: Add more filters
        var accounts = await _dbContext.UserAccounts
            .AsNoTracking()
            .Where(a => a.UserId == _auth.User.Id)
            .ToListAsync(cancellationToken);

        return Response<IEnumerable<UserAccount>>.Success(accounts);
    }
}