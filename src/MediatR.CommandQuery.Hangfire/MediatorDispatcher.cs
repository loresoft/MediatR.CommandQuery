using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Hangfire
{
    public class MediatorDispatcher : IMediatorDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MediatorDispatcher> _logger;


        public MediatorDispatcher(IMediator mediator, ILogger<MediatorDispatcher> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [JobDisplayName("Job: {0}")]
        public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken)
            where TRequest : IBaseRequest
        {
            try
            {
                _logger.LogTrace("Dispatching request '{request}' ...", request);
                var watch = Stopwatch.StartNew();

                var response = await _mediator.Send(request, cancellationToken);

                watch.Stop();
                _logger.LogTrace("Dispatched request '{request}': {elapsed} ms", request, watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Dispatching request '{request}': {errorMessage}", request, ex.Message);
                throw;
            }
        }
    }
}