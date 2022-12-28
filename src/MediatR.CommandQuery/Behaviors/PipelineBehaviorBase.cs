using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public abstract partial class PipelineBehaviorBase<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly string _name;

    protected PipelineBehaviorBase(ILoggerFactory loggerFactory)
    {
        if (loggerFactory is null)
            throw new ArgumentNullException(nameof(loggerFactory));

        var type = GetType();

        Logger = loggerFactory.CreateLogger(type);
        _name = type.Name;
    }

    protected ILogger Logger { get; }


    public virtual async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (next is null)
            throw new ArgumentNullException(nameof(next));

        try
        {
            LogStart(Logger, _name, request);
            var watch = Stopwatch.StartNew();

            var response = await Process(request, next, cancellationToken).ConfigureAwait(false);

            watch.Stop();
            LogFinish(Logger, _name, request, watch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            LogError(Logger, _name, request, ex.Message, ex);
            throw;
        }
    }

    protected abstract Task<TResponse> Process(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken);


    [LoggerMessage(1, LogLevel.Trace, "Processing behavior '{behavior}' for request '{request}' ...")]
    static partial void LogStart(ILogger logger, string behavior, IRequest<TResponse> request);

    [LoggerMessage(2, LogLevel.Trace, "Processed behavior '{behavior}' for request '{request}': {elapsed} ms")]
    static partial void LogFinish(ILogger logger, string behavior, IRequest<TResponse> request, long elapsed);

    [LoggerMessage(3, LogLevel.Trace, "Error processing behavior '{behavior}' for request '{request}': {errorMessage}")]
    static partial void LogError(ILogger logger, string behavior, IRequest<TResponse> request, string errorMessage, Exception? exception);
}
