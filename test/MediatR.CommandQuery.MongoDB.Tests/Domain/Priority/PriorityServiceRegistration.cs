using System.Collections.Generic;
using MongoDB.Abstracts;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain
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
