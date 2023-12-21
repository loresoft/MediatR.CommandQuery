using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain;

public class AuditServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<IMongoEntityRepository<Audit>, Audit, string, AuditReadModel>();
        services.AddEntityCommands<IMongoEntityRepository<Audit>, Audit, string, AuditReadModel, AuditCreateModel, AuditUpdateModel>();
    }
}
