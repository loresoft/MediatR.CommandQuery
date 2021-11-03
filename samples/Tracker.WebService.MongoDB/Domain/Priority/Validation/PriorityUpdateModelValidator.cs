using FluentValidation;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation
{
    public class PriorityUpdateModelValidator
        : AbstractValidator<PriorityUpdateModel>
    {
        public PriorityUpdateModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(255);
        }
    }
}
