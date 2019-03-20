using System;
using EntityFrameworkCore.CommandQuery.Queries;
using FluentValidation;

namespace EntityFrameworkCore.CommandQuery.Validation
{
    public class EntityListQueryValidator<TReadModel> : AbstractValidator<EntityListQuery<TReadModel>>
    {
        public EntityListQueryValidator()
        {
            RuleFor(p => p.Query).NotEmpty();
        }
    }
}