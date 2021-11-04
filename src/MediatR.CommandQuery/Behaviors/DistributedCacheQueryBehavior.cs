using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Definitions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors
{
    public class DistributedCacheQueryBehavior<TRequest, TResponse> : PipelineBehaviorBase<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IDistributedCacheSerializer _distributedCacheSerializer;

        public DistributedCacheQueryBehavior(ILoggerFactory loggerFactory, IDistributedCache distributedCache, IDistributedCacheSerializer distributedCacheSerializer) : base(loggerFactory)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            _distributedCacheSerializer = distributedCacheSerializer ?? throw new ArgumentNullException(nameof(distributedCacheSerializer));
        }

        protected override async Task<TResponse> Process(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // cache only if implements interface
            var cacheRequest = request as ICacheQueryResult;
            if (cacheRequest == null || !cacheRequest.IsCacheable())
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

                _logCacheHit(Logger, cacheKey, null);

                return cachedItem;
            }

            _logCacheMiss(Logger, cacheKey, null);

            // continue if not found in cache
            var result = await next().ConfigureAwait(false);
            if (result == null)
                return default;

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

            _logCacheInsert(Logger, cacheKey, null);

            return result;
        }

        private static readonly Action<ILogger, string, Exception> _logCacheHit
            = LoggerMessage.Define<string>(LogLevel.Trace, 0, "Cache Hit; Key: '{cacheKey}'");

        private static readonly Action<ILogger, string, Exception> _logCacheMiss
            = LoggerMessage.Define<string>(LogLevel.Trace, 0, "Cache Miss; Key: '{cacheKey}'");

        private static readonly Action<ILogger, string, Exception> _logCacheInsert
            = LoggerMessage.Define<string>(LogLevel.Trace, 0, "Cache Insert; Key: '{cacheKey}'");

    }
}
