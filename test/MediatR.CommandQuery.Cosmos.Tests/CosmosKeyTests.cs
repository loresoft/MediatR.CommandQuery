using System.Collections.Generic;

using FluentAssertions;

using Microsoft.Azure.Cosmos;

using Xunit;

namespace MediatR.CommandQuery.Cosmos.Tests;

public class CosmosKeyTests
{
    [Theory]
    [MemberData(nameof(DecodeData))]
    [Trait("Category", "Cosmos")]
    public void TryDecodeTests(string cosmosKey, bool expectedResult, string expectedId, PartitionKey expectedPartitionKey)
    {
        var result = CosmosKey.TryDecode(cosmosKey, out var id, out var partitionKey);

        result.Should().Be(expectedResult);
        id.Should().Be(expectedId);
        partitionKey.Should().Be(expectedPartitionKey);
    }


    [Theory]
    [MemberData(nameof(EncodePartitionKeyData))]
    [Trait("Category", "Cosmos")]
    public void EncodePartitionKeyTests(string id, PartitionKey partitionKey, string cosmosKey)
    {
        var result = CosmosKey.Encode(id, partitionKey);
        result.Should().Be(cosmosKey);
    }

    [Theory]
    [MemberData(nameof(EncodeData))]
    [Trait("Category", "Cosmos")]
    public void EncodeTests(string id, string partitionKey, string cosmosKey)
    {
        var result = CosmosKey.Encode(id, partitionKey);
        result.Should().Be(cosmosKey);
    }


    public static IEnumerable<object[]> DecodeData =>
        new List<object[]>
        {
            new object[] { "key~partition", true, "key", new PartitionKey("partition") },
            new object[] { "340dfba8-d2a6-4b8d-8c42-171e46220287~340dfba8-d2a6-4b8d-8c42-171e46220287", true, "340dfba8-d2a6-4b8d-8c42-171e46220287", new PartitionKey("340dfba8-d2a6-4b8d-8c42-171e46220287") },
            new object[] { "key", true, "key", default(PartitionKey) },
            new object[] { "", false, default(string), default(PartitionKey) },
            new object[] { null, false, default(string), default(PartitionKey) },
            new object[] { "key~partition~other", true, "key", new PartitionKey("partition") },
            new object[] { "~partition", false, "", new PartitionKey("partition") }
        };

    public static IEnumerable<object[]> EncodePartitionKeyData =>
        new List<object[]>
        {
            new object[] { "key", new PartitionKey("partition"), "key~partition" },
            new object[] { "key", default(PartitionKey), "key" },
            new object[] { "key", null, "key" },
            new object[] { "key", new PartitionKey(123), "key~123.0" },
        };

    public static IEnumerable<object[]> EncodeData =>
        new List<object[]>
        {
            new object[] { "key", "partition", "key~partition" },
            new object[] { "key", string.Empty, "key" },
            new object[] { "key", null, "key" },
        };
}
