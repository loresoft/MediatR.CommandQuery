using System;
using System.Security.Principal;

namespace MediatR.CommandQuery.Queries;

public class EntityPagedQuery<TReadModel> : CacheableQueryBase<EntityPagedResult<TReadModel>>
{
    public EntityPagedQuery(IPrincipal principal, EntityQuery query)
        : base(principal)
    {
        Query = query ?? throw new ArgumentNullException(nameof(query));
    }

    public EntityQuery Query { get; }


    public override string GetCacheKey()
    {
        var hash = Query.GetHashCode();
        return $"{typeof(TReadModel).FullName}-Paged-{hash}";
    }
}
