using System;
using EntityFrameworkCore.CommandQuery.Queries;
using FluentValidation;

namespace EntityFrameworkCore.CommandQuery.Validation
{
    public class EntityIdentifierQueryValidator<TKey, TReadModel> : AbstractValidator<EntityIdentifierQuery<TKey, TReadModel>>
    {
        public EntityIdentifierQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}