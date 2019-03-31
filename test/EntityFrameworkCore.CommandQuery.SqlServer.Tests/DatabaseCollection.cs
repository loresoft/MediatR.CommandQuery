using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests
{
    [CollectionDefinition(DatabaseCollection.CollectionName)]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        public const string CollectionName = "DatabaseCollection";
    }
}
