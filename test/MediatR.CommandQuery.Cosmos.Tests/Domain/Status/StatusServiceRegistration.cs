using Cosmos.Abstracts;

using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class StatusServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<ICosmosRepository<Data.Entities.Status>, Data.Entities.Status, StatusReadModel>();
        services.AddEntityCommands<ICosmosRepository<Data.Entities.Status>, Data.Entities.Status, StatusReadModel, StatusCreateModel, StatusUpdateModel>();
    }
}
