// Ignore Spelling: Cacheable

using System.Security.Claims;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Queries;

public abstract record CacheableQueryBase<TResponse> : PrincipalQueryBase<TResponse>, ICacheResult
{
    private DateTimeOffset? _absoluteExpiration;
    private TimeSpan? _slidingExpiration;

    protected CacheableQueryBase(ClaimsPrincipal? principal) : base(principal)
    {
    }


    public abstract string GetCacheKey();

    public abstract string? GetCacheTag();

    public bool IsCacheable()
    {
        return _absoluteExpiration.HasValue
            || _slidingExpiration.HasValue;
    }


    public void Cache(DateTimeOffset absoluteExpiration)
    {
        _absoluteExpiration = absoluteExpiration;
    }

    public void Cache(TimeSpan expiration)
    {
        _slidingExpiration = expiration;
    }


    DateTimeOffset? ICacheResult.AbsoluteExpiration()
    {
        return _absoluteExpiration;
    }

    TimeSpan? ICacheResult.SlidingExpiration()
    {
        return _slidingExpiration;
    }
}
