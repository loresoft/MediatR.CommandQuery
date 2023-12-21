using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain;

public class TenantServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<IMongoEntityRepository<Data.Entities.Tenant>, Data.Entities.Tenant, string, TenantReadModel>();
        services.AddEntityCommands<IMongoEntityRepository<Data.Entities.Tenant>, Data.Entities.Tenant, string, TenantReadModel, TenantCreateModel, TenantUpdateModel>();
    }
}
