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
                _logStart(Logger, _name, request, null);
                var watch = Stopwatch.StartNew();

                var response = await Process(request, cancellationToken).ConfigureAwait(false);

                watch.Stop();
                _logFinish(Logger, _name, request, watch.ElapsedMilliseconds, null);

                return response;
            }
            catch (Exception ex)
            {
                _logError(Logger, _name, request, ex.Message, ex);
                throw;
            }
        }

        protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken);


        private static readonly Action<ILogger, string, TRequest, Exception> _logStart
            = LoggerMessage.Define<string, TRequest>(LogLevel.Trace, 0, "Processing handler '{handler}' for request '{request}' ...");

        private static readonly Action<ILogger, string, TRequest, long, Exception> _logFinish
            = LoggerMessage.Define<string, TRequest, long>(LogLevel.Trace, 0, "Processed handler '{handler}' for request '{request}': {elapsed} ms");

        private static readonly Action<ILogger, string, TRequest, string, Exception> _logError
            = LoggerMessage.Define<string, TRequest, string>(LogLevel.Trace, 0, "Error processing handler '{handler}' for request '{request}': {errorMessage}");

    }
}
