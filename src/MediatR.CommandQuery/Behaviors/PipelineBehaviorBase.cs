using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors
{
    public abstract class PipelineBehaviorBase<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private static readonly Lazy<string> _requestName = new Lazy<string>(() => typeof(TRequest).ToString());

        protected PipelineBehaviorBase(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
        }

        protected ILogger Logger { get; }


        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                Logger.LogTrace("Processing pipeline request '{requestName}' ...", _requestName.Value);
                var watch = Stopwatch.StartNew();

                var response = await Process(request, cancellationToken, next).ConfigureAwait(false);

                watch.Stop();
                Logger.LogTrace("Processed pipeline request '{requestName}': {elapsed} ms", _requestName.Value, watch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error handling pipeline request '{requestName}': {errorMessage}", _requestName.Value, ex.Message);
                throw;
            }
        }

        protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);

    }
}