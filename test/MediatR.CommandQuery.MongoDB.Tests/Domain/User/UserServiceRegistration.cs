using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain;

public class UserServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<IMongoEntityRepository<Data.Entities.User>, Data.Entities.User, string, UserReadModel>();
        services.AddEntityCommands<IMongoEntityRepository<Data.Entities.User>, Data.Entities.User, string, UserReadModel, UserCreateModel, UserUpdateModel>();
    }
}
