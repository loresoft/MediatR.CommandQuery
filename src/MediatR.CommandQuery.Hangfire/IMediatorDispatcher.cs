using Hangfire;
using Hangfire.Server;

namespace MediatR.CommandQuery.Hangfire;

public interface IMediatorDispatcher
{
    [JobDisplayName("Job: {0}")]
    Task Send<TRequest>(TRequest request, PerformContext? context, CancellationToken cancellationToken)
        where TRequest : IBaseRequest;
}
