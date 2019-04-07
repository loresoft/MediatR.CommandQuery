using System;
using System.Collections.Generic;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.User.Models;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.User
{
    public class UserServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<TrackerContext, Data.Entities.User, Guid, UserReadModel>();
            services.AddEntityCommands<TrackerContext, Data.Entities.User, Guid, UserReadModel, UserCreateModel, UserUpdateModel>();
        }
    }
}
