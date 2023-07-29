using System.Collections.Generic;

using Cosmos.Abstracts;

using KickStart.DependencyInjection;

using MediatR.CommandQuery.Cosmos.Tests.Data.Entities;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class AuditServiceRegistration : IDependencyInjectionRegistration
{
    public void Register(IServiceCollection services, IDictionary<string, object> data)
    {
        services.AddEntityQueries<ICosmosRepository<Audit>, Audit, AuditReadModel>();
        services.AddEntityCommands<ICosmosRepository<Audit>, Audit, AuditReadModel, AuditCreateModel, AuditUpdateModel>();
    }
}
