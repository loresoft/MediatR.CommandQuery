using System;

namespace MediatR.CommandQuery.Definitions
{
    public interface ICacheQueryResult
    {
        bool IsCacheable();

        string GetCacheKey();

        TimeSpan? SlidingExpiration();

        DateTimeOffset? AbsoluteExpiration();
    }
}
