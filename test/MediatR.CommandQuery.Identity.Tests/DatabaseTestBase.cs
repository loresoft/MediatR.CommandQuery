using XUnit.Hosting;

namespace MediatR.CommandQuery.Identity.Tests;

[Collection(DatabaseCollection.CollectionName)]
public abstract class DatabaseTestBase : TestHostBase<DatabaseFixture>
{
    protected DatabaseTestBase(ITestOutputHelper output, DatabaseFixture databaseFixture)
        : base(output, databaseFixture)
    {
    }

    public IServiceProvider ServiceProvider => Fixture.Services;

}
