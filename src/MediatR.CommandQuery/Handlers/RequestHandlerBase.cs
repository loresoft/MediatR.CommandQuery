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
        private readonly string _name;

        protected RequestHandlerBase(ILoggerFactory loggerFactory)
        {
            var type = GetType();

            Logger = loggerFactory.CreateLogger(type);
            _name = type.Name;
        }

        protected ILogger Logger { get; }

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogTrace("Processing handler '{handler}' for request '{request}' ...", _name, request);
                var watch = Stopwatch.StartNew();

                var response = await Process(request, cancellationToken).ConfigureAwait(false);

                watch.Stop();
                Logger.LogTrace("Processed handler '{handler}' for request '{requestName}': {elapsed} ms", _name, request, watch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error processing handler '{handler}' for request '{requestName}': {errorMessage}", _name, request, ex.Message);
                throw;
            }
        }

        protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken);
    }
}