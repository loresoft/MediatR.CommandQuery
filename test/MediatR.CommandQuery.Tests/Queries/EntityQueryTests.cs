using System.Linq;

using FluentAssertions;

using MediatR.CommandQuery.Queries;

using Xunit;

namespace MediatR.CommandQuery.Tests.Queries;

public class EntityQueryTests
{
    [Fact]
    public void ConstructorDefault()
    {
        var entityQuery = new EntityQuery();
        entityQuery.Should().NotBeNull();
        entityQuery.Page.Should().Be(1);
        entityQuery.PageSize.Should().Be(20);

        entityQuery.Sort.Should().BeNullOrEmpty();
        entityQuery.Filter.Should().BeNull();

    }

    [Fact]
    public void ConstructorParameters()
    {
        var entityFilter = new EntityFilter { Name = "rank", Value = 7 };
        var entityQuery = new EntityQuery("name = 'blah'", 2, 10, "updated:desc");
        entityQuery.Filter = entityFilter;

        entityQuery.Should().NotBeNull();

        entityQuery.Query.Should().Be("name = 'blah'");
        entityQuery.Page.Should().Be(2);
        entityQuery.PageSize.Should().Be(10);
        entityQuery.Filter.Should().NotBeNull();
        entityQuery.Sort.Should().NotBeNullOrEmpty();

        var first = entityQuery.Sort.First();
        first.Name.Should().Be("updated");
        first.Direction.Should().Be("desc");
    }

    [Fact]
    public void ConstructorParametersNull()
    {
        var entityQuery = new EntityQuery(null, 1, 5, null);
        entityQuery.Should().NotBeNull();
        entityQuery.Query.Should().BeNull();
        entityQuery.Page.Should().Be(1);
        entityQuery.PageSize.Should().Be(5);
        entityQuery.Sort.Should().BeNullOrEmpty();
    }
}
