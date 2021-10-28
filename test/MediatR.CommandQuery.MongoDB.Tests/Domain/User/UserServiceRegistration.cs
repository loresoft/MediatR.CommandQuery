using System.Collections.Generic;
using MongoDB.Abstracts;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain
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
