using System.Collections.Generic;

using KickStart.DependencyInjection;

using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.MongoDB.Tests.Domain;

public class UserLoginServiceRegistration : IDependencyInjectionRegistration
{
    public void Register(IServiceCollection services, IDictionary<string, object> data)
    {
        services.AddEntityQueries<IMongoEntityRepository<Data.Entities.UserLogin>, Data.Entities.UserLogin, string, UserLoginReadModel>();
    }
}
