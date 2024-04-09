using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Tenant.Models;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Tenant;

public class TenantServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddEntityQueries<TrackerContext, Data.Entities.Tenant, Guid, TenantReadModel>();
        services.AddEntityCommands<TrackerContext, Data.Entities.Tenant, Guid, TenantReadModel, TenantCreateModel, TenantUpdateModel>();
    }
}
