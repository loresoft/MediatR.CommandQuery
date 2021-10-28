using System;
using System.Text;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.MongoDB.Tests.Logging
{
    public class LogEntry
    {
        public LogEntry(
            string category,
            LogLevel logLevel,
            EventId eventId,
            object state,
            Exception exception,
            string message)
        {
            Timestamp = DateTimeOffset.Now;
            LogLevel = logLevel;
            EventId = eventId;
            State = state;
            Exception = exception;
            Message = message;
            Category = category;
        }

        public DateTimeOffset Timestamp { get; }

        public string Category { get; }

        public EventId EventId { get; }

        public Exception Exception { get; }

        public LogLevel LogLevel { get; }

        public string Message { get; }

        public object State { get; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder
                .Append(Timestamp.ToString("HH:mm:ss.fff"))
                .Append(" [")
                .Append(LogLevel.ToString())
                .Append("] ")
                .Append(Category)
                .Append(": ")
                .Append(Message);

            if (Exception != null)
                builder.AppendLine().AppendLine(Exception.ToString());

            return builder.ToString();
        }
    }
}