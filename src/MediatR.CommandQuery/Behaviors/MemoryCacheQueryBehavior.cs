using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

/// <summary>
/// A behavior for caching the response of a query to <see cref="IMemoryCache"/>.
/// <typeparamref name="TRequest"/> must implement <see cref="ICacheResult"/> for the response to be cached.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public partial class MemoryCacheQueryBehavior<TRequest, TResponse> : PipelineBehaviorBase<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheQueryBehavior(ILoggerFactory loggerFactory, IMemoryCache memoryCache) : base(loggerFactory)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    protected override async Task<TResponse> Process(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        // cache only if implements interface
        var cacheRequest = request as ICacheResult;
        if (cacheRequest?.IsCacheable() != true)
            return await next().ConfigureAwait(false);

        var cacheKey = cacheRequest.GetCacheKey();

        if (_memoryCache.TryGetValue(cacheKey, out TResponse? cachedResult))
        {
            LogCacheAction(Logger, "Hit", cacheKey);
            return cachedResult!;
        }

        LogCacheAction(Logger, "Miss", cacheKey);

        // continue if not found in cache
        var result = await next().ConfigureAwait(false);
        if (result == null)
            return result;

        using (var entry = _memoryCache.CreateEntry(cacheKey))
        {
            entry.SlidingExpiration = cacheRequest.SlidingExpiration();
            entry.AbsoluteExpiration = cacheRequest.AbsoluteExpiration();
            entry.SetValue(result);

            LogCacheAction(Logger, "Insert", cacheKey);
        }

        return result;
    }

    [LoggerMessage(1, LogLevel.Trace, "Cache {Action}; Key: '{CacheKey}'")]
    static partial void LogCacheAction(ILogger logger, string action, string cacheKey);
}
