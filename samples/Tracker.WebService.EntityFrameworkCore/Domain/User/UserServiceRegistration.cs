using System;

using Injectio.Attributes;

using MediatR.CommandQuery.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;

using Tracker.WebService.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.WebService.Domain
{
    public class UserServiceRegistration
    {
        [RegisterServices]
        public void Register(IServiceCollection services)
        {
            services.AddEntityQueries<Tracker.WebService.Data.TrackerServiceContext, Tracker.WebService.Data.Entities.User, Guid, UserReadModel>();

            services.AddEntityCommands<Tracker.WebService.Data.TrackerServiceContext, Tracker.WebService.Data.Entities.User, Guid, UserReadModel, UserCreateModel, UserUpdateModel>();

        }

    }
}
