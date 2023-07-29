using System;
using System.Collections.Generic;

using Cosmos.Abstracts;

using KickStart.DependencyInjection;

using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class StatusServiceRegistration : IDependencyInjectionRegistration
{
    public void Register(IServiceCollection services, IDictionary<string, object> data)
    {
        services.AddEntityQueries<ICosmosRepository<Data.Entities.Status>, Data.Entities.Status, StatusReadModel>();
        services.AddEntityCommands<ICosmosRepository<Data.Entities.Status>, Data.Entities.Status, StatusReadModel, StatusCreateModel, StatusUpdateModel>();
    }
}
