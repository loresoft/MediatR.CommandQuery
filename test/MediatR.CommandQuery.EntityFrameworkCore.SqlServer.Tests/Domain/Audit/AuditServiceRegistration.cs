using System;
using System.Collections.Generic;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit.Models;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit
{
    public class AuditServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<TrackerContext, Data.Entities.Audit, Guid, AuditReadModel>();
            services.AddEntityCommands<TrackerContext, Data.Entities.Audit, Guid, AuditReadModel, AuditCreateModel, AuditUpdateModel>();
        }
    }
}
