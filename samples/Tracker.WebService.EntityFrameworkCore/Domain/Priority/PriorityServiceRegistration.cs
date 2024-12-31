using System;

using Injectio.Attributes;

using MediatR.CommandQuery.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;

using Tracker.WebService.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.WebService.Domain;

public class PriorityServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<Data.TrackerServiceContext, Data.Entities.Priority, Guid, PriorityReadModel>();

        services.AddEntityCommands<Data.TrackerServiceContext, Data.Entities.Priority, Guid, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>();

    }

}
