using Injectio.Attributes;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain
{
    public class RoleServiceRegistration
    {
        [RegisterServices]
        public void Register(IServiceCollection services)
        {
            services.AddEntityQueries<IMongoEntityRepository<Data.Entities.Role>, Data.Entities.Role, string, RoleReadModel>();
            services.AddEntityCommands<IMongoEntityRepository<Data.Entities.Role>, Data.Entities.Role, string, RoleReadModel, RoleCreateModel, RoleUpdateModel>();
        }
    }
}
