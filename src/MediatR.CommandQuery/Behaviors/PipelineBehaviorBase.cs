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
        private readonly string _name;

        protected PipelineBehaviorBase(ILoggerFactory loggerFactory)
        {
            var type = GetType();

            Logger = loggerFactory.CreateLogger(type);
            _name = type.Name;
        }

        protected ILogger Logger { get; }


        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                Logger.LogTrace("Processing behavior '{behavior}' for request '{request}' ...", _name, request);
                var watch = Stopwatch.StartNew();

                var response = await Process(request, cancellationToken, next).ConfigureAwait(false);

                watch.Stop();
                Logger.LogTrace("Processed behavior '{behavior}' for request '{request}': {elapsed} ms", _name, request, watch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error processing behavior '{behavior}' for request '{request}': {errorMessage}", _name, request, ex.Message);
                throw;
            }
        }

        protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);

    }
}