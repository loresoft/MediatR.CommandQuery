using System;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Role.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Role;

public class RoleServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<TrackerContext, Data.Entities.Role, Guid, RoleReadModel>();
        services.AddEntityCommands<TrackerContext, Data.Entities.Role, Guid, RoleReadModel, RoleCreateModel, RoleUpdateModel>();
    }
}
