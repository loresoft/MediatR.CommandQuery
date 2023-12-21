using Cosmos.Abstracts;

using Injectio.Attributes;

using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class UserServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<ICosmosRepository<Data.Entities.User>, Data.Entities.User, UserReadModel>();
        services.AddEntityCommands<ICosmosRepository<Data.Entities.User>, Data.Entities.User, UserReadModel, UserCreateModel, UserUpdateModel>();
    }
}
