using System;
using System.Collections.Generic;

using KickStart.DependencyInjection;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Role.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Role;

public class RoleServiceRegistration : IDependencyInjectionRegistration
{
    public void Register(IServiceCollection services, IDictionary<string, object> data)
    {
        services.AddEntityQueries<TrackerContext, Data.Entities.Role, Guid, RoleReadModel>();
        services.AddEntityCommands<TrackerContext, Data.Entities.Role, Guid, RoleReadModel, RoleCreateModel, RoleUpdateModel>();
    }
}
