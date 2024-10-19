using FluentValidation;

using MediatR.CommandQuery.Commands;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public class ValidateEntityModelCommandBehavior<TEntityModel, TResponse>
    : PipelineBehaviorBase<EntityModelCommand<TEntityModel, TResponse>, TResponse>
    where TEntityModel : class
{
    private readonly IValidator<TEntityModel> _validator;

    public ValidateEntityModelCommandBehavior(ILoggerFactory loggerFactory, IValidator<TEntityModel> validator) : base(loggerFactory)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    protected override async Task<TResponse> Process(
        EntityModelCommand<TEntityModel, TResponse> request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        // validate before processing
        await _validator.ValidateAndThrowAsync(request.Model, cancellationToken: cancellationToken).ConfigureAwait(false);

        // continue pipeline
        return await next().ConfigureAwait(false);
    }
}
