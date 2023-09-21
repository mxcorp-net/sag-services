using MediatR;
using Microsoft.EntityFrameworkCore;
using sag.Application.Features.Users.Queries;
using sag.Domain.Entities;
using sag.Persistence.Contexts;

namespace sag.Application.Features.Users.Handlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly SagDbContext _dbContext;

    public GetUserByIdHandler(SagDbContext dbContext) => _dbContext = dbContext;

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        return user ?? new User();
    }
}