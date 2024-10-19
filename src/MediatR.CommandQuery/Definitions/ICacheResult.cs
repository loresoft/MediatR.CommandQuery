namespace MediatR.CommandQuery.Definitions;

public interface ICacheResult
{
    bool IsCacheable();


    string GetCacheKey();

    string? GetCacheTag();


    TimeSpan? SlidingExpiration();

    DateTimeOffset? AbsoluteExpiration();
}
