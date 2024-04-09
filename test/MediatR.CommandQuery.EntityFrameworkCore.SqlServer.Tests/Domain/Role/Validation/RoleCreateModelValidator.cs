using FluentValidation;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Role.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Role.Validation;

public partial class RoleCreateModelValidator
    : AbstractValidator<RoleCreateModel>
{
    public RoleCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(256);
        #endregion
    }

}
