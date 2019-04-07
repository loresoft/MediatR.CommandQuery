using System;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Logging
{
    public class MemoryLogger : ILogger
    {
        private readonly MemoryLoggerProvider _provider;
        private readonly string _categoryName;

        public MemoryLogger(MemoryLoggerProvider provider, string categoryName)
        {
            _provider = provider;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _provider.ScopeProvider?.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            var logEvent = new LogEntry(_categoryName, logLevel, eventId, state, exception, message);

            _provider.WriteLog(logEvent);
        }
    }
}
