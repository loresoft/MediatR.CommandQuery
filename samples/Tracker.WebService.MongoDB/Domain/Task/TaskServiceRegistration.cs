using Injectio.Attributes;

using MediatR;
using MediatR.CommandQuery.MongoDB;
using MediatR.CommandQuery.Notifications;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Domain.Handlers;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain
{
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
}
