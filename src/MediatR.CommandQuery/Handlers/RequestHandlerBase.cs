using System.Diagnostics;

using MediatR.CommandQuery.Services;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Handlers;

public abstract partial class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly string _name;

    protected RequestHandlerBase(ILoggerFactory loggerFactory)
    {
        if (loggerFactory is null)
            throw new ArgumentNullException(nameof(loggerFactory));

        var type = GetType();

        Logger = loggerFactory.CreateLogger(type);
        _name = type.Name;
    }

    protected ILogger Logger { get; }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var startTime = ActivityTimer.GetTimestamp();
        try
        {
            LogStart(Logger, _name, request);
            return await Process(request, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            var elaspsed = ActivityTimer.GetElapsedTime(startTime);
            LogFinish(Logger, _name, request, elaspsed.TotalMilliseconds);
        }
    }

    protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken);


    [LoggerMessage(1, LogLevel.Trace, "Processing handler '{Handler}' for request '{Request}' ...")]
    static partial void LogStart(ILogger logger, string handler, IRequest<TResponse> request);

    [LoggerMessage(2, LogLevel.Trace, "Processed handler '{Handler}' for request '{Request}': {Elapsed} ms")]
    static partial void LogFinish(ILogger logger, string handler, IRequest<TResponse> request, double elapsed);
}
