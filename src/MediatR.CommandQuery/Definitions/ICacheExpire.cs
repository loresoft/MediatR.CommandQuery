namespace MediatR.CommandQuery.Definitions;

/// <summary>
/// An <c>interface</c> for cache expiration and cache tagging
/// </summary>
public interface ICacheExpire
{
    /// <summary>
    /// Gets the cache tag for this entity.
    /// </summary>
    /// <returns>The cache tag for this entity</returns>
    string? GetCacheTag();
}
