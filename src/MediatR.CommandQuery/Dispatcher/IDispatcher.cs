namespace MediatR.CommandQuery.Dispatcher;

public interface IDispatcher
{
    Task<TResponse?> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
