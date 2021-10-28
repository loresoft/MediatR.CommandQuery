using System.Collections.Generic;
using MongoDB.Abstracts;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain
{
    public class AuditServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<IMongoEntityRepository<Audit>, Audit, string, AuditReadModel>();
            services.AddEntityCommands<IMongoEntityRepository<Audit>, Audit, string, AuditReadModel, AuditCreateModel, AuditUpdateModel>();
        }
    }
}
