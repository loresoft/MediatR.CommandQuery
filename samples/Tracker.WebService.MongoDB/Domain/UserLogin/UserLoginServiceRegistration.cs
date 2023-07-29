using Injectio.Attributes;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain;

public class UserLoginServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<IMongoEntityRepository<Data.Entities.UserLogin>, Data.Entities.UserLogin, string, UserLoginReadModel>();
    }
}
