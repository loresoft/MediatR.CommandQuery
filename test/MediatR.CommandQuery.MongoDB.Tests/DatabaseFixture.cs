using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using MongoDB.Abstracts;

using XUnit.Hosting;

namespace MediatR.CommandQuery.MongoDB.Tests;

public class DatabaseFixture : TestApplicationFixture
{
    protected override void ConfigureApplication(HostApplicationBuilder builder)
    {
        base.ConfigureApplication(builder);

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
