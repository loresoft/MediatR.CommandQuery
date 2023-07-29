using FluentValidation;

using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Validation;

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
