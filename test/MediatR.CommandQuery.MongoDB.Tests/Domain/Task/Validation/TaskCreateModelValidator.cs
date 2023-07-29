using FluentValidation;

using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Validation;

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
