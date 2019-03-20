using System;

namespace EntityFrameworkCore.CommandQuery.Definitions
{
    public interface ICacheQueryResult
    {
        string GetCacheKey();

        TimeSpan? SlidingExpiration();

        DateTimeOffset? AbsoluteExpiration();
    }
}
