using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Notifications;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public class EntityChangeNotificationBehavior<TKey, TEntityModel, TResponse>
    : PipelineBehaviorBase<PrincipalCommandBase<TResponse>, TResponse>
    where TEntityModel : class
{
    private readonly IMediator _mediator;

    public EntityChangeNotificationBehavior(ILoggerFactory loggerFactory, IMediator mediator) : base(loggerFactory)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    protected override async Task<TResponse> Process(PrincipalCommandBase<TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (next is null)
            throw new ArgumentNullException(nameof(next));

        var response = await next().ConfigureAwait(false);

        await SendNotification(request, response, cancellationToken);

        return response;
    }

    private async Task SendNotification(PrincipalCommandBase<TResponse> request, TResponse response, CancellationToken cancellationToken)
    {
        var operation = request switch
        {
            EntityCreateCommand<TEntityModel, TResponse> _ => EntityChangeOperation.Created,
            EntityDeleteCommand<TKey, TResponse> _ => EntityChangeOperation.Deleted,
            _ => EntityChangeOperation.Updated
        };

        var notification = new EntityChangeNotification<TResponse>(response, operation);
        await _mediator.Publish(notification, cancellationToken);
    }
}
