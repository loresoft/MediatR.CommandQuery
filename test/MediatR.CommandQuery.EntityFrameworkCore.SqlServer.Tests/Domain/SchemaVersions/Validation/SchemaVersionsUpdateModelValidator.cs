using System;
using FluentValidation;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Validation
{
    public partial class SchemaVersionsUpdateModelValidator
        : AbstractValidator<SchemaVersionsUpdateModel>
    {
        public SchemaVersionsUpdateModelValidator()
        {
            #region Generated Constructor
        RuleFor(p => p.ScriptName).NotEmpty();
        RuleFor(p => p.ScriptName).MaximumLength(255);
        #endregion
        }

    }
}
