using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.CommandQuery.Commands;
using FluentValidation;

namespace EntityFrameworkCore.CommandQuery.Validation
{
    public class EntityCreateCommandValidator<TCreateModel, TReadModel> : AbstractValidator<EntityCreateCommand<TCreateModel, TReadModel>>
    {
        public EntityCreateCommandValidator()
        {
            RuleFor(p => p.Model).NotEmpty();
        }
    }
}
