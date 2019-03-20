using System;
using EntityFrameworkCore.CommandQuery.Commands;
using FluentValidation;

namespace EntityFrameworkCore.CommandQuery.Validation
{
    public class EntityDeleteCommandValidator<TKey, TReadModel> : AbstractValidator<EntityDeleteCommand<TKey, TReadModel>>
    {
        public EntityDeleteCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}