using System.Security.Claims;
using System.Text.Json.Serialization;

using MediatR.CommandQuery.Services;

namespace MediatR.CommandQuery.Queries;

public record EntityContinuationQuery<TReadModel> : CacheableQueryBase<EntityContinuationResult<TReadModel>>
{
    public EntityContinuationQuery(ClaimsPrincipal? principal, EntitySelect? query, int pageSize = 10, string? continuationToken = null)
        : base(principal)
    {
        Query = query ?? new EntitySelect();
        PageSize = pageSize;
        ContinuationToken = continuationToken;
    }

    [JsonPropertyName("query")]
    public EntitySelect Query { get; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; }

    [JsonPropertyName("continuationToken")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ContinuationToken { get; }

    public override string GetCacheKey()
        => CacheTagger.GetKey<TReadModel, int>
        (
            bucket: CacheTagger.Buckets.Continuation,
            value: HashCode.Combine(Query.GetHashCode(), PageSize, ContinuationToken)
        );

    public override string? GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
