using System;
using System.Collections.Generic;
using KickStart.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data
{
    public class DataServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            data.TryGetValue("configuration", out var configurationData);

            if (!(configurationData is IConfiguration configuration))
                return;

            var connectionString = configuration.GetConnectionString("Tracker");

            services.AddDbContext<TrackerContext>(options => options
                .UseSqlServer(connectionString)
            );
        }
    }
}
