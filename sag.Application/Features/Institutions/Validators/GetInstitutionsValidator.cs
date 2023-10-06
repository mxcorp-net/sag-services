using FluentValidation;
using sag.Application.Features.Institutions.Queries;

namespace sag.Application.Features.Institutions.Validators;

public class GetInstitutionsValidator : AbstractValidator<GetInstitutionsQuery>
{
    public GetInstitutionsValidator()
    {
        RuleFor(r => r.Filters.Type)
            .NotNull().WithMessage("Missing parameter: Type");
    }
    
}