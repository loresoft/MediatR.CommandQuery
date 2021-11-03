using FluentValidation;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation
{
    public class RoleUpdateModelValidator
        : AbstractValidator<RoleUpdateModel>
    {
        public RoleUpdateModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(256);
        }
    }
}
