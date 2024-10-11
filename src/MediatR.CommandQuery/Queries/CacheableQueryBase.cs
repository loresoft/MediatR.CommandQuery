// Ignore Spelling: Cacheable

using System.Security.Claims;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Queries;

public abstract record CacheableQueryBase<TResponse> : PrincipalQueryBase<TResponse>, ICacheQueryResult
{
    private DateTimeOffset? _absoluteExpiration;
    private TimeSpan? _slidingExpiration;

    protected CacheableQueryBase(ClaimsPrincipal? principal) : base(principal)
    {
    }


    public abstract string GetCacheKey();

    public bool IsCacheable()
    {
        return _absoluteExpiration.HasValue
            || _slidingExpiration.HasValue;
    }


    public void Cache(DateTimeOffset? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
    {
        _absoluteExpiration = absoluteExpiration;
        _slidingExpiration = slidingExpiration;
    }


    DateTimeOffset? ICacheQueryResult.AbsoluteExpiration()
    {
        return _absoluteExpiration;
    }

    TimeSpan? ICacheQueryResult.SlidingExpiration()
    {
        return _slidingExpiration;
    }
}
