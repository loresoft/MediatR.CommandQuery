using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
{
    public class ValidateCommandBehavior<TCommand, TResponse>
        : PipelineBehaviorBase<TCommand, TResponse>
        where TCommand : PrincipalCommandBase<TResponse>
    {
        private readonly IValidator<TCommand> _validator;

        public ValidateCommandBehavior(ILoggerFactory loggerFactory, IValidator<TCommand> validator) : base(loggerFactory)
        {
            _validator = validator;
        }

        protected override async Task<TResponse> Process(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // validate before processing
            await _validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken).ConfigureAwait(false);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }
    }
}