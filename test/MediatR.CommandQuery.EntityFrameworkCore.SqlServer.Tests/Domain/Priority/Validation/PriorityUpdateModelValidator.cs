using FluentValidation;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority.Validation;

public partial class PriorityUpdateModelValidator
    : AbstractValidator<PriorityUpdateModel>
{
    public PriorityUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(100);
        RuleFor(p => p.Description).MaximumLength(255);
        #endregion
    }

}
