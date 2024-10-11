using System.Security.Claims;

namespace MediatR.CommandQuery.Queries;

public record EntityPagedQuery<TReadModel> : CacheableQueryBase<EntityPagedResult<TReadModel>>
{
    public EntityPagedQuery(ClaimsPrincipal? principal, EntityQuery? query)
        : base(principal)
    {
        Query = query ?? new EntityQuery();
    }

    public EntityQuery Query { get; }


    public override string GetCacheKey()
    {
        var hash = Query.GetHashCode();
        return $"{typeof(TReadModel).FullName}-Paged-{hash}";
    }
}
