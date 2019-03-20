using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Queries;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
{
    public class ValidateQueryBehavior<TQuery, TResponse>
        : PipelineBehaviorBase<TQuery, TResponse>
        where TQuery : PrincipalQueryBase<TResponse>
    {
        private readonly IValidator<TQuery> _validator;

        public ValidateQueryBehavior(ILoggerFactory loggerFactory, IValidator<TQuery> validator) : base(loggerFactory)
        {
            _validator = validator;
        }

        protected override async Task<TResponse> Process(TQuery request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // validate before processing
            await _validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken).ConfigureAwait(false);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }
    }
}