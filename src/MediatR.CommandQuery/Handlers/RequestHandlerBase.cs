using System.Diagnostics;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Handlers;

/// <summary>
/// A base handler for a request
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response from the handler.</typeparam>
/// <seealso cref="MediatR.IRequestHandler{TRequest, TResponse}" />
public abstract partial class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly string _name;

    protected RequestHandlerBase(ILoggerFactory loggerFactory)
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
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Response from the request
    /// </returns>
    /// <exception cref="System.ArgumentNullException">When request is null</exception>
    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var startTime = Stopwatch.GetTimestamp();
        try
        {
            LogStart(Logger, _name, request);
            return await Process(request, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            var elapsed = Stopwatch.GetElapsedTime(startTime);
            LogFinish(Logger, _name, request, elapsed.TotalMilliseconds);
        }
    }

    /// <summary>
    /// Processes the specified request.
    /// </summary>
    /// <param name="request">The request to process.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Response from the request</returns>
    protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken);


    [LoggerMessage(1, LogLevel.Trace, "Processing handler '{Handler}' for request '{Request}' ...")]
    static partial void LogStart(ILogger logger, string handler, IRequest<TResponse> request);

    [LoggerMessage(2, LogLevel.Trace, "Processed handler '{Handler}' for request '{Request}': {Elapsed} ms")]
    static partial void LogFinish(ILogger logger, string handler, IRequest<TResponse> request, double elapsed);
}
