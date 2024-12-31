using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

/// <summary>
/// A behavior for caching the response of a query to <see cref="HybridCache"/>.
/// <typeparamref name="TRequest"/> must implement <see cref="ICacheResult"/> for the response to be cached.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public partial class HybridCacheQueryBehavior<TRequest, TResponse> : PipelineBehaviorBase<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly HybridCache _hybridCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="HybridCacheQueryBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="hybridCache">The hybrid cache.</param>
    /// <exception cref="System.ArgumentNullException">hybridCache</exception>
    public HybridCacheQueryBehavior(
        ILoggerFactory loggerFactory,
        HybridCache hybridCache)
        : base(loggerFactory)
    {
        _hybridCache = hybridCache ?? throw new ArgumentNullException(nameof(hybridCache));
    }

    /// <inheritdoc />
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
        var cacheTag = cacheRequest.GetCacheTag();

        var cacheOptions = new HybridCacheEntryOptions
        {
            Expiration = cacheRequest.SlidingExpiration(),
            LocalCacheExpiration = cacheRequest.SlidingExpiration(),
        };

        return await _hybridCache
            .GetOrCreateAsync(
                key: cacheKey,
                factory: async token => await next().ConfigureAwait(false),
                options: cacheOptions,
                tags: string.IsNullOrEmpty(cacheTag) ? null : [cacheTag],
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
