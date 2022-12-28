using Injectio.Attributes;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain
{
    public class PriorityServiceRegistration
    {
        [RegisterServices]
        public void Register(IServiceCollection services)
        {
            services.AddEntityQueries<IMongoEntityRepository<Data.Entities.Priority>, Data.Entities.Priority, string, PriorityReadModel>();
            services.AddEntityCommands<IMongoEntityRepository<Data.Entities.Priority>, Data.Entities.Priority, string, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>();
        }
    }
}
