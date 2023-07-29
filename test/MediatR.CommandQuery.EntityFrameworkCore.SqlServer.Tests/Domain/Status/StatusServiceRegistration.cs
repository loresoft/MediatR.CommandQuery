using System;
using System.Collections.Generic;

using KickStart.DependencyInjection;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Status.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Status;

public class StatusServiceRegistration : IDependencyInjectionRegistration
{
    public void Register(IServiceCollection services, IDictionary<string, object> data)
    {
        services.AddEntityQueries<TrackerContext, Data.Entities.Status, Guid, StatusReadModel>();
        services.AddEntityCommands<TrackerContext, Data.Entities.Status, Guid, StatusReadModel, StatusCreateModel, StatusUpdateModel>();
    }
}
