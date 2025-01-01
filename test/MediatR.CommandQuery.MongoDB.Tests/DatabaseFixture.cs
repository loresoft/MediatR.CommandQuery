
using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using MongoDB.Abstracts;
using MongoDB.Driver;

using Testcontainers.MongoDb;

using XUnit.Hosting;

namespace MediatR.CommandQuery.MongoDB.Tests;

public class DatabaseFixture : TestApplicationFixture, IAsyncLifetime
{
    private readonly MongoDbContainer _mongoDbContainer = new MongoDbBuilder()
        .WithUsername(string.Empty)
        .WithPassword(string.Empty)
        .Build();

    public async Task InitializeAsync()
    {
        await _mongoDbContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _mongoDbContainer.DisposeAsync();
    }

    protected override void ConfigureApplication(HostApplicationBuilder builder)
    {
        base.ConfigureApplication(builder);

        // change database from container default
        var connectionBuilder = new MongoUrlBuilder(_mongoDbContainer.GetConnectionString())
        {
            DatabaseName = "CommandQueryTracker"
        };

        // override connection string to use docker container
        var configurationData = new Dictionary<string, string>
        {
            ["ConnectionStrings:Tracker"] = connectionBuilder.ToString()
        };

        builder.Configuration.AddInMemoryCollection(configurationData);

        var services = builder.Services;

        services.AddHostedService<DatabaseInitializer>();

        services.TryAddTransient<ITenantResolver<string>, MockTenantResolver>();

        services.AddSingleton(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("Tracker");

            return MongoFactory.GetDatabaseFromConnectionString(connectionString);
        });
        services.AddSingleton(typeof(IMongoEntityRepository<>), typeof(MongoEntityRepository<>));

        services.AddAutoMapper(typeof(DatabaseFixture).Assembly);
        services.AddMediator();
        services.AddValidatorsFromAssembly<DatabaseFixture>();

        services.AddMediatRCommandQueryMongoDBTests();
    }
}
