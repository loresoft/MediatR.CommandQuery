using FluentValidation;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation;

public class TaskUpdateModelValidator
    : AbstractValidator<TaskUpdateModel>
{
    public TaskUpdateModelValidator()
    {
        RuleFor(p => p.Title).NotEmpty();
        RuleFor(p => p.Title).MaximumLength(255);
    }
}
