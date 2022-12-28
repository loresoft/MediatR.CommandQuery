using Injectio.Attributes;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain
{
    public class AuditServiceRegistration
    {
        [RegisterServices]
        public void Register(IServiceCollection services)
        {
            services.AddEntityQueries<IMongoEntityRepository<Audit>, Audit, string, AuditReadModel>();
            services.AddEntityCommands<IMongoEntityRepository<Audit>, Audit, string, AuditReadModel, AuditCreateModel, AuditUpdateModel>();
        }
    }
}
