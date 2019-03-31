using System;
using FluentValidation;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Validation
{
    public partial class TaskExtendedCreateModelValidator
        : AbstractValidator<TaskExtendedCreateModel>
    {
        public TaskExtendedCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Browser).MaximumLength(256);
            RuleFor(p => p.OperatingSystem).MaximumLength(256);
            #endregion
        }

    }
}
