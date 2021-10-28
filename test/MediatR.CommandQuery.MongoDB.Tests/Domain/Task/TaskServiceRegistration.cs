using System.Collections.Generic;
using MongoDB.Abstracts;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Handlers;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;
using MediatR.CommandQuery.Notifications;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain
{
    public class TaskServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<IMongoEntityRepository<Data.Entities.Task>, Data.Entities.Task, string, TaskReadModel>();
            services.AddEntityCommands<IMongoEntityRepository<Data.Entities.Task>, Data.Entities.Task, string, TaskReadModel, TaskCreateModel, TaskUpdateModel>();

            services.AddTransient<INotificationHandler<EntityChangeNotification<TaskReadModel>>, TaskChangedNotificationHandler>();
        }
    }
}
