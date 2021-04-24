using FluentValidation;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Validation
{
    public partial class TaskUpdateModelValidator
        : AbstractValidator<TaskUpdateModel>
    {
        public TaskUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Title).MaximumLength(255);
            #endregion
        }

    }
}
