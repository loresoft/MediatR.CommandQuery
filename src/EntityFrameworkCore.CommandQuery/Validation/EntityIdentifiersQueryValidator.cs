using System;
using EntityFrameworkCore.CommandQuery.Queries;
using FluentValidation;

namespace EntityFrameworkCore.CommandQuery.Validation
{
    public class EntityIdentifiersQueryValidator<TKey, TReadModel> : AbstractValidator<EntityIdentifiersQuery<TKey, TReadModel>>
    {
        public EntityIdentifiersQueryValidator()
        {
            RuleFor(p => p.Ids).NotEmpty();
        }
    }
}