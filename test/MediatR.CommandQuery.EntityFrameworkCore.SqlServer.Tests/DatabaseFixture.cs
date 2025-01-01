using MediatR.CommandQuery.Definitions;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using Testcontainers.MsSql;

using XUnit.Hosting;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests;

public class DatabaseFixture : TestApplicationFixture, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPassword("Bn87bBYhLjYRj%9zRgUc")
        .Build();

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
    }


    protected override void ConfigureApplication(HostApplicationBuilder builder)
    {
        base.ConfigureApplication(builder);

        // change database from container default
        var connectionBuilder = new SqlConnectionStringBuilder(_msSqlContainer.GetConnectionString())
        {
            InitialCatalog = "TrackerCommandQuery"
        };

        // override connection string to use docker container
        var configurationData = new Dictionary<string, string>
        {
            ["ConnectionStrings:Tracker"] = connectionBuilder.ToString()
        };

        builder.Configuration.AddInMemoryCollection(configurationData);

        var services = builder.Services;

        services.AddHostedService<DatabaseInitializer>();

        services.TryAddTransient<ITenantResolver<Guid>, MockTenantResolver>();

        services.AddAutoMapper(typeof(DatabaseFixture).Assembly);
        services.AddMediator();
        services.AddValidatorsFromAssembly<DatabaseFixture>();

        services.AddServerDispatcher();

        services.AddMediatRCommandQueryEntityFrameworkCoreSqlServerTests();
    }
}
