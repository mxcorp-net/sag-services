using FluentValidation;
using sag.Application.Features.Transactions.Commands;
using sag.Infrastructure.Services;
using sag.Persistence.Contexts;

namespace sag.Application.Features.Transactions.Validators;

public class AddTransactionValidator : AbstractValidator<AddTransactionCommand>
{
    public AddTransactionValidator(SagDbContext sagDbContext, IAuthService auth)
    {
        // TODO: Implement Rules for AddTransactionValidator
    }
}