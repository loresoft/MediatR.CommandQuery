using Cosmos.Abstracts;

using Injectio.Attributes;

using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class RoleServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<ICosmosRepository<Data.Entities.Role>, Data.Entities.Role, RoleReadModel>();
        services.AddEntityCommands<ICosmosRepository<Data.Entities.Role>, Data.Entities.Role, RoleReadModel, RoleCreateModel, RoleUpdateModel>();
    }
}
