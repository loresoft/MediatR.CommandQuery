using System.Collections.Generic;

using KickStart.DependencyInjection;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain
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
