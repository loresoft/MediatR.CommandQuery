using System.Collections.Generic;

using KickStart.DependencyInjection;

using MediatR;
using MediatR.CommandQuery.MongoDB;
using MediatR.CommandQuery.Notifications;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Domain.Handlers;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain
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
