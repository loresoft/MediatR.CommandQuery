using MediatR.CommandQuery.Identity.Tests.Data;
using MediatR.CommandQuery.Identity.Tests.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Testcontainers.MsSql;

using XUnit.Hosting;

namespace MediatR.CommandQuery.Identity.Tests;

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
            InitialCatalog = "IdentityCommandQuery"
        };

        // override connection string to use docker container
        var configurationData = new Dictionary<string, string?>
        {
            ["ConnectionStrings:Identity"] = connectionBuilder.ToString()
        };

        builder.Configuration.AddInMemoryCollection(configurationData);

        var services = builder.Services;

        services.AddHostedService<DatabaseInitializer>();

        services.AddAutoMapper(typeof(DatabaseFixture).Assembly);
        services.AddMediator();
        services.AddValidatorsFromAssembly<DatabaseFixture>();

        services.AddServerDispatcher();

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        services
            .AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<Role>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddIdentityCommands<User>();

        services.AddMediatRCommandQueryIdentityTests();
    }
}
