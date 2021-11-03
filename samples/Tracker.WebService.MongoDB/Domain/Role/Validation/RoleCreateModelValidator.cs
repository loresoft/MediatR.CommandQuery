using FluentValidation;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation
{
    public class RoleCreateModelValidator
        : AbstractValidator<RoleCreateModel>
    {
        public RoleCreateModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(256);
        }
    }
}
