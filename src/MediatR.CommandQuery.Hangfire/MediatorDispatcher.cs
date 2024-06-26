using System.Diagnostics;

using Hangfire;

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
    public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IBaseRequest
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

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
        }

    }

    [LoggerMessage(1, LogLevel.Trace, "Dispatching request '{request}' ...")]
    static partial void LogStart(ILogger logger, IBaseRequest request);

    [LoggerMessage(2, LogLevel.Trace, "Dispatched request '{request}': {elapsed} ms")]
    static partial void LogFinish(ILogger logger, IBaseRequest request, double elapsed);

    [LoggerMessage(3, LogLevel.Error, "Error Dispatching request '{request}': {errorMessage}")]
    static partial void LogError(ILogger logger, IBaseRequest request, string errorMessage, Exception? exception);
}
