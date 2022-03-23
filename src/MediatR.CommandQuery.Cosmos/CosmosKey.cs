using System;
using System.Diagnostics.CodeAnalysis;

using Cosmos.Abstracts;
using Cosmos.Abstracts.Extensions;
using Microsoft.Azure.Cosmos;

namespace MediatR.CommandQuery.Cosmos;

public static class CosmosKey
{
    public static bool TryDecode(string cosmosKey, [NotNullWhen(true)] out string? id, out PartitionKey partitionKey)
    {
        if (string.IsNullOrWhiteSpace(cosmosKey))
        {
            id = null;
            partitionKey = default;
            return false;
        }

        var parts = cosmosKey.Split('~');
        if (parts.Length == 1)
        {
            id = parts[0];
            partitionKey = default;
            return id.HasValue();
        }

        if (parts.Length >= 2)
        {
            id = parts[0];
            partitionKey = new PartitionKey(parts[1]);
            return id.HasValue();
        }

        id = null;
        partitionKey = default;
        return false;
    }


    public static string Encode(string id, string partitionKey)
    {
        if (id.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(id));

        return partitionKey.HasValue() ? $"{id}~{partitionKey}" : id;
    }

    public static string Encode(string id, PartitionKey partitionKey)
    {
        if (id.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(id));

        if (partitionKey == default)
            return id;

        var json = partitionKey.ToString();
        var keys = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(json);
        if (keys == null || keys.Length < 1)
            return id;

        return partitionKey != default ? $"{id}~{keys[0]}" : id;
    }

    public static string ToCosmosKey(this ICosmosEntity cosmosEntity)
    {
        if (cosmosEntity == null)
            throw new ArgumentNullException(nameof(cosmosEntity));

        var partitionKey = cosmosEntity.GetPartitionKey();

        return Encode(cosmosEntity.Id, partitionKey);
    }
}
