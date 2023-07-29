using System;

using Xunit;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests;

[CollectionDefinition(DatabaseCollection.CollectionName)]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    public const string CollectionName = "DatabaseCollection";
}
