using Cosmos.Abstracts;

using MediatR.CommandQuery.Cosmos.Tests.Data.Entities;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class AuditServiceRegistration
{

    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<ICosmosRepository<Audit>, Audit, AuditReadModel>();
        services.AddEntityCommands<ICosmosRepository<Audit>, Audit, AuditReadModel, AuditCreateModel, AuditUpdateModel>();
    }
}
