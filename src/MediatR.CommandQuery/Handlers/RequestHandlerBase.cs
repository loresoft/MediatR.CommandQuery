using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Handlers
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private static readonly Lazy<string> _requestName = new Lazy<string>(() => typeof(TRequest).ToString());

        protected RequestHandlerBase(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
        }

        protected ILogger Logger { get; }

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogTrace("Processing request '{requestName}' ...", _requestName.Value);
                var watch = Stopwatch.StartNew();

                var response = await Process(request, cancellationToken).ConfigureAwait(false);

                watch.Stop();
                Logger.LogTrace("Processed request '{requestName}': {elapsed} ms", _requestName.Value, watch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error processing request '{requestName}': {errorMessage}", _requestName.Value, ex.Message);
                throw;
            }
        }

        protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken);
    }
}