using MediatR.CommandQuery.Queries;

namespace MediatR.CommandQuery.Services.Tests;

public class QueryStringEncoderTests
{
    [Fact]
    public void EncodeEntitySelect()
    {
        var entitySelect = new EntitySelect
        {
            Sort = [new() { Name = "Updated", Direction = "Descending" }],
            Filter = new() { Name = "Description", Operator = "IsNull" }
        };

        var queryString = QueryStringEncoder.Encode(entitySelect);
        queryString.Should().NotBeNullOrWhiteSpace();

        var resultSelect = QueryStringEncoder.Decode<EntitySelect>(queryString);
        resultSelect.Should().NotBeNull();
        resultSelect.Filter.Should().NotBeNull();
        resultSelect.Filter.Name.Should().Be("Description");
        resultSelect.Filter.Operator.Should().Be("IsNull");

        resultSelect.Sort.Should().NotBeNullOrEmpty();
        resultSelect.Sort[0].Name.Should().Be("Updated");
        resultSelect.Sort[0].Direction.Should().Be("Descending");

    }
}
