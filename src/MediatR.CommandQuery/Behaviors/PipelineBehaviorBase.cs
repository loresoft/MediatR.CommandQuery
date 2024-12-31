using System.Diagnostics;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

/// <summary>
/// A base pipeline behavior to surround the inner handler
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response from the handler.</typeparam>
/// <seealso cref="MediatR.IPipelineBehavior{TRequest, TResponse}" />
public abstract partial class PipelineBehaviorBase<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly string _name;

    protected PipelineBehaviorBase(ILoggerFactory loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(loggerFactory);

        var type = GetType();

        Logger = loggerFactory.CreateLogger(type);
        _name = type.Name;
    }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <value>
    /// The logger.
    /// </value>
    protected ILogger Logger { get; }


    /// <summary>
    /// Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Awaitable task returning the <typeparamref name="TResponse" />
    /// </returns>
    /// <exception cref="System.ArgumentNullException">When <paramref name="request"/> or <paramref name="next"/> is null</exception>
    public virtual async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ArgumentNullException.ThrowIfNull(next);

        var startTime = Stopwatch.GetTimestamp();
        try
        {
            LogStart(Logger, _name, request);
            return await Process(request, next, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            var elaspsed = Stopwatch.GetElapsedTime(startTime);
            LogFinish(Logger, _name, request, elaspsed.TotalMilliseconds);
        }
    }

    /// <summary>
    /// Processes the specified request with the additional behavior.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// Awaitable task returning the <typeparamref name="TResponse" />
    /// </returns>
    protected abstract Task<TResponse> Process(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken);


    [LoggerMessage(1, LogLevel.Trace, "Processing behavior '{Behavior}' for request '{Request}' ...")]
    static partial void LogStart(ILogger logger, string behavior, IRequest<TResponse> request);

    [LoggerMessage(2, LogLevel.Trace, "Processed behavior '{Behavior}' for request '{Request}': {Elapsed} ms")]
    static partial void LogFinish(ILogger logger, string behavior, IRequest<TResponse> request, double elapsed);
}
