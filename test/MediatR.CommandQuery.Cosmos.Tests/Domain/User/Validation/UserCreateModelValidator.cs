using FluentValidation;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Validation
{
    public partial class UserCreateModelValidator
        : AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.EmailAddress).NotEmpty();
            RuleFor(p => p.EmailAddress).MaximumLength(256);
            RuleFor(p => p.DisplayName).NotEmpty();
            RuleFor(p => p.DisplayName).MaximumLength(256);
            #endregion
        }

    }
}
