using System.Security.Claims;
using System.Text.Json.Serialization;

using MediatR.CommandQuery.Services;

namespace MediatR.CommandQuery.Queries;

public record EntitySelectQuery<TReadModel> : CacheableQueryBase<IReadOnlyCollection<TReadModel>>
{
    public EntitySelectQuery(ClaimsPrincipal? principal)
        : this(principal, new EntitySelect())
    {
    }

    public EntitySelectQuery(ClaimsPrincipal? principal, EntityFilter filter)
        : this(principal, new EntitySelect(filter))
    {
    }

    public EntitySelectQuery(ClaimsPrincipal? principal, EntityFilter filter, EntitySort sort)
        : this(principal, filter, [sort])
    {
    }

    public EntitySelectQuery(ClaimsPrincipal? principal, EntityFilter filter, IEnumerable<EntitySort> sort)
        : this(principal, new EntitySelect(filter, sort))
    {

    }

    [JsonConstructor]
    public EntitySelectQuery(ClaimsPrincipal? principal, EntitySelect? select)
        : base(principal)
    {
        Select = select ?? new EntitySelect();
    }

    public EntitySelect Select { get; }


    public override string GetCacheKey()
        => CacheTagger.GetKey<TReadModel, int>(CacheTagger.Buckets.List, Select.GetHashCode());

    public override string? GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
