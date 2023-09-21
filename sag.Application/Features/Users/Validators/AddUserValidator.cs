using System.Net.Mail;
using FluentValidation;
using sag.Application.Features.Users.Commands;

namespace sag.Application.Features.Users.Validators;

public class AddUserValidator : AbstractValidator<AddUserCommand>
{
    public AddUserValidator()
    {
        RuleFor(r => r.User.Name)
            .NotNull().NotEmpty().WithMessage("Missing parameter: Name");

        RuleFor(r => r.User.Email)
            .NotNull().NotEmpty().WithMessage("Missing parameter: Email");

        RuleFor(r => r.User.Email)
            .Must(email => MailAddress.TryCreate(email, out _))
            .WithMessage("Invalid parameter: Email");

        RuleFor(r => r.User.Password)
            .NotNull().NotEmpty().WithMessage("Missing parameter: password");
    }
}