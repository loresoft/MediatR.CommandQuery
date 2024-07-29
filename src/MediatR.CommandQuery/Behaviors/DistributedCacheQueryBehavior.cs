using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public partial class DistributedCacheQueryBehavior<TRequest, TResponse> : PipelineBehaviorBase<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IDistributedCache _distributedCache;
    private readonly IDistributedCacheSerializer _distributedCacheSerializer;

    public DistributedCacheQueryBehavior(
        ILoggerFactory loggerFactory,
        IDistributedCache distributedCache,
        IDistributedCacheSerializer distributedCacheSerializer)
        : base(loggerFactory)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        _distributedCacheSerializer = distributedCacheSerializer ?? throw new ArgumentNullException(nameof(distributedCacheSerializer));
    }

    protected override async Task<TResponse> Process(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (next is null)
            throw new ArgumentNullException(nameof(next));

        if (next is null)
            throw new ArgumentNullException(nameof(next));

        // cache only if implements interface
        var cacheRequest = request as ICacheQueryResult;
        if (cacheRequest?.IsCacheable() != true)
            return await next().ConfigureAwait(false);

        var cacheKey = cacheRequest.GetCacheKey();

        // check cache
        var cachedBuffer = await _distributedCache
            .GetAsync(cacheKey, cancellationToken)
            .ConfigureAwait(false);

        if (cachedBuffer != null)
        {
            var cachedItem = await _distributedCacheSerializer
                .FromByteArrayAsync<TResponse>(cachedBuffer)
                .ConfigureAwait(false);

            LogCacheAction(Logger, "Hit", cacheKey);

            return cachedItem;
        }

        LogCacheAction(Logger, "Miss", cacheKey);

        // continue if not found in cache
        var result = await next().ConfigureAwait(false);
        if (result == null)
            return result;

        // save to cache
        var itemBuffer = await _distributedCacheSerializer
            .ToByteArrayAsync(result)
            .ConfigureAwait(false);

        var options = new DistributedCacheEntryOptions
        {
            SlidingExpiration = cacheRequest.SlidingExpiration(),
            AbsoluteExpiration = cacheRequest.AbsoluteExpiration()
        };

        await _distributedCache
            .SetAsync(cacheKey, itemBuffer, options, cancellationToken)
            .ConfigureAwait(false);

        LogCacheAction(Logger, "Insert", cacheKey);

        return result;
    }

    [LoggerMessage(1, LogLevel.Trace, "Cache {Action}; Key: '{CacheKey}'")]
    static partial void LogCacheAction(ILogger logger, string action, string cacheKey);

}
