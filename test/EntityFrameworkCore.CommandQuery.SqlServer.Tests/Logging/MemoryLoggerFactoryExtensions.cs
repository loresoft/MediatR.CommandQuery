using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Logging
{
    public static class MemoryLoggerFactoryExtensions
    {
        public static ILoggingBuilder AddMemory(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider>(s => MemoryLoggerProvider.Current);
            return builder;
        }
    }
}