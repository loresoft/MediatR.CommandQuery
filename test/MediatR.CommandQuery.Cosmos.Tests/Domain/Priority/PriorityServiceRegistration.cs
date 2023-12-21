using System.Collections.Generic;

using Cosmos.Abstracts;

using Injectio.Attributes;

using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class PriorityServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<ICosmosRepository<Data.Entities.Priority>, Data.Entities.Priority, PriorityReadModel>();
        services.AddEntityCommands<ICosmosRepository<Data.Entities.Priority>, Data.Entities.Priority, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>();
    }
}
