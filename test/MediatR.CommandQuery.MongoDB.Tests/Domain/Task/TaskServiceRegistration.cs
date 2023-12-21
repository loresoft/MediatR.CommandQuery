using MediatR.CommandQuery.MongoDB.Tests.Domain.Handlers;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;
using MediatR.CommandQuery.Notifications;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain;

public class TaskServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<IMongoEntityRepository<Data.Entities.Task>, Data.Entities.Task, string, TaskReadModel>();
        services.AddEntityCommands<IMongoEntityRepository<Data.Entities.Task>, Data.Entities.Task, string, TaskReadModel, TaskCreateModel, TaskUpdateModel>();

        services.AddTransient<INotificationHandler<EntityChangeNotification<TaskReadModel>>, TaskChangedNotificationHandler>();
    }
}
