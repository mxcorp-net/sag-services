using FluentValidation;
using sag.Application.Features.Institutions.Commands;

namespace sag.Application.Features.Institutions.Validators;

public class AddInstitutionValidator : AbstractValidator<AddInstitutionCommand>
{
    public AddInstitutionValidator()
    {
        RuleFor(r => r.Institution.Name)
            .NotNull().NotEmpty().WithMessage("Missing parameter: Name");
        
        RuleFor(r => r.Institution.Type)
            .NotNull().NotEmpty().WithMessage("Missing parameter: Type");
    }
}