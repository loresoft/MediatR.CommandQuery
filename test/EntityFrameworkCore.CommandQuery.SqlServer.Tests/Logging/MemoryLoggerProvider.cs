using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Logging
{
    [ProviderAlias("Memory")]
    public class MemoryLoggerProvider : ILoggerProvider, ISupportExternalScope
    {
        private readonly ConcurrentQueue<LogEntry> _logEntries = new ConcurrentQueue<LogEntry>();


        public IExternalScopeProvider ScopeProvider { get; private set; }

        public IReadOnlyList<LogEntry> LogEntries => _logEntries.ToList();


        public ILogger CreateLogger(string categoryName)
        {
            return new MemoryLogger(this, categoryName);
        }

        public void WriteLog(LogEntry logEntry)
        {
            _logEntries.Enqueue(logEntry);
        }

        public void Clear()
        {
            _logEntries.Clear();
        }

        public void Dispose()
        {
            _logEntries.Clear();
        }

        void ISupportExternalScope.SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            ScopeProvider = scopeProvider;
        }


        private static readonly Lazy<MemoryLoggerProvider> _current = new Lazy<MemoryLoggerProvider>();

        public static MemoryLoggerProvider Current => _current.Value;
    }
}