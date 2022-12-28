using Injectio.Attributes;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain
{
    public class UserServiceRegistration
    {
        [RegisterServices]
        public void Register(IServiceCollection services)
        {
            services.AddEntityQueries<IMongoEntityRepository<Data.Entities.User>, Data.Entities.User, string, UserReadModel>();
            services.AddEntityCommands<IMongoEntityRepository<Data.Entities.User>, Data.Entities.User, string, UserReadModel, UserCreateModel, UserUpdateModel>();
        }
    }
}
