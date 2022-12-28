using Injectio.Attributes;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tracker.WebService.Data
{
    public class DataServiceRegistration
    {
        [RegisterServices]
        public void Register(IServiceCollection services)
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
