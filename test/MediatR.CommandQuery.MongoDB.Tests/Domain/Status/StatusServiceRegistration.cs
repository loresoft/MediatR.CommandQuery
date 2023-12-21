using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain;

public class StatusServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<IMongoEntityRepository<Data.Entities.Status>, Data.Entities.Status, string, StatusReadModel>();
        services.AddEntityCommands<IMongoEntityRepository<Data.Entities.Status>, Data.Entities.Status, string, StatusReadModel, StatusCreateModel, StatusUpdateModel>();
    }
}
