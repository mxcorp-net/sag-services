using FluentValidation;
using Microsoft.EntityFrameworkCore;
using sag.Application.Features.UserAccounts.Commands;
using sag.Infrastructure.Services;
using sag.Persistence.Contexts;

namespace sag.Application.Features.UserAccounts.Validators;

public class AddUserAccountValidator : AbstractValidator<AddUserAccountCommand>
{
    public AddUserAccountValidator(SagDbContext dbContext, IAuthService auth)
    {
        RuleFor(r => r.UserId)
            .NotNull().NotEmpty().WithMessage("Missing parameter: UserId");

        RuleFor(r => r.UserId)
            .Must(id => auth.User.Id == id)
            .WithMessage("Invalid parameter: UserId");

        RuleFor(r => r.UserAccount.Name)
            .NotNull().NotEmpty().WithMessage("Missing parameter: Name");
        
        RuleFor(r => r.UserAccount.AccountType)
            .NotNull().WithMessage("Missing parameter: AccountType");
        
        RuleFor(r => r.UserAccount.InstitutionId)
            .NotNull().NotEmpty().WithMessage("Missing parameter: InstitutionId");
        
        RuleFor(r => r.UserAccount.InstitutionId)
            .Must(intId => dbContext.Institutions.AsNoTracking().Any(i => i.Id == intId))
            .WithMessage("Invalid parameter: InstitutionId");
    }
}