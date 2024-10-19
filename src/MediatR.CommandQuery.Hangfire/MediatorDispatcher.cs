using System.Diagnostics;

using Hangfire;
using Hangfire.Server;

using MediatR.CommandQuery.Services;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Hangfire;

public partial class MediatorDispatcher : IMediatorDispatcher
{
    private readonly IMediator _mediator;
    private readonly ILogger<MediatorDispatcher> _logger;

    public MediatorDispatcher(IMediator mediator, ILogger<MediatorDispatcher> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [JobDisplayName("Job: {0}")]
    public async Task Send<TRequest>(TRequest request, PerformContext? context, CancellationToken cancellationToken)
        where TRequest : IBaseRequest
    {
        ArgumentNullException.ThrowIfNull(request);

        var scope = _logger.BeginScope("Hangfire Job Id: {JobId}", context?.BackgroundJob?.Id);

        var startTime = ActivityTimer.GetTimestamp();
        try
        {
            LogStart(_logger, request);
            var watch = Stopwatch.StartNew();

            var response = await _mediator.Send(request, cancellationToken);

            watch.Stop();
            LogFinish(_logger, request, watch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            LogError(_logger, request, ex.Message, ex);
            throw;
        }
        finally
        {
            var elaspsed = ActivityTimer.GetElapsedTime(startTime);
            LogFinish(_logger, request, elaspsed.TotalMilliseconds);

            scope?.Dispose();
        }

    }

    [LoggerMessage(1, LogLevel.Trace, "Dispatching request '{Request}' ...")]
    static partial void LogStart(ILogger logger, IBaseRequest request);

    [LoggerMessage(2, LogLevel.Trace, "Dispatched request '{Request}': {Elapsed} ms")]
    static partial void LogFinish(ILogger logger, IBaseRequest request, double elapsed);

    [LoggerMessage(3, LogLevel.Error, "Error Dispatching request '{Request}': {ErrorMessage}")]
    static partial void LogError(ILogger logger, IBaseRequest request, string errorMessage, Exception? exception);
}
