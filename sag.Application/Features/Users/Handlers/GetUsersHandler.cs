using MediatR;
using Microsoft.EntityFrameworkCore;
using sag.Application.Features.Users.Queries;
using sag.Domain.Common.Enums;
using sag.Domain.Entities;
using sag.Persistence.Contexts;

namespace sag.Application.Features.Users.Handlers;

public class GetUsersHandler: IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly SagDbContext _dbContext;

    public GetUsersHandler(SagDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.Where(u => u.Status == EntityStatus.Enable).ToListAsync(cancellationToken: cancellationToken);
    }
}