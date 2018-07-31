using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
{
    public abstract class PipelineBehaviorBase<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private static readonly Lazy<string> _requestName = new Lazy<string>(() => typeof(TRequest).Name);

        protected PipelineBehaviorBase(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
        }

        protected ILogger Logger { get; }


        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                Logger.LogTrace("Processing pipeline request '{0}' ...", _requestName.Value);
                var watch = Stopwatch.StartNew();

                var response = await Process(request, cancellationToken, next).ConfigureAwait(false);

                watch.Stop();
                Logger.LogTrace("Processed pipeline request '{0}': {1} ms", _requestName.Value, watch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error handling pipeline request '{0}': {1}", _requestName.Value, ex.Message);
                throw;
            }
        }

        protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);

    }
}