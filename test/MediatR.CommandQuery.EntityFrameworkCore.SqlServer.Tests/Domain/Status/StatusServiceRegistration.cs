using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Status.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Status;

public class StatusServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<TrackerContext, Data.Entities.Status, Guid, StatusReadModel>();
        services.AddEntityCommands<TrackerContext, Data.Entities.Status, Guid, StatusReadModel, StatusCreateModel, StatusUpdateModel>();
    }
}
