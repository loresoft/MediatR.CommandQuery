using System;
using System.Collections.Generic;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tracker.WebService.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.WebService.Domain
{
    public class RoleServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<Tracker.WebService.Data.TrackerServiceContext, Tracker.WebService.Data.Entities.Role, Guid, RoleReadModel>();

            services.AddEntityCommands<Tracker.WebService.Data.TrackerServiceContext, Tracker.WebService.Data.Entities.Role, Guid, RoleReadModel, RoleCreateModel, RoleUpdateModel>();

        }

    }
}