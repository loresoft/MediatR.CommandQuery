using System;

using FluentValidation;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.TaskExtended.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.TaskExtended.Validation;

public partial class TaskExtendedUpdateModelValidator
    : AbstractValidator<TaskExtendedUpdateModel>
{
    public TaskExtendedUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Browser).MaximumLength(256);
        RuleFor(p => p.OperatingSystem).MaximumLength(256);
        #endregion
    }

}
