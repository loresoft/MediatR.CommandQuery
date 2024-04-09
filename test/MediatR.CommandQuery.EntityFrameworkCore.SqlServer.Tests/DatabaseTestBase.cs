using XUnit.Hosting;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests;

[Collection(DatabaseCollection.CollectionName)]
public abstract class DatabaseTestBase : TestHostBase<DatabaseFixture>
{
    protected DatabaseTestBase(ITestOutputHelper output, DatabaseFixture databaseFixture)
        : base(output, databaseFixture)
    {
    }

    public IServiceProvider ServiceProvider => Fixture.Services;

}
