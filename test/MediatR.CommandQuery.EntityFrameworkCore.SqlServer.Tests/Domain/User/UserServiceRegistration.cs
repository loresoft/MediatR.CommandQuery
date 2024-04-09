using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.User.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.User;

public class UserServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<TrackerContext, Data.Entities.User, Guid, UserReadModel>();
        services.AddEntityCommands<TrackerContext, Data.Entities.User, Guid, UserReadModel, UserCreateModel, UserUpdateModel>();
    }
}
