using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

/// <summary>
/// A behavior for removing a cache tag of the response from <see cref="HybridCache"/>.
/// <typeparamref name="TRequest"/> must implement <see cref="ICacheExpire"/> for the cached tag.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public partial class HybridCacheExpireBehavior<TRequest, TResponse> : PipelineBehaviorBase<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly HybridCache _hybridCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="HybridCacheExpireBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="hybridCache">The hybrid cache.</param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public HybridCacheExpireBehavior(
        ILoggerFactory loggerFactory,
        HybridCache hybridCache)
        : base(loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(hybridCache);

        _hybridCache = hybridCache;
    }

    /// <inheritdoc />
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
