using System.Threading;

using MediatR;
using MediatR.CommandQuery.Notifications;

using Microsoft.Extensions.Logging;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Handlers;

public class TaskChangedNotificationHandler : INotificationHandler<EntityChangeNotification<TaskReadModel>>
{
    private readonly ILogger<TaskChangedNotificationHandler> _logger;

    public TaskChangedNotificationHandler(ILogger<TaskChangedNotificationHandler> logger)
    {
        _logger = logger;
    }

    public System.Threading.Tasks.Task Handle(EntityChangeNotification<TaskReadModel> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Task Changed: {id} {operation}", notification.Model?.Id, notification.Operation);

        return System.Threading.Tasks.Task.CompletedTask;
    }
}
