using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain;

public class PriorityServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<IMongoEntityRepository<Data.Entities.Priority>, Data.Entities.Priority, string, PriorityReadModel>();
        services.AddEntityCommands<IMongoEntityRepository<Data.Entities.Priority>, Data.Entities.Priority, string, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>();
    }
}
