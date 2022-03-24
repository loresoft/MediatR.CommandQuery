using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

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

        try
        {
            LogStart(Logger, _name, request);
            var watch = Stopwatch.StartNew();

            var response = await Process(request, cancellationToken).ConfigureAwait(false);

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

    protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken);


    [LoggerMessage(1, LogLevel.Trace, "Processing handler '{handler}' for request '{request}' ...")]
    static partial void LogStart(ILogger logger, string handler, IRequest<TResponse> request);

    [LoggerMessage(2, LogLevel.Trace, "Processed handler '{handler}' for request '{request}': {elapsed} ms")]
    static partial void LogFinish(ILogger logger, string handler, IRequest<TResponse> request, long elapsed);

    [LoggerMessage(3, LogLevel.Trace, "Error processing handler '{handler}' for request '{request}': {errorMessage}")]
    static partial void LogError(ILogger logger, string handler, IRequest<TResponse> request, string errorMessage, Exception? exception);
}
