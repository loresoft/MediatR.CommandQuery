using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;

public class DataServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddDbContext<TrackerContext>(
            optionsAction: (provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Tracker");

                options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure());
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);
    }
}
