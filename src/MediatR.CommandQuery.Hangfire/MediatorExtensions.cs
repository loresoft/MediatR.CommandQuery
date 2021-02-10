using System.Threading;
using System.Threading.Tasks;
using Hangfire;

namespace MediatR.CommandQuery.Hangfire
{
    public static class MediatorExtensions
    {
        // extra stuff to match IMediator.Send signature
        public static Task Enqueue<TRequest>(this IMediator mediator, TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IBaseRequest
        {
            BackgroundJob.Enqueue<IMediatorDispatcher>(dispatcher => dispatcher.Send(request, CancellationToken.None));

            return Task.CompletedTask;
        }
    }
}