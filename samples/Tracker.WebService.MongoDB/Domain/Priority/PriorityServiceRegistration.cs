using System.Collections.Generic;

using KickStart.DependencyInjection;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain
{
    public class PriorityServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<IMongoEntityRepository<Data.Entities.Priority>, Data.Entities.Priority, string, PriorityReadModel>();
            services.AddEntityCommands<IMongoEntityRepository<Data.Entities.Priority>, Data.Entities.Priority, string, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>();
        }
    }
}
