using System;
using FluentValidation;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Tenant.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Tenant.Validation
{
    public partial class TenantCreateModelValidator
        : AbstractValidator<TenantCreateModel>
    {
        public TenantCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(255);
            #endregion
        }

    }
}
