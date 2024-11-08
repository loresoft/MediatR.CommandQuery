using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public partial class HybridCacheExpireBehavior<TRequest, TResponse> : PipelineBehaviorBase<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly HybridCache _hybridCache;

    public HybridCacheExpireBehavior(
        ILoggerFactory loggerFactory,
        HybridCache hybridCache)
        : base(loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(hybridCache);

        _hybridCache = hybridCache;
    }

    protected override async Task<TResponse> Process(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        var response = await next().ConfigureAwait(false);

        // expire cache
        if (request is not ICacheExpire cacheRequest)
            return response;

        var cacheTag = cacheRequest.GetCacheTag();
        if (!string.IsNullOrEmpty(cacheTag))
            await _hybridCache.RemoveByTagAsync(cacheTag, cancellationToken).ConfigureAwait(false);

        return response;
    }
}
