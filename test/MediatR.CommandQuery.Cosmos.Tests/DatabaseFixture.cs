using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using XUnit.Hosting;

namespace MediatR.CommandQuery.Cosmos.Tests;

public class DatabaseFixture : TestHostFixture
{
    protected override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddHostedService<DatabaseInitializer>();

        services.TryAddTransient<ITenantResolver<string>, MockTenantResolver>();

        services.AddCosmosRepository();
        services.AddAutoMapper(typeof(DatabaseFixture).Assembly);
        services.AddMediator();
        services.AddValidatorsFromAssembly<DatabaseFixture>();

        services.AddMediatRCommandQueryCosmosTests();
    }
}
