using System.Threading;
using System.Threading.Tasks;

using Hangfire;

namespace MediatR.CommandQuery.Hangfire;

public interface IMediatorDispatcher
{
    [JobDisplayName("Job: {0}")]
    Task Send<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IBaseRequest;
}
