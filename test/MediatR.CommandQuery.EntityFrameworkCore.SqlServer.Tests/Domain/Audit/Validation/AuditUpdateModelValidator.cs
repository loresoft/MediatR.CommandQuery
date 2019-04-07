using System;
using FluentValidation;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit.Validation
{
    public partial class AuditUpdateModelValidator
        : AbstractValidator<AuditUpdateModel>
    {
        public AuditUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Content).NotEmpty();
            RuleFor(p => p.Username).NotEmpty();
            RuleFor(p => p.Username).MaximumLength(50);
            #endregion
        }

    }
}
