using FluentValidation;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation;

public class UserCreateModelValidator
    : AbstractValidator<UserCreateModel>
{
    public UserCreateModelValidator()
    {
        RuleFor(p => p.EmailAddress).NotEmpty();
        RuleFor(p => p.EmailAddress).MaximumLength(256);
        RuleFor(p => p.DisplayName).NotEmpty();
        RuleFor(p => p.DisplayName).MaximumLength(256);
    }
}
