using System;
using System.Linq;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Logging;
using KickStart;
using Xunit;
using Xunit.Abstractions;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests
{
    [Collection(DatabaseCollection.CollectionName)]
    public abstract class DatabaseTestBase : IDisposable
    {
        protected DatabaseTestBase(ITestOutputHelper output, DatabaseFixture databaseFixture)
        {
            Output = output;
            Fixture = databaseFixture;
            Fixture?.Report(Output);
        }


        public ITestOutputHelper Output { get; }

        public DatabaseFixture Fixture { get; }

        public IServiceProvider ServiceProvider => Fixture?.ServiceProvider;


        public void Dispose()
        {
            Fixture?.Report(Output);
        }
    }
}