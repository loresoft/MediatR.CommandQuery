using System.Collections.Generic;
using Cosmos.Abstracts;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Handlers;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;
using MediatR.CommandQuery.Notifications;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain
{
    public class TaskServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<ICosmosRepository<Data.Entities.Task>, Data.Entities.Task, TaskReadModel>();
            services.AddEntityCommands<ICosmosRepository<Data.Entities.Task>, Data.Entities.Task, TaskReadModel, TaskCreateModel, TaskUpdateModel>();

            services.AddTransient<INotificationHandler<EntityChangeNotification<TaskReadModel>>, TaskChangedNotificationHandler>();
        }
    }
}
