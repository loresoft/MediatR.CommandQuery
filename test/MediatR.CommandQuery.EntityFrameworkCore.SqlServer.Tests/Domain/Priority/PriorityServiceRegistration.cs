using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority;

public class PriorityServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<TrackerContext, Data.Entities.Priority, Guid, PriorityReadModel>();
        services.AddEntityCommands<TrackerContext, Data.Entities.Priority, Guid, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>();
    }
}
