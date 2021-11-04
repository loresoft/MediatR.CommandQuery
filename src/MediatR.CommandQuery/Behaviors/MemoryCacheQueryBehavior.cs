using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Definitions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors
{
    public class MemoryCacheQueryBehavior<TRequest, TResponse> : PipelineBehaviorBase<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheQueryBehavior(ILoggerFactory loggerFactory, IMemoryCache memoryCache) : base(loggerFactory)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        protected override async Task<TResponse> Process(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // cache only if implements interface
            var cacheRequest = request as ICacheQueryResult;
            if (cacheRequest == null || !cacheRequest.IsCacheable())
                return await next().ConfigureAwait(false);

            var cacheKey = cacheRequest.GetCacheKey();

            if (_memoryCache.TryGetValue(cacheKey, out TResponse cachedResult))
            {
                _logCacheHit(Logger, cacheKey, null);
                return cachedResult;
            }

            _logCacheMiss(Logger, cacheKey, null);

            // continue if not found in cache
            var result = await next().ConfigureAwait(false);
            if (result == null)
                return default;

            using (var entry = _memoryCache.CreateEntry(cacheKey))
            {
                entry.SlidingExpiration = cacheRequest.SlidingExpiration();
                entry.AbsoluteExpiration = cacheRequest.AbsoluteExpiration();
                entry.SetValue(result);

                _logCacheInsert(Logger, cacheKey, null);
            }

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
