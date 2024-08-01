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
        queryString.Should().Be("CzsAAASe5889-eEHdHnRNvqRiyjYgBPCSHNOZNb72k7Uz0E3Rf2G1FNZKoamH5HbQf2pIoSw_X_G6I-iURkQtGj4RfU_qxOuhyLOyjHqEdIuoPiPR9Mj2EMwlSXXZQA");

        var resultSelect = QueryStringEncoder.Decode<EntitySelect>(queryString);
        resultSelect.Should().NotBeNull();

        resultSelect.Filter.Name.Should().Be("Description");
        resultSelect.Filter.Operator.Should().Be("IsNull");

        resultSelect.Sort.Should().NotBeNullOrEmpty();
    }
}
