using FluentValidation;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation
{
    public class TaskCreateModelValidator
        : AbstractValidator<TaskCreateModel>
    {
        public TaskCreateModelValidator()
        {
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Title).MaximumLength(255);
        }
    }
}
