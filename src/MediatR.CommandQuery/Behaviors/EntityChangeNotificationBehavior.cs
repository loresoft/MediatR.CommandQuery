using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Notifications;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

/// <summary>
/// A behavior for sending change notifications
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TEntityModel">The type of the entity model.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class EntityChangeNotificationBehavior<TKey, TEntityModel, TResponse>
    : PipelineBehaviorBase<PrincipalCommandBase<TResponse>, TResponse>
    where TEntityModel : class
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityChangeNotificationBehavior{TKey, TEntityModel, TResponse}"/> class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="mediator">The mediator.</param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public EntityChangeNotificationBehavior(ILoggerFactory loggerFactory, IMediator mediator) : base(loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(mediator);

        _mediator = mediator;
    }

    /// <inheritdoc />
    protected override async Task<TResponse> Process(
        PrincipalCommandBase<TResponse> request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        var response = await next().ConfigureAwait(false);

        if (response is not null)
            await SendNotification(request, response, cancellationToken).ConfigureAwait(false);

        return response;
    }

    private async Task SendNotification(
        PrincipalCommandBase<TResponse> request,
        TResponse response,
        CancellationToken cancellationToken)
    {
        var operation = request switch
        {
            EntityCreateCommand<TEntityModel, TResponse> _ => EntityChangeOperation.Created,
            EntityDeleteCommand<TKey, TResponse> _ => EntityChangeOperation.Deleted,
            _ => EntityChangeOperation.Updated
        };

        var notification = new EntityChangeNotification<TResponse>(response, operation);
        await _mediator.Publish(notification, cancellationToken).ConfigureAwait(false);
    }
}
