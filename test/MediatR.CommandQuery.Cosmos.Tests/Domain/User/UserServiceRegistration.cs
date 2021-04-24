using System.Collections.Generic;
using Cosmos.Abstracts;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain
{
    public class UserServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<ICosmosRepository<Data.Entities.User>, Data.Entities.User, UserReadModel>();
            services.AddEntityCommands<ICosmosRepository<Data.Entities.User>, Data.Entities.User, UserReadModel, UserCreateModel, UserUpdateModel>();
        }
    }
}
