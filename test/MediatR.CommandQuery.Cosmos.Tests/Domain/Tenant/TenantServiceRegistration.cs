using Cosmos.Abstracts;

using Injectio.Attributes;

using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class TenantServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<ICosmosRepository<Data.Entities.Tenant>, Data.Entities.Tenant, TenantReadModel>();
        services.AddEntityCommands<ICosmosRepository<Data.Entities.Tenant>, Data.Entities.Tenant, TenantReadModel, TenantCreateModel, TenantUpdateModel>();
    }
}
