using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR.CommandQuery.Commands;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors
{
    public class ValidateEntityModelCommandBehavior<TEntityModel, TResponse>
        : PipelineBehaviorBase<EntityModelCommand<TEntityModel, TResponse>, TResponse>
        where TEntityModel : class
    {
        private readonly IValidator<TEntityModel> _validator;

        public ValidateEntityModelCommandBehavior(ILoggerFactory loggerFactory, IValidator<TEntityModel> validator) : base(loggerFactory)
        {
            _validator = validator;
        }

        protected override async Task<TResponse> Process(EntityModelCommand<TEntityModel, TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // validate before processing
            await _validator.ValidateAndThrowAsync(request.Model, cancellationToken: cancellationToken).ConfigureAwait(false);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }
    }
}