using System.Collections.Generic;

using KickStart.DependencyInjection;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain
{
    public class UserServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<IMongoEntityRepository<Data.Entities.User>, Data.Entities.User, string, UserReadModel>();
            services.AddEntityCommands<IMongoEntityRepository<Data.Entities.User>, Data.Entities.User, string, UserReadModel, UserCreateModel, UserUpdateModel>();
        }
    }
}
