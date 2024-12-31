namespace MediatR.CommandQuery.Definitions;

/// <summary>
/// An <c>interface</c> for cache expiration and cache tagging
/// </summary>
public interface ICacheResult
{
    /// <summary>
    /// Determines whether this entity is cacheable.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this entity is cacheable; otherwise, <c>false</c>.
    /// </returns>
    bool IsCacheable();


    /// <summary>
    /// Gets the cache key for this entity.
    /// </summary>
    /// <returns>The cache key for this entity</returns>
    string GetCacheKey();

    /// <summary>
    /// Gets the cache tag for this entity.
    /// </summary>
    /// <returns>The cache tag for this entity</returns>
    string? GetCacheTag();


    /// <summary>
    /// Gets how long a cache entry can be inactive (e.g. not accessed) before it will be removed.
    /// </summary>
    /// <returns>The sliding expiration <see cref="TimeSpan"/> if set; otherwise <c>null</c>.</returns>
    TimeSpan? SlidingExpiration();

    /// <summary>
    /// Gets an absolute expiration date for the cache entry.
    /// </summary>
    /// <returns>The absolute expiration <see cref="DateTimeOffset"/> if set; otherwise <c>null</c>.</returns>
    DateTimeOffset? AbsoluteExpiration();
}
