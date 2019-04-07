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
                Logger.LogTrace("Cache Hit; Key: '{cacheKey}'.", cacheKey);
                return cachedResult;
            }

            Logger.LogTrace("Cache Miss; Key: '{cacheKey}'.", cacheKey);

            // continue if not found in cache
            var result = await next().ConfigureAwait(false);
            if (result == null)
                return default(TResponse);

            using (var entry = _memoryCache.CreateEntry(cacheKey))
            {
                entry.SlidingExpiration = cacheRequest.SlidingExpiration();
                entry.AbsoluteExpiration = cacheRequest.AbsoluteExpiration();
                entry.SetValue(result);

                Logger.LogTrace("Cache Insert; Key: '{cacheKey}'.", cacheKey);
            }

            return result;
        }
    }
}