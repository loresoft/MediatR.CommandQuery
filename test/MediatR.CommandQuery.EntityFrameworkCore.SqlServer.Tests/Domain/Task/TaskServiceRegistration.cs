using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Task.Handlers;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Task.Models;
using MediatR.CommandQuery.Notifications;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Task;

public class TaskServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<TrackerContext, Data.Entities.Task, Guid, TaskReadModel>();
        services.AddEntityCommands<TrackerContext, Data.Entities.Task, Guid, TaskReadModel, TaskCreateModel, TaskUpdateModel>();

        services.AddTransient<INotificationHandler<EntityChangeNotification<TaskReadModel>>, TaskChangedNotificationHandler>();
    }
}
