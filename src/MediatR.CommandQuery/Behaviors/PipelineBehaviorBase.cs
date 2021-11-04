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
                _logStart(Logger, _name, request, null);
                var watch = Stopwatch.StartNew();

                var response = await Process(request, cancellationToken, next).ConfigureAwait(false);

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

        protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);


        private static readonly Action<ILogger, string, TRequest, Exception> _logStart
            = LoggerMessage.Define<string, TRequest>(LogLevel.Trace, 0, "Processing behavior '{behavior}' for request '{request}' ...");

        private static readonly Action<ILogger, string, TRequest, long, Exception> _logFinish
            = LoggerMessage.Define<string, TRequest, long>(LogLevel.Trace, 0, "Processed behavior '{behavior}' for request '{request}': {elapsed} ms");

        private static readonly Action<ILogger, string, TRequest, string, Exception> _logError
            = LoggerMessage.Define<string, TRequest, string>(LogLevel.Trace, 0, "Error processing behavior '{behavior}' for request '{request}': {errorMessage}");

    }
}
