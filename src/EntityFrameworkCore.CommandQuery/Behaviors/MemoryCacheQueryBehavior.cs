using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Definitions;
using EntityFrameworkCore.CommandQuery.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
{
    public class MemoryCacheQueryBehavior<TResponse> : PipelineBehaviorBase<PrincipalQueryBase<TResponse>, TResponse>
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheQueryBehavior(ILoggerFactory loggerFactory, IMemoryCache memoryCache) : base(loggerFactory)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        protected override async Task<TResponse> Process(PrincipalQueryBase<TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var cacheRequest = request as ICacheQueryResult;
            if (cacheRequest == null)
                return await next().ConfigureAwait(false); // don't cache

            var cacheKey = cacheRequest.GetCacheKey();
            var cachedResult = await _memoryCache
                .GetOrCreateAsync(cacheKey, entry =>
                {
                    entry.SlidingExpiration = cacheRequest.SlidingExpiration();
                    entry.AbsoluteExpiration = cacheRequest.AbsoluteExpiration();
                    return next();
                })
                .ConfigureAwait(false);

            return cachedResult;
        }
    }
}