using System.Diagnostics;

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
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            LogError(Logger, _name, request, ex.Message, ex);
            throw new DomainException(ex.Message, ex);
        }
    }

    protected abstract Task<TResponse> Process(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken);


    [LoggerMessage(1, LogLevel.Trace, "Processing behavior '{Behavior}' for request '{Request}' ...")]
    static partial void LogStart(ILogger logger, string behavior, IRequest<TResponse> request);

    [LoggerMessage(2, LogLevel.Trace, "Processed behavior '{Behavior}' for request '{Request}': {Elapsed} ms")]
    static partial void LogFinish(ILogger logger, string behavior, IRequest<TResponse> request, long elapsed);

    [LoggerMessage(3, LogLevel.Error, "Error processing behavior '{Behavior}' for request '{Request}': {ErrorMessage}")]
    static partial void LogError(ILogger logger, string behavior, IRequest<TResponse> request, string errorMessage, Exception? exception);
}
