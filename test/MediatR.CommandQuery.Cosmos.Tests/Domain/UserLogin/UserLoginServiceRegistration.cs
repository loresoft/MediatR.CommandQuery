using Cosmos.Abstracts;

using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain;

public class UserLoginServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<ICosmosRepository<Data.Entities.UserLogin>, Data.Entities.UserLogin, UserLoginReadModel>();
    }
}
