using System;
using EntityFrameworkCore.CommandQuery.Commands;
using FluentValidation;

namespace EntityFrameworkCore.CommandQuery.Validation
{
    public class EntityPatchCommandValidator<TKey, TReadModel> : AbstractValidator<EntityPatchCommand<TKey, TReadModel>>
    {
        public EntityPatchCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Patch).NotEmpty();
        }
    }
}