using System;
using FluentValidation;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation
{
    public partial class TaskCreateModelValidator
        : AbstractValidator<TaskCreateModel>
    {
        public TaskCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Title).MaximumLength(255);
            #endregion
        }

    }
}
