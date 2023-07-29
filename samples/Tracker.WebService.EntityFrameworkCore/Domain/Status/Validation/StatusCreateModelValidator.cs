using System;

using FluentValidation;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation;

public partial class StatusCreateModelValidator
    : AbstractValidator<StatusCreateModel>
{
    public StatusCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(100);
        RuleFor(p => p.Description).MaximumLength(255);
        #endregion
    }

}
