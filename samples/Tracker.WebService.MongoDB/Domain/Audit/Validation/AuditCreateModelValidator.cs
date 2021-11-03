using FluentValidation;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation
{
    public class AuditCreateModelValidator
        : AbstractValidator<AuditCreateModel>
    {
        public AuditCreateModelValidator()
        {
            RuleFor(p => p.Content).NotEmpty();
            RuleFor(p => p.Username).NotEmpty();
            RuleFor(p => p.Username).MaximumLength(50);
        }
    }
}
