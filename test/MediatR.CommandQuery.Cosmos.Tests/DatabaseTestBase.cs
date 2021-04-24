using System;
using Xunit;
using Xunit.Abstractions;

namespace MediatR.CommandQuery.Cosmos.Tests
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