using System.Collections.Generic;

using KickStart.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tracker.WebService.Data
{
    public class DataServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddDbContext<TrackerServiceContext>(
                (serviceProvider, options) =>
                {
                    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("Tracker");

                    options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure());
                },
                ServiceLifetime.Transient
            );
        }
    }
}
