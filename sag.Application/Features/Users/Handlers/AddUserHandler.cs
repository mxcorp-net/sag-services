using System.Diagnostics;
using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.Users.Commands;
using sag.Domain.Entities;
using sag.Persistence.Contexts;

namespace sag.Application.Features.Users.Handlers;

public class AddUserHandler : IRequestHandler<AddUserCommand, Response<User>>
{
    private readonly SagDbContext _dbContext;

    public AddUserHandler(SagDbContext dbContext) => _dbContext = dbContext;

    public async Task<Response<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.User.Name,
            Email = request.User.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.User.Password),
            Status = request.User.Status
        };

        var newUser = await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Response<User>.Success(newUser.Entity);
    }
}