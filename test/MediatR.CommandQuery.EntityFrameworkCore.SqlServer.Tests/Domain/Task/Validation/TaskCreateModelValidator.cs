using System;
using FluentValidation;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Task.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Task.Validation
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
