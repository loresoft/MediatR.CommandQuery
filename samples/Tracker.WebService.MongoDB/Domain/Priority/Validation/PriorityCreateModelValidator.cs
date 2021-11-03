using FluentValidation;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation
{
    public class PriorityCreateModelValidator
        : AbstractValidator<PriorityCreateModel>
    {
        public PriorityCreateModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(255);
        }
    }
}
