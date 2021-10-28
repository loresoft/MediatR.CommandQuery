using FluentValidation;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Validation
{
    public partial class RoleUpdateModelValidator
        : AbstractValidator<RoleUpdateModel>
    {
        public RoleUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(256);
            #endregion
        }

    }
}
