using Hangfire;

namespace MediatR.CommandQuery.Hangfire;

public static class MediatorExtensions
{
    // extra stuff to match IMediator.Send signature
    public static Task Enqueue<TRequest>(this ISender mediator, TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IBaseRequest
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(request);

        BackgroundJob.Enqueue<IMediatorDispatcher>(dispatcher => dispatcher.Send(request, null, CancellationToken.None));

        return Task.CompletedTask;
    }
}
