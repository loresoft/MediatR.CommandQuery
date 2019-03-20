using System;
using EntityFrameworkCore.CommandQuery.Commands;
using FluentValidation;

namespace EntityFrameworkCore.CommandQuery.Validation
{
    public class EntityUpdateCommandValidator<TKey, TUpdateModel, TReadModel> : AbstractValidator<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>>
    {
        public EntityUpdateCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Model).NotEmpty();
        }
    }
}