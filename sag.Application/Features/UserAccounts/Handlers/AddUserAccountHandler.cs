using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.UserAccounts.Commands;
using sag.Domain.Common.Enums;
using sag.Domain.Entities;
using sag.Persistence.Contexts;

namespace sag.Application.Features.UserAccounts.Handlers;

public class AddUserAccountHandler : IRequestHandler<AddUserAccountCommand, Response<UserAccount>>
{
    private readonly SagDbContext _dbContext;

    public AddUserAccountHandler(SagDbContext dbContext) => _dbContext = dbContext;

    public async Task<Response<UserAccount>> Handle(AddUserAccountCommand request, CancellationToken cancellationToken)
    {
        var userAccount = new UserAccount
        {
            Name = request.UserAccount.Name,
            UserId = request.UserId,
            InstitutionId = request.UserAccount.InstitutionId,
            AccountType = request.UserAccount.AccountType,
            Status = EntityStatus.Enable
        };

        var newUserAccount = _dbContext.UserAccounts.Add(userAccount);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Response<UserAccount>.Success(newUserAccount.Entity);
    }
}