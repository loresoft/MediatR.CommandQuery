using FluentValidation;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Validation
{
    public partial class AuditCreateModelValidator
        : AbstractValidator<AuditCreateModel>
    {
        public AuditCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Content).NotEmpty();
            RuleFor(p => p.Username).NotEmpty();
            RuleFor(p => p.Username).MaximumLength(50);
            #endregion
        }

    }
}
