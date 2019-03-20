using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Definitions;
using EntityFrameworkCore.CommandQuery.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
{
    public class DistributedCacheQueryBehavior<TResponse> : PipelineBehaviorBase<PrincipalQueryBase<TResponse>, TResponse>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IDistributedCacheSerializer _distributedCacheSerializer;

        public DistributedCacheQueryBehavior(ILoggerFactory loggerFactory, IDistributedCache distributedCache, IDistributedCacheSerializer distributedCacheSerializer) : base(loggerFactory)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            _distributedCacheSerializer = distributedCacheSerializer ?? throw new ArgumentNullException(nameof(distributedCacheSerializer));
        }

        protected override async Task<TResponse> Process(PrincipalQueryBase<TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var cacheRequest = request as ICacheQueryResult;
            if (cacheRequest == null)
                return await next().ConfigureAwait(false); // don't cache


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

                return cachedItem;
            }

            // continue if not found in cache
            var item = await next().ConfigureAwait(false);
            if (item == null)
                return default(TResponse);

            // save to cache
            var itemBuffer = await _distributedCacheSerializer
                .ToByteArrayAsync(item)
                .ConfigureAwait(false);

            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = cacheRequest.SlidingExpiration(),
                AbsoluteExpiration = cacheRequest.AbsoluteExpiration()
            };

            await _distributedCache
                .SetAsync(cacheKey, itemBuffer, options, cancellationToken)
                .ConfigureAwait(false);

            return item;
        }
    }
}