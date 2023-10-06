using FluentValidation;
using sag.Application.Features.UserAccounts.Queries;
using sag.Infrastructure.Services;
using sag.Persistence.Contexts;

namespace sag.Application.Features.UserAccounts.Validators;

public class GetUserAccountsValidator : AbstractValidator<GetUserAccountsQuery>
{
    public GetUserAccountsValidator(SagDbContext dbContext, IAuthService auth)
    {
        RuleFor(r => r.UserId)
            .NotNull().NotEmpty().WithMessage("Missing parameter: UserId");

        RuleFor(r => r.UserId)
            .Must(id => auth.User.Id == id)
            .WithMessage("Invalid parameter: UserId");

        // TODO: Authenticated User should have permissions

        RuleFor(r => r.UserId)
            .Must(id => dbContext.Users.Any(u => u.Id == id))
            .WithMessage("Invalid parameter: UserId");
    }
}