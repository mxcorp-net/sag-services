using System.Net.Mail;
using FluentValidation;
using sag.Application.Features.Auth.Queries;

namespace sag.Application.Features.Auth.Validators;

public class LoginUserValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserValidator()
    {
        RuleFor(r => r.LoginData.Email)
            .NotNull().NotEmpty().WithMessage("Missing parameter: email");

        RuleFor(r => r.LoginData.Email)
            .Must(email => MailAddress.TryCreate(email, out _))
            .WithMessage("Invalid parameter: email");

        RuleFor(r => r.LoginData.Password)
            .NotNull().NotEmpty().WithMessage("Missing parameter: password");
    }
}