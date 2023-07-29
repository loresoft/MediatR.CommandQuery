using FluentValidation;

using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Validation;

public partial class PriorityCreateModelValidator
    : AbstractValidator<PriorityCreateModel>
{
    public PriorityCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(100);
        RuleFor(p => p.Description).MaximumLength(255);
        #endregion
    }

}
