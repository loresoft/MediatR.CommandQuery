using Cosmos.Abstracts;

using Injectio.Attributes;

using MediatR.CommandQuery.Cosmos.Tests.Domain.Handlers;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;
using MediatR.CommandQuery.Notifications;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class TaskServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<ICosmosRepository<Data.Entities.Task>, Data.Entities.Task, TaskReadModel>();
        services.AddEntityCommands<ICosmosRepository<Data.Entities.Task>, Data.Entities.Task, TaskReadModel, TaskCreateModel, TaskUpdateModel>();

        services.AddTransient<INotificationHandler<EntityChangeNotification<TaskReadModel>>, TaskChangedNotificationHandler>();
    }
}
