using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit;

public class AuditServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<TrackerContext, Data.Entities.Audit, Guid, AuditReadModel>();
        services.AddEntityCommands<TrackerContext, Data.Entities.Audit, Guid, AuditReadModel, AuditCreateModel, AuditUpdateModel>();
    }
}
