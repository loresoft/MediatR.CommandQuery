using System;
using EntityFrameworkCore.CommandQuery.Queries;
using EntityFrameworkCore.CommandQuery.Tests.Samples;
using FluentAssertions;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.Tests.Queries
{
    public class EntityListQueryTests
    {
        [Fact]
        public void ConstructorNull()
        {
            var listQuery = new EntityListQuery<LocationReadModel>(null, null);
            listQuery.Should().NotBeNull();
        }

        [Fact]
        public void ConstructorWithParameters()
        {
            var entityFilter = new EntityFilter { Name = "rank", Value = 7 };
            var entityQuery = new EntityQuery("name = 'blah'", 2, 10, "updated:desc");
            entityQuery.Filter = entityFilter;

            var listQuery = new EntityListQuery<LocationReadModel>(entityQuery, MockPrincipal.Default);
            listQuery.Should().NotBeNull();

            listQuery.Query.Should().NotBeNull();
            listQuery.Principal.Should().NotBeNull();
        }
    }
}