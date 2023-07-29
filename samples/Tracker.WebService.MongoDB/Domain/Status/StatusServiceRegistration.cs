using Injectio.Attributes;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain;

public class StatusServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<IMongoEntityRepository<Data.Entities.Status>, Data.Entities.Status, string, StatusReadModel>();
        services.AddEntityCommands<IMongoEntityRepository<Data.Entities.Status>, Data.Entities.Status, string, StatusReadModel, StatusCreateModel, StatusUpdateModel>();
    }
}
