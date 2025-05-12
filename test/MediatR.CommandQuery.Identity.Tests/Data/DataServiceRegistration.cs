using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.CommandQuery.Identity.Tests.Data;

public class DataServiceRegistration : IDesignTimeDbContextFactory<IdentityContext>
{
    [RegisterServices]
    public static void Register(IServiceCollection services)
    {
        services.AddDbContext<IdentityContext>(
            optionsAction: (provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Identity");

                options.UseAzureSql(
                    connectionString,
                    providerOptions => providerOptions
                        .EnableRetryOnFailure()
                        .CommandTimeout(300)
                );
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient
        );
    }

    public IdentityContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json")
         .Build();

        var connectionString = configuration.GetConnectionString("Identity");

        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();

        optionsBuilder.UseAzureSql(
            connectionString,
            providerOptions => providerOptions
                .EnableRetryOnFailure()
                .CommandTimeout(300)
        );

        return new IdentityContext(optionsBuilder.Options);
    }
}
